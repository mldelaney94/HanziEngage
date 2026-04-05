using System.Text.RegularExpressions;
using UnityEngine;

namespace ShadowrunReturnsLanguageEngage
{
  public static class WordPopup
  {
    private static UIPanel parentPanel;
    private static UILabel label;

    private const int PanelWidth = 300;
    private const int PanelHeight = 400;
    private const int TextLineWidth = 280;
    private const int BorderThickness = 1;
    private const int PopupVerticalOffset = 100;
    private const int PopupRightOffset = 100;
    private const int PopupLeftOffset = -250;
    private const string BackgroundColor = "060606";
    private const string BorderColor = "62b6bd";
    private const string WordHighlightColor = "EFD27B";

    public static void Show(string text, UIPanel convoPanel, Vector3 worldPos)
    {
      if (Regex.IsMatch(text, "[0-9a-zA-Z]+")) return;

      var root = EnsureCreated(convoPanel);
      label.text = FormatDictionaryDefinition(text);
      Position(convoPanel, root, worldPos);
      parentPanel.gameObject.SetActive(true);
    }

    public static void Hide()
    {
      parentPanel.gameObject?.SetActive(false);
    }

    private static UIRoot EnsureCreated(UIPanel convoPanel)
    {
      var root = NGUITools.FindInParents<UIRoot>(convoPanel.gameObject);
      if (parentPanel == null) Create(root);
      return root;
    }

    private static void Position(UIPanel convoPanel, UIRoot root, Vector3 worldPos)
    {
      var isRight = root.transform.InverseTransformPoint(worldPos).x > 0;
      var xOffset = isRight ? PopupRightOffset : PopupLeftOffset;
      parentPanel.transform.localPosition = new Vector3(
        convoPanel.transform.localPosition.x + xOffset,
        PopupVerticalOffset,
        0);
    }

    private static void Create(UIRoot root)
    {
      parentPanel = NGUITools.AddChild<UIPanel>(root.gameObject);
      parentPanel.name = "SLRETextPopup";

      CreateBackground(parentPanel.gameObject);
      label = CreateTextPanel(parentPanel.gameObject);
      //CreateScrollBar(parentPanel.gameObject);
    }

    private static void CreateBackground(GameObject parent)
    {
      var panel = NGUITools.AddChild<UIPanel>(parent);

      var bg = NGUITools.AddWidget<UITexture>(panel.gameObject);
      bg.color = NGUITools.ParseColor(BackgroundColor, 0);
      bg.transform.localScale = new Vector3(PanelWidth, PanelHeight, 1f);
      bg.material = CreateFlatMaterial(renderQueue: 1);

      var border = NGUITools.AddWidget<UITexture>(panel.gameObject);
      border.color = NGUITools.ParseColor(BorderColor, 0);
      border.transform.localScale = new Vector3(
        PanelWidth + BorderThickness,
        PanelHeight + BorderThickness,
        1f);
      border.material = CreateFlatMaterial(renderQueue: bg.material.renderQueue - 1);
    }

    private static UILabel CreateTextPanel(GameObject parent)
    {
      var panel = NGUITools.AddChild<UIPanel>(parent);
      panel.clipping = UIDrawCall.Clipping.HardClip;
      panel.clipRange = new Vector4(0, 0, PanelWidth, PanelHeight);

      var dragPanel = NGUITools.AddChild<UIDraggablePanel>(panel.gameObject);
      dragPanel.disableDragIfFits = true;
      dragPanel.transform.localScale = new Vector3(PanelWidth, PanelHeight, 1f);

      var textLabel = NGUITools.AddWidget<UILabel>(panel.gameObject);
      textLabel.font = FindFont();
      textLabel.lineWidth = TextLineWidth;
      textLabel.pivot = UIWidget.Pivot.TopLeft;
      textLabel.transform.localPosition = new Vector3(-PanelWidth / 2f, PanelHeight / 2f, 0);

      return textLabel;
    }

    private static void CreateScrollBar(GameObject parent)
    {
      var scrollBar = NGUITools.AddChild<UIScrollBar>(parent);

      var collider = scrollBar.gameObject.AddComponent<BoxCollider>();
      collider.center = Vector3.zero;
      collider.size = new Vector2(PanelWidth, PanelHeight);

      scrollBar.gameObject.AddComponent<UIEventListener>();
    }

    private static Material CreateFlatMaterial(int renderQueue)
    {
      return new Material(Shader.Find("Unlit/Transparent Colored"))
      {
        renderQueue = renderQueue
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

    // This belongs here because it formats the string for display
    // specifically for this popup
    private static string FormatDictionaryDefinition(string word)
    {
      return "{{" + WordHighlightColor + "}}" + word + "{{-}}" + '\n'
        + Globals.CEDict[word]["pinyin"] + '\n'
        + Globals.CEDict[word]["english"];
    }
  }
}
