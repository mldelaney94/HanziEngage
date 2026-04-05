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

//=== ConversationAnchor(Clone) @ 12:55:46 ===
//[ConversationAnchor(Clone)] active=True layer=12 pos=(-454.7, 0.0, -30.0) scale=(1.0, 1.0, 1.0)
//  <ConversationAnchor> background="Frame"(UISlicedSprite) portraitFrame="PortraitFrame"(UISlicedSprite) portrait="Portrait"(UISprite) speakerPortraitFrame="SpeakerFrame"(UISlicedSprite) speakerPortrait="SpeakerPortrait"(UISprite) textLabel="TextLabel"(UILabel) nameLabel="NameLabel"(UILabel) continueButton="ContinueButton"(UISprite) continueButtonUI="ContinueButton"(UIButton) textDragPanelContents="ConversationDragContents"(UIDragPanelContents) textDragPanel="ConversationDragPanel"(UIDraggablePanel) textDragContentsPanel="ConversationDragPanel"(UIPanel) textDragScrollBarBG="Background"(UISlicedSprite) textDragScrollBarFG="Foreground"(UISlicedSprite) responseDragPanelContents="ResponsesDragContents"(UIDragPanelContents) responseDragPanel="ResponsesDragPanel"(UIDraggablePanel) responseDragContentsPanel="ResponsesDragPanel"(UIPanel) responseDragScrollBarBG="Background"(UISlicedSprite) responseDragScrollBarFG="Foreground"(UISlicedSprite) responseDragPanelTop="ResponseDragPanelTop"(GameObject) responseDragPanelBottom="ResponseDragPanelBottom"(GameObject) thisTransform="ConversationAnchor(Clone)"(Transform)
//  <UIPanel> clipping=None clipRange=(0.0, 0.0, 286.0, 486.0) showInPanelTool=True generateNormals=False depthPass=False widgetsAreStatic=False cachedTransform="ConversationAnchor(Clone)"(Transform) changedLastFrame=False debugInfo=Gizmos clipping=None clipRange=(0.0, 0.0, 286.0, 486.0) clipSoftness=(40.0, 40.0) widgets=System.Collections.Generic.List`1[UIWidget] drawCalls=System.Collections.Generic.List`1[UIDrawCall]
//  <iTween> id="rNCt6WUCRUe45u2" type="move" method="to" easeType=easeOutExpo time=1 delay=0 loopType=none isRunning=True isPaused=False _name=null
//  [Frame] active=True layer=12 pos=(-4.0, 374.0, 0.0) scale=(460.0, 750.0, 1.0)
//    <UISlicedSprite> pdaAtlas=Conversation spriteName="conversation_frame_9p" depth=-1 color=RGBA(1.000, 1.000, 1.000, 1.000)
//      sprites(5)=[continue_button, conversation_frame_9p, portrait_frame_9p, speakerPortraitFrame, text_button_9p]
//  [ContinueButton] active=True layer=12 pos=(400.0, 54.0, -4.0) scale=(80.0, 42.0, 1.0)
//    <UISprite> pdaAtlas=Conversation spriteName="continue_button" depth=0 color=RGBA(0.922, 0.792, 0.525, 0.500)
//      sprites(5)=[continue_button, conversation_frame_9p, portrait_frame_9p, speakerPortraitFrame, text_button_9p]
//    <BasicButton> target="ConversationAnchor(Clone)"(GameObject) onClick=True onDoubleClick=False onHover=False onPress=False onDrag=False dragHorizontal=False dragThreshold=10 buttonName="continue" icon=null label=null butId=0
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//    <UIButton> disabledColor=RGBA(0.500, 0.500, 0.500, 1.000) forcedEnabled=False isEnabled=True
//    <HBSUIButtonSound> audioClip="Beep40" trigger=OnClick volume=1 pitch=1
//  [Background] active=True layer=12 pos=(0.0, 0.0, 0.0) scale=(7680.0, 4292.0, 1.0)
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//    <AutoScaleToScreen>
//  [SpeakerFrame] active=True layer=12 pos=(-188.0, 30.0, 0.0) scale=(189.0, 211.0, 1.0)
//    <UISlicedSprite> pdaAtlas=Conversation spriteName="speakerPortraitFrame" depth=-1 color=RGBA(1.000, 1.000, 1.000, 1.000)
//      sprites(5)=[continue_button, conversation_frame_9p, portrait_frame_9p, speakerPortraitFrame, text_button_9p]
//  [SpeakerPortrait] active=True layer=12 pos=(-74.0, -77.0, 10.0) scale=(170.0, 170.0, 1.0)
//    <UISprite> pdaAtlas= spriteName="Generic_Static_Blank" depth=-1 color=RGBA(1.000, 1.000, 1.000, 1.000)
//  [ResponseDragPanelTop] active=True layer=12 pos=(234.0, 415.0, -3.0) scale=(394.0, 768.0, 1.0)
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//  [ResponseDragPanelBottom] active=True layer=12 pos=(234.0, -735.0, -3.0) scale=(394.0, 768.0, 1.0)
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//  [ResponsesDragContents] active=True layer=12 pos=(234.0, -160.0, -1.0) scale=(394.0, 380.0, 1.0)
//    <UIDragPanelContents> draggablePanel="ResponsesDragPanel"(UIDraggablePanel)
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//  [ResponsesDragPanel] active=True layer=12 pos=(234.0, 25.0, -2.0) scale=(1.0, 1.0, 1.0)
//    <UIPanel> clipping=SoftClip clipRange=(2.0, -185.0, 394.0, 380.0) showInPanelTool=True generateNormals=False depthPass=False widgetsAreStatic=False cachedTransform="ResponsesDragPanel"(Transform) changedLastFrame=False debugInfo=Gizmos clipping=SoftClip clipRange=(2.0, -185.0, 394.0, 380.0) clipSoftness=(1.0, 5.0) widgets=System.Collections.Generic.List`1[UIWidget] drawCalls=System.Collections.Generic.List`1[UIDrawCall]
//    <UIDraggablePanel> restrictWithinPanel=True disableDragIfFits=True dragEffect=Momentum scale=(0.0, 1.0, 0.0) scrollWheelFactor=1.5 momentumAmount=35 relativePositionOnReset=(0.0, -311.0) repositionClipping=False horizontalScrollBar=null verticalScrollBar="ResponsesDragScrollBar"(UIScrollBar) showScrollBars=OnlyIfNeeded bounds=Center: (1.0, -99.0, -1.5), Extents: (195.0, 99.0, 0.5) shouldMoveHorizontally=False shouldMoveVertically=False currentMomentum=(0.0, 0.0, 0.0)
//    [ConversationResponse(Clone)] active=True layer=12 pos=(-194.0, 0.0, -1.0) scale=(1.0, 1.0, 1.0)
//      <ConversationResponse> background="Background"(UISlicedSprite) buttonUI="ConversationResponse(Clone)"(UIButton) text="TextLabel"(UILabel) index=0 isEnabled=True height=66 boxCollider="ConversationResponse(Clone)"(BoxCollider) thisTransform="ConversationResponse(Clone)"(Transform)
//      <UIDragPanelContents> draggablePanel="ResponsesDragPanel"(UIDraggablePanel)
//      <BoxCollider> center=(195.0, -33.0, 0.0) size=(390.0, 66.0, 1.0)
//      <BasicButton> target="ConversationResponse(Clone)"(GameObject) onClick=True onDoubleClick=False onHover=False onPress=False onDrag=False dragHorizontal=False dragThreshold=10 buttonName="button" icon=null label=null butId=0
//      <UIButton> disabledColor=RGBA(0.745, 0.745, 0.745, 1.000) forcedEnabled=False isEnabled=True
//      <HBSUIButtonSound> audioClip="ClickGeneric01" trigger=OnClick volume=1 pitch=1
//      [Background] active=True layer=12 pos=(0.0, 0.0, 0.0) scale=(390.0, 66.0, 1.0)
//        <UISlicedSprite> pdaAtlas=Conversation spriteName="text_button_9p" depth=0 color=RGBA(0.114, 0.816, 0.871, 1.000)
//          sprites(5)=[continue_button, conversation_frame_9p, portrait_frame_9p, speakerPortraitFrame, text_button_9p]
//      [TextLabel] active=True layer=12 pos=(18.0, -14.0, -1.0) scale=(18.0, 18.0, 1.0)
//        <UILabel> text="{{96D5DA}}[​查看​联系人​清单​。​]{{-}}
//{{96D5DA}}[ cha2kan4 lian2xi4..." font=MediumNormal fontSize=36 lineWidth=356 depth=1 pivot=TopLeft color=RGBA(0.302, 0.753, 0.788, 1.000)
//    [ConversationResponse(Clone)] active=True layer=12 pos=(-194.0, -66.0, -1.0) scale=(1.0, 1.0, 1.0)
//      <ConversationResponse> background="Background"(UISlicedSprite) buttonUI="ConversationResponse(Clone)"(UIButton) text="TextLabel"(UILabel) index=1 isEnabled=True height=66 boxCollider="ConversationResponse(Clone)"(BoxCollider) thisTransform="ConversationResponse(Clone)"(Transform)
//      <UIDragPanelContents> draggablePanel="ResponsesDragPanel"(UIDraggablePanel)
//      <BoxCollider> center=(195.0, -33.0, 0.0) size=(390.0, 66.0, 1.0)
//      <BasicButton> target="ConversationResponse(Clone)"(GameObject) onClick=True onDoubleClick=False onHover=False onPress=False onDrag=False dragHorizontal=False dragThreshold=10 buttonName="button" icon=null label=null butId=0
//      <UIButton> disabledColor=RGBA(0.745, 0.745, 0.745, 1.000) forcedEnabled=False isEnabled=True
//      <HBSUIButtonSound> audioClip="ClickGeneric01" trigger=OnClick volume=1 pitch=1
//      [Background] active=True layer=12 pos=(0.0, 0.0, 0.0) scale=(390.0, 66.0, 1.0)
//        <UISlicedSprite> pdaAtlas=Conversation spriteName="text_button_9p" depth=0 color=RGBA(0.114, 0.816, 0.871, 1.000)
//          sprites(5)=[continue_button, conversation_frame_9p, portrait_frame_9p, speakerPortraitFrame, text_button_9p]
//      [TextLabel] active=True layer=12 pos=(18.0, -14.0, -1.0) scale=(18.0, 18.0, 1.0)
//        <UILabel> text="{{96D5DA}}[​查看​个人​日程表​。​]{{-}}
//{{96D5DA}}[ cha2kan4 ge4ren2 ..." font=MediumNormal fontSize=36 lineWidth=356 depth=1 pivot=TopLeft color=RGBA(0.302, 0.753, 0.788, 1.000)
//    [ConversationResponse(Clone)] active=True layer=12 pos=(-194.0, -132.0, -1.0) scale=(1.0, 1.0, 1.0)
//      <ConversationResponse> background="Background"(UISlicedSprite) buttonUI="ConversationResponse(Clone)"(UIButton) text="TextLabel"(UILabel) index=2 isEnabled=True height=66 boxCollider="ConversationResponse(Clone)"(BoxCollider) thisTransform="ConversationResponse(Clone)"(Transform)
//      <UIDragPanelContents> draggablePanel="ResponsesDragPanel"(UIDraggablePanel)
//      <BoxCollider> center=(195.0, -33.0, 0.0) size=(390.0, 66.0, 1.0)
//      <BasicButton> target="ConversationResponse(Clone)"(GameObject) onClick=True onDoubleClick=False onHover=False onPress=False onDrag=False dragHorizontal=False dragThreshold=10 buttonName="button" icon=null label=null butId=0
//      <UIButton> disabledColor=RGBA(0.745, 0.745, 0.745, 1.000) forcedEnabled=False isEnabled=True
//      <HBSUIButtonSound> audioClip="ClickGeneric01" trigger=OnClick volume=1 pitch=1
//      [Background] active=True layer=12 pos=(0.0, 0.0, 0.0) scale=(390.0, 66.0, 1.0)
//        <UISlicedSprite> pdaAtlas=Conversation spriteName="text_button_9p" depth=0 color=RGBA(0.114, 0.816, 0.871, 1.000)
//          sprites(5)=[continue_button, conversation_frame_9p, portrait_frame_9p, speakerPortraitFrame, text_button_9p]
//      [TextLabel] active=True layer=12 pos=(18.0, -14.0, -1.0) scale=(18.0, 18.0, 1.0)
//        <UILabel> text="{{96D5DA}}[​放下​你​的​笔记本​。​]{{-}}
//{{96D5DA}}[ fang4xia4 ni3 de..." font=MediumNormal fontSize=36 lineWidth=356 depth=1 pivot=TopLeft color=RGBA(0.302, 0.753, 0.788, 1.000)
//  [ResponsesDragScrollBar] active=True layer=12 pos=(442.0, 24.0, -3.0) scale=(1.0, 1.0, 1.0)
//    <UIScrollBar> onChange=<delegate(1):UIDraggablePanel.OnVerticalBar> cachedTransform="ResponsesDragScrollBar"(Transform) cachedCamera="Camera"(Camera) background="Background"(UISlicedSprite) foreground="Foreground"(UISlicedSprite) direction=Vertical inverted=False scrollValue=0 barSize=1 alpha=0
//    [Scalar] active=True layer=12 pos=(0.0, 0.0, 0.0) scale=(0.5, 0.5, 1.0)
//      [Background] active=False layer=12 pos=(0.0, 0.0, 0.0) scale=(28.0, 742.0, 1.0)
//        <UISlicedSprite> pdaAtlas=PDA spriteName="scrollBarFrame" depth=1 color=RGBA(0.114, 0.816, 0.871, 0.000)
//          sprites(55)=[add_Button, arrow_name, bracket, cart_Bar, centerCameraButton, check_box, compass_needle, compass_overlay, cyberLink, icon_character, icon_frame, icon_gear, icon_menu, icon_objectives, iconFrameWithNotification, itemBG, moneySlot, objectiveCompleteIcon, objectiveFailedIcon, pda_button, pda_buttonBG, pda_help_button, pda_help_button_tempcover, primary_obj-icon, removeTab, right_handle, rosterBG, runnerBG, scrollArrow, scrollBar, ...+25 more]
//        <BoxCollider> center=(0.0, -0.5, 0.0) size=(1.0, 1.0, 1.0)
//        <UIEventListener> parameter=null onSubmit=null onClick=null onDoubleClick=null onHover=null onPress=<delegate(1):UIScrollBar.OnPressBackground> onSelect=null onScroll=null onDrag=<delegate(1):UIScrollBar.OnDragBackground> onDrop=null onInput=null
//      [Transform] active=True layer=12 pos=(1.0, 0.0, 0.0) scale=(1.0, 1.0, 1.0)
//        [Foreground] active=False layer=12 pos=(0.0, 2.0, 0.0) scale=(32.0, 746.0, 1.0)
//          <UISlicedSprite> pdaAtlas=PDA spriteName="scrollBar" depth=2 color=RGBA(0.114, 0.816, 0.871, 0.000)
//            sprites(55)=[add_Button, arrow_name, bracket, cart_Bar, centerCameraButton, check_box, compass_needle, compass_overlay, cyberLink, icon_character, icon_frame, icon_gear, icon_menu, icon_objectives, iconFrameWithNotification, itemBG, moneySlot, objectiveCompleteIcon, objectiveFailedIcon, pda_button, pda_buttonBG, pda_help_button, pda_help_button_tempcover, primary_obj-icon, removeTab, right_handle, rosterBG, runnerBG, scrollArrow, scrollBar, ...+25 more]
//          <BoxCollider> center=(0.0, -0.5, 0.0) size=(1.0, 1.0, 1.0)
//          <UIEventListener> parameter=null onSubmit=null onClick=null onDoubleClick=null onHover=null onPress=<delegate(1):UIScrollBar.OnPressForeground> onSelect=null onScroll=null onDrag=<delegate(1):UIScrollBar.OnDragForeground> onDrop=null onInput=null
//  [Scalar] active=True layer=0 pos=(0.0, 0.0, 0.0) scale=(0.5, 0.5, 1.0)
//    [PortraitFrame] active=True layer=12 pos=(-500.0, 700.0, -1.0) scale=(520.0, 644.0, 1.0)
//      <UISlicedSprite> pdaAtlas=Conversation spriteName="portrait_frame_9p" depth=0 color=RGBA(0.588, 0.835, 0.855, 1.000)
//        sprites(5)=[continue_button, conversation_frame_9p, portrait_frame_9p, speakerPortraitFrame, text_button_9p]
//  [Portrait] active=True layer=12 pos=(-120.0, 188.0, 0.0) scale=(212.0, 278.0, 1.0)
//    <UISprite> pdaAtlas=SharedPortraitAtlas 0 spriteName="pc_elffemale_00_faceless" depth=-1 color=RGBA(1.000, 1.000, 1.000, 1.000)
//      sprites(14)=[backer_humanmale_johnholmes, backer_humanmale_johnholmes thumbnail, backer_humanmale_robertorivera_diamondwire, backer_humanmale_robertorivera_diamondwire thumbnail, generic_static_blank, generic_static_blank thumbnail, npc_humanmale_samwatts, npc_humanmale_samwatts thumbnail, npc_humanmale_samwattspresent, npc_humanmale_samwattspresent thumbnail, npc_orkfemale_sangoma, npc_orkfemale_sangoma thumbnail, pc_elffemale_00_faceless, pc_elffemale_00_faceless thumbnail]
//  [NameLabel] active=True layer=12 pos=(40.0, 330.0, -4.0) scale=(18.0, 18.0, 1.0)
//    <UILabel> text="name" font=MediumBold fontSize=36 lineWidth=390 depth=4 pivot=TopLeft color=RGBA(0.922, 0.663, 0.129, 1.000)
//  [ConversationDragContents] active=True layer=12 pos=(234.0, 194.0, -4.0) scale=(394.0, 228.0, 1.0)
//    <UIDragPanelContents> draggablePanel="ConversationDragPanel"(UIDraggablePanel)
//    <BoxCollider> center=(0.0, 0.0, 0.0) size=(1.0, 1.0, 1.0)
//  [ConversationDragPanel] active=True layer=12 pos=(234.0, -20.0, -5.0) scale=(1.0, 1.0, 1.0)
//    <UIPanel> clipping=SoftClip clipRange=(1.0, 211.0, 394.0, 228.0) showInPanelTool=True generateNormals=False depthPass=False widgetsAreStatic=False cachedTransform="ConversationDragPanel"(Transform) changedLastFrame=False debugInfo=Gizmos clipping=SoftClip clipRange=(1.0, 211.0, 394.0, 228.0) clipSoftness=(1.0, 5.0) widgets=System.Collections.Generic.List`1[UIWidget] drawCalls=System.Collections.Generic.List`1[UIDrawCall]
//    <UIDraggablePanel> restrictWithinPanel=True disableDragIfFits=True dragEffect=Momentum scale=(0.0, 1.0, 0.0) scrollWheelFactor=1.5 momentumAmount=35 relativePositionOnReset=(0.0, -311.0) repositionClipping=False horizontalScrollBar=null verticalScrollBar="ConversationDragScrollBar"(UIScrollBar) showScrollBars=OnlyIfNeeded bounds=Center: (0.0, 275.0, 1.0), Extents: (195.0, 45.0, 0.0) shouldMoveHorizontally=False shouldMoveVertically=False currentMomentum=(0.0, 0.0, 0.0)
//    [TextLabel] active=True layer=12 pos=(-195.0, 320.0, 1.0) scale=(18.0, 18.0, 1.0)
//      <UILabel> text="{{EFD27B}}[你​的​笔记本​-​记录​了​日程表​，​联系人​之类​的​东西​。]{{-}}

//{{EFD27..." font=MediumNormal fontSize=36 lineWidth=390 depth=0 pivot=TopLeft color=RGBA(0.800, 0.675, 0.294, 1.000)
//  [ConversationDragScrollBar] active=True layer=12 pos=(442.0, 304.0, -3.0) scale=(1.0, 1.0, 1.0)
//    <UIScrollBar> onChange=<delegate(1):UIDraggablePanel.OnVerticalBar> cachedTransform="ConversationDragScrollBar"(Transform) cachedCamera="Camera"(Camera) background="Background"(UISlicedSprite) foreground="Foreground"(UISlicedSprite) direction=Vertical inverted=False scrollValue=0 barSize=1 alpha=0
//    [Scalar] active=True layer=12 pos=(0.0, 0.0, 0.0) scale=(0.5, 0.5, 1.0)
//      [Background] active=False layer=12 pos=(0.0, 0.0, 0.0) scale=(28.0, 454.0, 1.0)
//        <UISlicedSprite> pdaAtlas=PDA spriteName="scrollBarFrame" depth=1 color=RGBA(0.114, 0.816, 0.871, 0.000)
//          sprites(55)=[add_Button, arrow_name, bracket, cart_Bar, centerCameraButton, check_box, compass_needle, compass_overlay, cyberLink, icon_character, icon_frame, icon_gear, icon_menu, icon_objectives, iconFrameWithNotification, itemBG, moneySlot, objectiveCompleteIcon, objectiveFailedIcon, pda_button, pda_buttonBG, pda_help_button, pda_help_button_tempcover, primary_obj-icon, removeTab, right_handle, rosterBG, runnerBG, scrollArrow, scrollBar, ...+25 more]
//        <BoxCollider> center=(0.0, -0.5, 0.0) size=(1.0, 1.0, 1.0)
//        <UIEventListener> parameter=null onSubmit=null onClick=null onDoubleClick=null onHover=null onPress=<delegate(1):UIScrollBar.OnPressBackground> onSelect=null onScroll=null onDrag=<delegate(1):UIScrollBar.OnDragBackground> onDrop=null onInput=null
//      [Transform] active=True layer=12 pos=(1.0, 0.0, 0.0) scale=(1.0, 1.0, 1.0)
//        [Foreground] active=False layer=12 pos=(0.0, 2.0, 0.0) scale=(32.0, 458.0, 1.0)
//          <UISlicedSprite> pdaAtlas=PDA spriteName="scrollBar" depth=2 color=RGBA(0.114, 0.816, 0.871, 0.000)
//            sprites(55)=[add_Button, arrow_name, bracket, cart_Bar, centerCameraButton, check_box, compass_needle, compass_overlay, cyberLink, icon_character, icon_frame, icon_gear, icon_menu, icon_objectives, iconFrameWithNotification, itemBG, moneySlot, objectiveCompleteIcon, objectiveFailedIcon, pda_button, pda_buttonBG, pda_help_button, pda_help_button_tempcover, primary_obj-icon, removeTab, right_handle, rosterBG, runnerBG, scrollArrow, scrollBar, ...+25 more]
//          <BoxCollider> center=(0.0, -0.5, 0.0) size=(1.0, 1.0, 1.0)
//          <UIEventListener> parameter=null onSubmit=null onClick=null onDoubleClick=null onHover=null onPress=<delegate(1):UIScrollBar.OnPressForeground> onSelect=null onScroll=null onDrag=<delegate(1):UIScrollBar.OnDragForeground> onDrop=null onInput=null

