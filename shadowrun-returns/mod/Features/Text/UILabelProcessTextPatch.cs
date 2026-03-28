using HarmonyLib;
using ShadowrunReturnsLanguageEngage.Features.LabelDataObject;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShadowrunReturnsLanguageEngage
{
  [HarmonyPatch(typeof(UILabel), nameof(UILabel.OnFill))]
  internal static class UILabelOnFillPatch
  {
    private static readonly Dictionary<string, Func<string, string>> Actions = new()
    {
      { "NameLabel", FormatNameLabel },
      { "TextLabel", FormatTextLabel }
    };

    private static void Prefix(UILabel __instance)
    {
      var name = __instance.gameObject.name;

      if (Actions.ContainsKey(name))
      {
        __instance.text = Actions[name].Invoke(__instance.text);
        Globals.LabelRegistry.Add(new LabelDataObject(__instance));
      }
    }

    // NameLabel text arrives as "Chinese\npin yin" — collapse pinyin spaces
    // and capitalize, then place it inline: "Chinese Pinyin"
    private static string FormatNameLabel(string text)
    {
      var parts = text.Split('\n');
      var chinese = parts[0];
      var pinyin = parts[1];
      pinyin = pinyin[0].ToString().ToUpper() + pinyin.Substring(1);
      pinyin = string.Join("", pinyin.Split(' '));
      return chinese + " " + pinyin;
    }

    // Emote lines erroneously look like so: {{EFD27B}}chinese\n\npinyin{{-}}.
    // This cannot be fixed in preprocessing because the game sometimes injects
    // the colours at runtime.
    // Split them into individually colored bracket-delimited lines
    private static string FormatTextLabel(string text)
    {
      bool isSingleColorEmote =
        text.StartsWith("{{EFD27B}}")
        && text.EndsWith("{{-}}")
        && Regex.Matches(text, Regex.Escape("{{EFD27B}}")).Count == 1;

      if (isSingleColorEmote)
        return string.Join("]{{-}}\n\n{{EFD27B}}[", text.Split('\n'));

      return text;
    }
  }
}
