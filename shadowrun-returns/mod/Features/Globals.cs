using ShadowrunReturnsLanguageEngage.Features.LabelDataObject;
using System.Collections.Generic;

public static class Globals
{
  public static Dictionary<UILabel, LabelDataObject> LabelRegistry = [];
  public static UILabel currentRenderingLabel = null;
}