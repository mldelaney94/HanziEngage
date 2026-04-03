using System.Text.RegularExpressions;
using UnityEngine;

namespace ShadowrunReturnsLanguageEngage
{
  public static class WordPopup
  {
    private static GameObject panel;
    private static UILabel label;
    private const int PanelWidth = 300;
    private const int PanelHeight = 400;
    private const int TextLineWidth = 280;

    public static void Show(string text, UIPanel parentPanel, Vector3 worldPos)
    {
      if (Regex.Match(text, "[0-9a-zA-Z]+").Success)
      {
        return;
      }

      if (panel == null) Create(parentPanel);

      label.text = GetDictionaryDefinition(text);

      var root = NGUITools.FindInParents<UIRoot>(parentPanel.gameObject);
      var isRight = root.transform.InverseTransformPoint(worldPos).x > 0;

      panel.transform.localPosition = isRight
        ? new Vector3(parentPanel.transform.localPosition.x + 100, 100, 0)
        : new Vector3(parentPanel.transform.localPosition.x - 250, 100, 0);
      panel.SetActive(true);
    }

    public static void Hide()
    {
      panel?.SetActive(false);
    }

    private static void Create(UIPanel parentPanel)
    {
      var root = NGUITools.FindInParents<UIRoot>(parentPanel.gameObject);
      panel = NGUITools.AddChild<UIPanel>(root.gameObject).gameObject;

      var bgTexture = NGUITools.AddWidget<UITexture>(panel.gameObject);
      SetBackgroundTexture(bgTexture);
      var borderTexture = NGUITools.AddWidget<UITexture>(panel.gameObject);
      SetBorderTexture(borderTexture, bgTexture);

      label = NGUITools.AddWidget<UILabel>(panel.gameObject);
      label.font = FindFont();
      label.lineWidth = TextLineWidth;
      label.pivot = UIWidget.Pivot.TopLeft;
      label.transform.localPosition = new Vector3(
        -TextLineWidth / 2f,
        new Vector4(0, 0, PanelWidth, PanelHeight).w / 2f,
        -1f);


      panel.name = "SLRETextPopup";
    }

    private static void SetBackgroundTexture(UITexture texture)
    {
      texture.color = NGUITools.ParseColor("060606", 0);
      texture.transform.localScale = new Vector3(PanelWidth, PanelHeight, 1f);
      texture.material = new Material(Shader.Find("Unlit/Transparent Colored"))
      {
        renderQueue = 1
      };
    }
    private static void SetBorderTexture(UITexture borderTexture, UITexture textureToBorder)
    {
      borderTexture.color = NGUITools.ParseColor("62b6bd", 0);
      borderTexture.transform.localScale = new Vector3(PanelWidth + 1, PanelHeight + 1, 1f);
      borderTexture.material = new Material(Shader.Find("Unlit/Transparent Colored"))
      {
        renderQueue = textureToBorder.material.renderQueue - 1
      };
    }

    private static UIFont FindFont()
    {
      foreach (var key in Globals.LabelRegistry.Keys)
      {
        if (key.transform != null) return key.font;
      }
      return null;
    }

    private static string GetDictionaryDefinition(string word)
    {
      return "{{EFD27B}}" + word + "{{-}}" + '\n'
        + Globals.CEDict[word]["pinyin"] + '\n'
        + Globals.CEDict[word]["english"];
    }
  }
}
