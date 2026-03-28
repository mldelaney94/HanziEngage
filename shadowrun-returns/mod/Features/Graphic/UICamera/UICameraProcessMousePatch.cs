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
    private static readonly HashSet<string> acceptableParents = 
      [
        "ConversationDragContents",
        "ConversationResponse(Clone)"
      ];

    // MouseOrTouch[] 0 is where what's underneath the mouse is
    private static void Postfix(MouseOrTouch[] ___mMouse, RaycastHit ___lastHit)
    {
      if (___mMouse.Length == 0 || ___mMouse[0].current == null) return;
      if (!acceptableParents.Contains(___mMouse[0].current.name))
      {
        lastWord = string.Empty;
        return;
      }

      // the mouse collides with ConversationDragPanel, which does not contain a TextLabel
      // however, as visually it contains the text, it must be stacked somewhere underneath the drag panels parent
      var parent = ___mMouse[0].current.transform.parent;
      var textLabel = FindTextLabel(parent, ___lastHit);

      if (textLabel == null) return;

      var textLabelPoint = textLabel.transform.InverseTransformPoint(___lastHit.point);

      var quadIndex = PointIsInBoxes(textLabelPoint, textLabel.textQuads);
      if (quadIndex < 0) return;

      string word = ExtractWord(quadIndex, textLabel);
      if (word.Length > 0 && word != lastWord)
      {
        lastWord = word;
        ShadowrunreturnsLanguageEngage.Log.LogInfo(lastWord);
      }
    }

    private static LabelDataObject FindTextLabel(Transform parent, RaycastHit lastHit)
    {
      // In conversations with NPC's, there are multiple TextLabels with no
      // guaranteed order returned from GetComponentsInChildren.
      // Thus we must also check which parentLabel the mouse is hovering over
      var labels = parent.GetComponentsInChildren<UILabel>();
      foreach (var parentLabel in labels)
      {
        foreach (var label in Globals.LabelRegistry)
        {
          if (label.text == parentLabel.text && PointIsInBoxes(label.transform.InverseTransformPoint(lastHit.point), label.corners) >= 0)
          {
            return label;
          }
        }
      }

      return null;
    }

    private static int PointIsInBoxes(Vector3 localPoint, List<Vector3> boxes)
    {
      if (boxes.Count % 4 != 0)
      {
        ShadowrunreturnsLanguageEngage.Log.LogWarning($"Box collection size modulo 4 should be 0. Is ({boxes.Count} % 4 == {boxes.Count % 4})");
      }
      for (int i = 0; i < boxes.Count; i += 4)
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
