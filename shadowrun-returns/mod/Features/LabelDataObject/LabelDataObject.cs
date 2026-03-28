using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowrunReturnsLanguageEngage.Features.LabelDataObject
{
  public class LabelDataObject
  {
    public BetterList<Vector3> corners = new ();
    public string text = string.Empty;
    public Transform transform;
    public List<int> textIndices = [];
    public BetterList<Vector3> textQuads = new ();
    public BetterList<Color> colors = new();

    public LabelDataObject(UILabel label, BetterList<Color> colors, BetterList<Vector3> textQuads, string text, List<int> textIndices)
    {
      transform = label.transform;
      this.text = text;
      this.colors = colors;
      this.textQuads = textQuads;
      this.textIndices = textIndices;

      corners = CalculateCorners(label);
    }

    private BetterList<Vector3> CalculateCorners(UILabel label)
    {
      // pivot offset ranges between 0 and 1
      var x = label.relativeSize.x * (label.pivotOffset.x + 1);
      var y = label.relativeSize.y * (label.pivotOffset.y - 1);

      var bounds = new BetterList<Vector3>();
      bounds.Add(new Vector3(x, 0, 0));
      bounds.Add(Vector3.zero);
      bounds.Add(new Vector3(0, y, 0));
      bounds.Add(Vector3.zero);

      return bounds;
    }
  }
}
