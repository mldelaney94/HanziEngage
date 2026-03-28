using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace ShadowrunReturnsLanguageEngage
{
  [HarmonyPatch(typeof(UIFont), nameof(UIFont.Print))]
  internal static class UIFontPrintPatch
  {
    private static void Postfix(
        string text,
        BetterList<Vector3> verts,
        bool encoding,
        UIFont __instance
      )
    {
      var indexMap = BuildIndexMap(text, encoding);
      
      int expectedQuadCount = verts.size / 4;

      if (expectedQuadCount != indexMap.Count)
      {
        ShadowrunreturnsLanguageEngage.Log.LogWarning(
          $"Index map mismatch: {indexMap.Count} mapped vs {expectedQuadCount} quads"
        );
      }

      foreach (var label in Globals.LabelRegistry)
      {
        if (label.text == text)
        {
          label.textQuads = verts.ToArray().ToList();
          label.textIndices = indexMap;
        }
      }
    }

    // Copied from UIFont.Print
    internal static List<int> BuildIndexMap(string text, bool encoding)
    {
      var indexMap = new List<int>();
      int length = text.Length;

      for (int i = 0; i < length; i++)
      {
        char c = text[i];

        if (c == '\n') continue;
        if (c < ' ') continue;
        if (c == ' ') continue;
        if (c == '\u200B') continue;

        // skip {{.*}}'s
        if (encoding && c == '{' && i + 1 < length && text[i + 1] == '{'
            && (i == 0 || text[i - 1] != '\\'))
        {
          int end = text.IndexOf("}}", i + 2);
          if (end >= 0)
          {
            i = end + 1;
            continue;
          }
        }

        if (encoding && c == '\\' && i + 2 < length
            && text[i + 1] == '{' && text[i + 2] == '{')
        {
          continue;
        }

        indexMap.Add(i);
      }

      return indexMap;
    }
  }
}
