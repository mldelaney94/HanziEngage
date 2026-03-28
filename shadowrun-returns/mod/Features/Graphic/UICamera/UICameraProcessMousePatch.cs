#pragma warning disable Harmony003 // Harmony non-ref patch parameters modified throws a lot of false positives here
using HarmonyLib;
using ShadowrunReturnsLanguageEngage.Features.LabelDataObject;
using System.Collections.Generic;
using UnityEngine;
using static UICamera;

namespace ShadowrunReturnsLanguageEngage
{
  [HarmonyPatch(typeof(UICamera), "ProcessMouse")]
  internal static class UICameraProcessMousePatch
  {
    private static string lastWord = "";
    private static readonly HashSet<string> collisionNamesToCheck = 
      [
        "ConversationDragContents",
        "ConversationResponse(Clone)"
      ];

    // MouseOrTouch[] 0 is where what's underneath the mouse is
    private static void Postfix(MouseOrTouch[] ___mMouse, RaycastHit ___lastHit)
    {
      if (___mMouse.Length == 0 || ___mMouse[0].current == null) return;
      if (!collisionNamesToCheck.Contains(___mMouse[0].current.name))
      {
        lastWord = string.Empty;
        return;
      }

      var textLabel = FindTextLabel(___lastHit.point);

      if (textLabel == null)
      {
        return;
      }

      var textLabelPoint = textLabel.transform.InverseTransformPoint(___lastHit.point);

      var quadIndex = PointIsInBoxes(textLabelPoint, textLabel.textQuads);
      if (quadIndex < 0)
      {
        return;
      }

      string word = ExtractWord(quadIndex, textLabel);
      if (word.Length > 0 && word != lastWord)
      {
        lastWord = word;
        ShadowrunreturnsLanguageEngage.Log.LogInfo($"{lastWord}");
      }
    }

    private static LabelDataObject FindTextLabel(Vector3 lastHit)
    {
      foreach (var label in Globals.LabelRegistry.Values)
      {
        if (label.transform == null) continue;
        var localPoint = label.transform.InverseTransformPoint(lastHit);
        int boundsHit = PointIsInBoxes(localPoint, label.corners);
        if (boundsHit >= 0)
        {
          return label;
        }
      }

      return null;
    }

    private static int PointIsInBoxes(Vector3 localPoint, BetterList<Vector3> boxes)
    {
      if (boxes.size % 4 != 0)
      {
        ShadowrunreturnsLanguageEngage.Log.LogWarning($"Box collection size modulo 4 should be 0. Is ({boxes.size} % 4 == {boxes.size % 4})");
      }
      for (int i = 0; i < boxes.size; i += 4)
      {
        // [0] = topright
        // [1] = bottomright
        // [2] = bottomleft
        // [3] = topleft
        // so you only need two corners to know if we're inside the quad
        var topRight = boxes[i];
        var top = topRight.y;
        var right = topRight.x;
        var bottomLeft = boxes[i + 2];
        var bottom = bottomLeft.y;
        var left = bottomLeft.x;

        if (localPoint.y <= top && localPoint.y >= bottom
          && localPoint.x <= right && localPoint.x >= left)
        {
          return i;
        }
      }

      return -1;
    }

    private static string ExtractWord(int vertexBaseIndex, LabelDataObject label)
    {
      int quadNumber = vertexBaseIndex / 4;
      if (quadNumber >= label.textIndices.Count) return "";

      int strIdx = label.textIndices[quadNumber];
      var text = label.text; 

      if (IsBoundary(text[strIdx])) return "";

      int left = strIdx;
      while (left > 0 && !IsBoundary(text[left - 1]))
        left--;

      int right = strIdx;
      while (right < text.Length - 1 && !IsBoundary(text[right + 1]))
        right++;

      return text.Substring(left, right - left + 1);
    }

    private static bool IsBoundary(char c)
    {
      return c == '\u200B' || c == ' ' || c == '\n'
        || c == '[' || c == ']' || c == '{' || c == '}' || c == '\\'
        || c == '？' || c == '，' || c == '！' || c == '。' || c == '；'
        || c == '"' || c == '：' || c == '–' || c == '—'
        || c == '＊' || c == '…' || c == '、' || c == '～' || c == '－'
        || c == '（' || c == '）' || c == '─' || c == '＜' || c == '＞'
        || c == '．' || c == '《' || c == '》' || c == '％' || c == '·'
        || c == '\'' || c == '【' || c == '】';
    }
  }
}
