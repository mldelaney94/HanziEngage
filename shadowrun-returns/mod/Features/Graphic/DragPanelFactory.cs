using UnityEngine;

namespace ShadowrunReturnsLanguageEngage
{
  public static class DragPanelFactory
  {
    public struct DragPanelGroup
    {
      public BoxCollider dragCollider;
      public UIPanel scrollPanel;
      public UIDraggablePanel draggablePanel;
      public UILabel label;
    }

    /// <summary>
    /// Creates the sibling trio that mirrors ConversationDragContents / ConversationDragPanel / TextLabel.
    /// All children are parented under <paramref name="parent"/>.
    /// </summary>
    public static DragPanelGroup Create(
      GameObject parent,
      string namePrefix,
      UIFont font,
      int lineWidth,
      Vector4 clipRange)
    {
      var dragContents = NGUITools.AddChild(parent);
      dragContents.name = namePrefix + "DragContents";
      var dragCollider = dragContents.AddComponent<BoxCollider>();
      dragCollider.center = Vector3.zero;
      dragCollider.size = new Vector3(clipRange.z, clipRange.w, 1f);

      var dragPanelGo = NGUITools.AddChild(parent);
      dragPanelGo.name = namePrefix + "DragPanel";
      var scrollPanel = dragPanelGo.AddComponent<UIPanel>();
      scrollPanel.clipping = UIDrawCall.Clipping.SoftClip;
      scrollPanel.clipRange = clipRange;
      var draggablePanel = dragPanelGo.AddComponent<UIDraggablePanel>();
      draggablePanel.restrictWithinPanel = true;

      var dragPanelContents = dragContents.AddComponent<UIDragPanelContents>();
      dragPanelContents.draggablePanel = draggablePanel;

      var label = NGUITools.AddWidget<UILabel>(dragPanelGo);
      label.gameObject.name = "TextLabel";
      label.font = font;
      label.lineWidth = lineWidth;
      label.pivot = UIWidget.Pivot.TopLeft;
      label.transform.localPosition = new Vector3(
        -lineWidth / 2f,
        clipRange.w / 2f,
        -1f);

      return new DragPanelGroup
      {
        dragCollider = dragCollider,
        scrollPanel = scrollPanel,
        draggablePanel = draggablePanel,
        label = label
      };
    }
  }
}
//[Info   :      SLRE][ConversationAnchor(Clone)](active = True, layer = 12)
//  < Transform >
//  < ConversationAnchor >
//  < UIPanel > clipping = None
//  [Frame](active = True, layer = 12)
//    < Transform >
//    < UISlicedSprite > atlas = Conversation spriteName = "conversation_frame_9p" depth = -1
//  [ContinueButton](active = True, layer = 12)
//    < Transform >
//    < UISprite > atlas = Conversation spriteName = "continue_button" depth = 0
//    < BasicButton >
//    < BoxCollider > center = (0.0, 0.0, 0.0) size = (1.0, 1.0, 1.0)
//    < UIButton >
//    < HBSUIButtonSound >
//  [Background](active = True, layer = 12)
//    < Transform >
//    < BoxCollider > center = (0.0, 0.0, 0.0) size = (1.0, 1.0, 1.0)
//    < AutoScaleToScreen >
//  [SpeakerFrame](active = True, layer = 12)
//    < Transform >
//    < UISlicedSprite > atlas = Conversation spriteName = "speakerPortraitFrame" depth = -1
//  [SpeakerPortrait](active = True, layer = 12)
//    < Transform >
//    < UISprite > atlas = spriteName = "Generic_Static_Blank" depth = -1
//  [ResponseDragPanelTop](active = True, layer = 12)
//    < Transform >
//    < BoxCollider > center = (0.0, 0.0, 0.0) size = (1.0, 1.0, 1.0)
//  [ResponseDragPanelBottom](active = True, layer = 12)
//    < Transform >
//    < BoxCollider > center = (0.0, 0.0, 0.0) size = (1.0, 1.0, 1.0)
//  [ResponsesDragContents](active = True, layer = 12)
//    < Transform >
//    < UIDragPanelContents >
//    < BoxCollider > center = (0.0, 0.0, 0.0) size = (1.0, 1.0, 1.0)
//  [ResponsesDragPanel](active = True, layer = 12)
//    < Transform >
//    < UIPanel > clipping = SoftClip
//    <UIDraggablePanel>
//    [ConversationResponse(Clone)](active = True, layer = 12)
//      < Transform >
//      < ConversationResponse >
//      < UIDragPanelContents >
//      < BoxCollider > center = (195.0, -33.0, 0.0) size = (390.0, 66.0, 1.0)
//      < BasicButton >
//      < UIButton >
//      < HBSUIButtonSound >
//      [Background](active = True, layer = 12)
//        < Transform >
//        < UISlicedSprite > atlas = Conversation spriteName = "text_button_9p" depth = 0
//      [TextLabel](active = True, layer = 12)
//        < Transform >
//        < UILabel > text = "{{96D5DA}}[​查看​联系人​清单​。​]{{-}}
//{
//  { 96D5DA}
//  ..." font=MediumNormal fontSize=36 lineWidth=356 depth=1 pivot=TopLeft pivotOffset=(0.0, 0.0) localPosition=(18.0, -14.0, -1.0)
//    [ConversationResponse(Clone)](active = True, layer = 12)
//      < Transform >
//      < ConversationResponse >
//      < UIDragPanelContents >
//      < BoxCollider > center = (195.0, -33.0, 0.0) size = (390.0, 66.0, 1.0)
//      < BasicButton >
//      < UIButton >
//      < HBSUIButtonSound >
//      [Background](active = True, layer = 12)
//        < Transform >
//        < UISlicedSprite > atlas = Conversation spriteName = "text_button_9p" depth = 0
//      [TextLabel](active = True, layer = 12)
//        < Transform >
//        < UILabel > text = "{{96D5DA}}[​查看​个人​日程表​。​]{{-}}
//{
//    { 96D5DA}
//    ..." font=MediumNormal fontSize=36 lineWidth=356 depth=1 pivot=TopLeft pivotOffset=(0.0, 0.0) localPosition=(18.0, -14.0, -1.0)
//    [ConversationResponse(Clone)](active = True, layer = 12)
//      < Transform >
//      < ConversationResponse >
//      < UIDragPanelContents >
//      < BoxCollider > center = (195.0, -33.0, 0.0) size = (390.0, 66.0, 1.0)
//      < BasicButton >
//      < UIButton >
//      < HBSUIButtonSound >
//      [Background](active = True, layer = 12)
//        < Transform >
//        < UISlicedSprite > atlas = Conversation spriteName = "text_button_9p" depth = 0
//      [TextLabel](active = True, layer = 12)
//        < Transform >
//        < UILabel > text = "{{96D5DA}}[​放下​你​的​笔记本​。​]{{-}}
//{
//      {
//        96D5DA..." font=MediumNormal fontSize=36 lineWidth=356 depth=1 pivot=TopLeft pivotOffset=(0.0, 0.0) localPosition=(18.0, -14.0, -1.0)
//  [ResponsesDragScrollBar](active = True, layer = 12)
//    < Transform >
//    < UIScrollBar >
//    [Scalar](active = True, layer = 12)
//      < Transform >
//      [Background](active = False, layer = 12)
//        < Transform >
//        < UISlicedSprite > atlas = PDA spriteName = "scrollBarFrame" depth = 1
//        < BoxCollider > center = (0.0, -0.5, 0.0) size = (1.0, 1.0, 1.0)
//        < UIEventListener >
//      [Transform](active = True, layer = 12)
//        < Transform >
//        [Foreground](active = False, layer = 12)
//          < Transform >
//          < UISlicedSprite > atlas = PDA spriteName = "scrollBar" depth = 2
//          < BoxCollider > center = (0.0, -0.5, 0.0) size = (1.0, 1.0, 1.0)
//          < UIEventListener >
//  [Scalar](active = True, layer = 0)
//    < Transform >
//    [PortraitFrame](active = True, layer = 12)
//      < Transform >
//      < UISlicedSprite > atlas = Conversation spriteName = "portrait_frame_9p" depth = 0
//  [Portrait](active = True, layer = 12)
//    < Transform >
//    < UISprite > atlas = SharedPortraitAtlas 0 spriteName = "pc_elffemale_00_faceless" depth = -1
//  [NameLabel](active = True, layer = 12)
//    < Transform >
//    < UILabel > text = "name" font = MediumBold fontSize = 36 lineWidth = 390 depth = 4 pivot = TopLeft pivotOffset = (0.0, 0.0) localPosition = (40.0, 330.0, -4.0)
//  [ConversationDragContents](active = True, layer = 12)
//    < Transform >
//    < UIDragPanelContents >
//    < BoxCollider > center = (0.0, 0.0, 0.0) size = (1.0, 1.0, 1.0)
//  [ConversationDragPanel](active = True, layer = 12)
//    < Transform >
//    < UIPanel > clipping = SoftClip
//    <UIDraggablePanel>
//    [TextLabel](active = True, layer = 12)
//      < Transform >
//      < UILabel > text = "{{EFD27B}}[你​的​笔记本​-​记录​了​日程表​，​联系人​之类​的..." font = MediumNormal fontSize = 36 lineWidth = 390 depth = 0 pivot = TopLeft pivotOffset = (0.0, 0.0) localPosition = (-195.0, 320.0, 1.0)
//  [ConversationDragScrollBar](active = True, layer = 12)
//    < Transform >
//    < UIScrollBar >
//    [Scalar](active = True, layer = 12)
//      < Transform >
//      [Background](active = False, layer = 12)
//        < Transform >
//        < UISlicedSprite > atlas = PDA spriteName = "scrollBarFrame" depth = 1
//        < BoxCollider > center = (0.0, -0.5, 0.0) size = (1.0, 1.0, 1.0)
//        < UIEventListener >
//      [Transform](active = True, layer = 12)
//        < Transform >
//        [Foreground](active = False, layer = 12)
//          < Transform >
//          < UISlicedSprite > atlas = PDA spriteName = "scrollBar" depth = 2
//          < BoxCollider > center = (0.0, -0.5, 0.0) size = (1.0, 1.0, 1.0)
//          < UIEventListener > [Info   :      SLRE] =============
//[Info   :      SLRE] [ConversationAnchor(Clone)] (active=True, layer=12)
//  <Transform>
//  <ConversationAnchor>
//  <UIPanel> clipping=None
//  [Frame] (active=True, layer=12)
//    <Transform>
//    <UISlicedSprite> atlas=Conversation spriteName="conversation_frame_9p" depth=-1
//  [ContinueButton] (active=True, layer=12)
//    <Transform>
//    <UISprite> atlas=Conversation spriteName="continue_button" depth=0
//    <BasicButton>
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//    <UIButton>
//    <HBSUIButtonSound>
//  [Background] (active=True, layer=12)
//    <Transform>
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//    <AutoScaleToScreen>
//  [SpeakerFrame] (active=True, layer=12)
//    <Transform>
//    <UISlicedSprite> atlas=Conversation spriteName="speakerPortraitFrame" depth=-1
//  [SpeakerPortrait] (active=True, layer=12)
//    <Transform>
//    <UISprite> atlas= spriteName="Generic_Static_Blank" depth=-1
//  [ResponseDragPanelTop] (active=True, layer=12)
//    <Transform>
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//  [ResponseDragPanelBottom] (active=True, layer=12)
//    <Transform>
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//  [ResponsesDragContents] (active=True, layer=12)
//    <Transform>
//    <UIDragPanelContents>
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//  [ResponsesDragPanel] (active=True, layer=12)
//    <Transform>
//    <UIPanel> clipping=SoftClip
//    <UIDraggablePanel>
//    [ConversationResponse(Clone)] (active=True, layer=12)
//      <Transform>
//      <ConversationResponse>
//      <UIDragPanelContents>
//      <BoxCollider> center=(195.0, -33.0, 0.0) size=(390.0, 66.0, 1.0)
//      <BasicButton>
//      <UIButton>
//      <HBSUIButtonSound>
//      [Background] (active=True, layer=12)
//        <Transform>
//        <UISlicedSprite> atlas=Conversation spriteName="text_button_9p" depth=0
//      [TextLabel] (active=True, layer=12)
//        <Transform>
//        <UILabel> text="{{96D5DA}}[​查看​联系人​清单​。​]{{-}}
//{{96D5DA}..." font=MediumNormal fontSize=36 lineWidth=356 depth=1 pivot=TopLeft pivotOffset=(0.0, 0.0) localPosition=(18.0, -14.0, -1.0)
//    [ConversationResponse(Clone)] (active=True, layer=12)
//      <Transform>
//      <ConversationResponse>
//      <UIDragPanelContents>
//      <BoxCollider> center=(195.0, -33.0, 0.0) size=(390.0, 66.0, 1.0)
//      <BasicButton>
//      <UIButton>
//      <HBSUIButtonSound>
//      [Background] (active=True, layer=12)
//        <Transform>
//        <UISlicedSprite> atlas=Conversation spriteName="text_button_9p" depth=0
//      [TextLabel] (active=True, layer=12)
//        <Transform>
//        <UILabel> text="{{96D5DA}}[​查看​个人​日程表​。​]{{-}}
//{{96D5DA}..." font=MediumNormal fontSize=36 lineWidth=356 depth=1 pivot=TopLeft pivotOffset=(0.0, 0.0) localPosition=(18.0, -14.0, -1.0)
//    [ConversationResponse(Clone)] (active=True, layer=12)
//      <Transform>
//      <ConversationResponse>
//      <UIDragPanelContents>
//      <BoxCollider> center=(195.0, -33.0, 0.0) size=(390.0, 66.0, 1.0)
//      <BasicButton>
//      <UIButton>
//      <HBSUIButtonSound>
//      [Background] (active=True, layer=12)
//        <Transform>
//        <UISlicedSprite> atlas=Conversation spriteName="text_button_9p" depth=0
//      [TextLabel] (active=True, layer=12)
//        <Transform>
//        <UILabel> text="{{96D5DA}}[​放下​你​的​笔记本​。​]{{-}}
//{{96D5DA..." font=MediumNormal fontSize=36 lineWidth=356 depth=1 pivot=TopLeft pivotOffset=(0.0, 0.0) localPosition=(18.0, -14.0, -1.0)
//  [ResponsesDragScrollBar] (active=True, layer=12)
//    <Transform>
//    <UIScrollBar>
//    [Scalar] (active=True, layer=12)
//      <Transform>
//      [Background] (active=False, layer=12)
//        <Transform>
//        <UISlicedSprite> atlas=PDA spriteName="scrollBarFrame" depth=1
//        <BoxCollider> center=(0.0, -0.5, 0.0) size=(1.0, 1.0, 1.0)
//        <UIEventListener>
//      [Transform] (active=True, layer=12)
//        <Transform>
//        [Foreground] (active=False, layer=12)
//          <Transform>
//          <UISlicedSprite> atlas=PDA spriteName="scrollBar" depth=2
//          <BoxCollider> center=(0.0, -0.5, 0.0) size=(1.0, 1.0, 1.0)
//          <UIEventListener>
//  [Scalar] (active=True, layer=0)
//    <Transform>
//    [PortraitFrame] (active=True, layer=12)
//      <Transform>
//      <UISlicedSprite> atlas=Conversation spriteName="portrait_frame_9p" depth=0
//  [Portrait] (active=True, layer=12)
//    <Transform>
//    <UISprite> atlas=SharedPortraitAtlas 0 spriteName="pc_elffemale_00_faceless" depth=-1
//  [NameLabel] (active=True, layer=12)
//    <Transform>
//    <UILabel> text="name" font=MediumBold fontSize=36 lineWidth=390 depth=4 pivot=TopLeft pivotOffset=(0.0, 0.0) localPosition=(40.0, 330.0, -4.0)
//  [ConversationDragContents] (active=True, layer=12)
//    <Transform>
//    <UIDragPanelContents>
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//  [ConversationDragPanel] (active=True, layer=12)
//    <Transform>
//    <UIPanel> clipping=SoftClip
//    <UIDraggablePanel>
//    [TextLabel] (active=True, layer=12)
//      <Transform>
//      <UILabel> text="{{EFD27B}}[你​的​笔记本​-​记录​了​日程表​，​联系人​之类​的..." font=MediumNormal fontSize=36 lineWidth=390 depth=0 pivot=TopLeft pivotOffset=(0.0, 0.0) localPosition=(-195.0, 320.0, 1.0)
//  [ConversationDragScrollBar] (active=True, layer=12)
//    <Transform>
//    <UIScrollBar>
//    [Scalar] (active=True, layer=12)
//      <Transform>
//      [Background] (active=False, layer=12)
//        <Transform>
//        <UISlicedSprite> atlas=PDA spriteName="scrollBarFrame" depth=1
//        <BoxCollider> center=(0.0, -0.5, 0.0) size=(1.0, 1.0, 1.0)
//        <UIEventListener>
//      [Transform] (active=True, layer=12)
//        <Transform>
//        [Foreground] (active=False, layer=12)
//          <Transform>
//          <UISlicedSprite> atlas=PDA spriteName="scrollBar" depth=2
//          <BoxCollider> center=(0.0, -0.5, 0.0) size=(1.0, 1.0, 1.0)
//          <UIEventListener>
