using UnityEngine;

namespace ShadowrunReturnsLanguageEngage
{
  public static class WordPopup
  {
    private static GameObject panel;
    private static UILabel label;

    public static void Show(string text, UIPanel parentPanel, Vector3 worldPos)
    {
      if (panel == null) Create(parentPanel);

      label.text = text;

      var root = NGUITools.FindInParents<UIRoot>(parentPanel.gameObject);
      panel.transform.localPosition = root.transform.InverseTransformPoint(worldPos);
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

      var texture = NGUITools.AddWidget<UITexture>(panel.gameObject);
      texture.alpha = 1;
      texture.color = Color.black;
      texture.transform.localScale = new Vector3(300, 400, 1f);
      texture.material = new Material(Shader.Find("Unlit/Transparent Colored"));

      label = NGUITools.AddWidget<UILabel>(panel.gameObject);

      foreach (var key in Globals.LabelRegistry.Keys)
      {
        if (key.transform != null)
        {
          label.font = key.font;
          break;
        }
      }

      panel.name = "SLRETextPopup";
    }
  }
}
