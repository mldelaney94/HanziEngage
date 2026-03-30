using System.Collections.Generic;

namespace ShadowrunReturnsLanguageEngage
{
  public static class Globals
  {
    public static Dictionary<UILabel, LabelDataObject> LabelRegistry = [];
    public static UILabel currentRenderingLabel = null;
  }
}