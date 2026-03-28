using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ShadowrunReturnsLanguageEngage.Features.LabelDataObject
{
  public class LabelDataObject
  {
    public List<Vector3> corners = [];
    public string text;
    public Transform transform;
    public List<int> textIndices;
    public List<Vector3> textQuads;

    public LabelDataObject(UILabel label)
    {
      transform = label.transform;
      text = label.text;

      corners = CalculateCorners(label);
    }

    private List<Vector3> CalculateCorners(UILabel label)
    {
      // pivot offset ranges between 0 and 1
      var x = label.relativeSize.x * (label.pivotOffset.x + 1);
      var y = label.relativeSize.y * (label.pivotOffset.y - 1);

      var bounds = new List<Vector3>
      {
        new Vector3(x, 0, 0),
        Vector3.zero,
        new Vector3(0, y, 0),
        Vector3.zero
      };

      return bounds;
    }
  }
}
