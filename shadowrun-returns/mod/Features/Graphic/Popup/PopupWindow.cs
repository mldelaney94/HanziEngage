using HarmonyLib;
using UnityEngine;

namespace ShadowrunReturnsLanguageEngage
{
  // UIFont.Print formats its pass-by-reference inputs *to be printed* by the caller
  // It does not actually print anything itself.
  // Each character has 4 vertices, so must be given 4 color values via `cols`
  // [HarmonyPatch(typeof(UIPopupList), nameof(UIPopupList.OnItemHover))]
  // internal static class PopupWindow
  // {
  //   private static void Postfix(
  //       string text,
  //       ref BetterList<Color> cols)
  //   {
  //     return;
  //   }
  // }
}
