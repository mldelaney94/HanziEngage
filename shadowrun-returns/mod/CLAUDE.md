# ShadowrunReturnsLanguageEngage

BepInEx mod for **Shadowrun Returns** (Unity 4.2.0f4) that adds language-learning support, with a focus on Chinese. The mod hooks into the game's NGUI-based text rendering pipeline to enable per-word interaction, lookup, and annotation.

## Tech Stack

| Component | Version | Purpose |
|-----------|---------|---------|
| BepInEx | 6.0.0-be.754 | Plugin loader (Unity Mono) |
| HarmonyX | 2.10.2 | Runtime method patching |
| Unity | 4.2.0f4 | Game engine |
| .NET Framework | 3.5 | Target runtime |
| Assembly-CSharp.dll | Game | Game types (`UIFont`, `UIPopupList`, `BetterList<T>`) |

## Architecture

### Entry Point

`ShadowrunreturnsLanguageEngage` inherits `BaseUnityPlugin`. In `Awake()` it creates a HarmonyX instance (`"matthewdelaney.ShadowRunReturnsLanguageEngage"`) and calls `PatchAll()`. It exposes a static `Log` property (`ManualLogSource`) for use by patch classes.

### HarmonyX Patches

Patches are `internal static` classes with a `Patch` suffix, organized under `Features/`. They target game types from `Assembly-CSharp.dll`, including private methods via string literals in `[HarmonyPatch]`. The primary hook is `UIFont.Print`, which formats pass-by-reference vertex and color buffers for the caller to render — it does not draw anything itself. Each character produces 4 vertices and 4 corresponding color values.

### Game Types

- `UIFont` — NGUI font renderer. `Print()` populates `BetterList<Vector3>` (vertices) and `BetterList<Color>` (colors) by reference.
- `UIPopupList` — NGUI popup component (future target).
- `BetterList<T>` — NGUI's lightweight generic list. Access `.size` for count.

## Design Intent

The mod aims to make in-game text interactive for language learners:

1. **Word segmentation** — Zero-width spaces (ZWS, U+200B) mark word boundaries in languages like Chinese that lack spaces. These are **already present** in the game's translated `.po` files, inserted by an external Python NLP pipeline as a pre-processing step. The mod does not perform segmentation at runtime; it consumes text that already contains ZWS delimiters.
2. **Vertex hit-testing** — Each character's 4 vertices define a bounding quad; mouse position is tested against these quads to identify which word the user is hovering over.
3. **Word-to-metadata mapping** — A mapping from character index to word/metadata enables lookup, pinyin display, and translation on hover.

## Investigation Findings

### ZWS Does Not Produce Vertices

Confirmed by reading decompiled `UIFont.Print`: ZWS (U+200B) passes the `c < ' '` check (8203 > 32) but `mFont.GetGlyph('\u200B')` returns null, hitting `if (glyph == null) { continue; }`. No vertices emitted. Same applies to spaces (explicit `continue` after advancing cursor), color codes (`{{RRGGBB}}`, `{{-}}` parsed by `NGUITools.ParseSymbol`), escape sequences (`\{{`), and any character missing from the bitmap font.

**Vertex quad index N does NOT correspond to string index N.** An explicit index map (`glyphToStringIndex`) is required.

### Quad Winding Order

From decompiled `UIFont.Print`, each character's 4 vertices are added in this order:
- `[0]` = (right, top), `[1]` = (right, bottom), `[2]` = (left, bottom), `[3]` = (left, top)

Vertices are in the **UILabel's local coordinate space**.

### Color Codes Are Runtime-Injected

`{{EFD27B}}`, `{{-}}` etc. are added by the game's dialogue system, not present in the `.po` files. The NLP pipeline has no visibility into them. Word boundary detection (`IsBoundary`) must handle `{`, `}`, `[`, `]` at runtime since the pipeline cannot insert ZWS around them.

### NGUI Conversation UI Hierarchy

Discovered via runtime `GetComponentsInChildren<UILabel>` traversal:

```
ConversationAnchor(Clone)
├── NameLabel                      ← UILabel (speaker name)
├── ConversationDragContents       ← Has collider, receives raycasts, 0 children
├── ConversationDragPanel
│   └── TextLabel                  ← UILabel (dialogue text)
└── (Frame, ContinueButton, Background, SpeakerFrame, SpeakerPortrait,
     ResponseDragPanel{Top,Bottom}, ResponsesDragContents, ResponsesDragPanel,
     ResponsesDragScrollBar, Scalar, Portrait, ConversationDragScrollBar)
```

Key findings:
- `UICamera.Raycast` uses `Physics.Raycast` — only GameObjects with colliders are hit. TextLabel has no collider; `ConversationDragContents` does.
- `mMouse[0].current.name == "ConversationDragContents"` when hovering over dialogue text.
- `ConversationDragContents` has **0 children** — TextLabel is NOT under it.
- TextLabel is under sibling `ConversationDragPanel`.
- Navigate: `current.transform.parent?.Find("ConversationDragPanel/TextLabel")`

### Coordinate Transform for Hit-Testing

`lastHit.point` (from `UICamera.ProcessMouse` raycast) is in world space. Convert to label-local space via `textLabel.InverseTransformPoint(lastHit.point)` to compare against vertex quads. Do NOT use `Camera.main` (that's the 3D game camera, not the UI camera).

## Build

Requires `libs/Assembly-CSharp.dll` copied from the game's `Managed/` folder. Build with MSBuild or Visual Studio 2022. NuGet restores from nuget.org, nuget.bepinex.dev, and nuget.samboy.dev.

## Conventions

- Namespace: `ShadowrunReturnsLanguageEngage` for all source files.
- Logging: Use `ShadowrunreturnsLanguageEngage.Log` (static `ManualLogSource`) from any patch class.
- Patches: `internal static class`, named `{TargetType}{TargetMethod}Patch`, placed under `Features/{category}/`.

## Next Feature Plan: Word Selection, Highlight, and Dictionary Popup

### Overview

User hovers over text, left-clicks to select/pin a word. Selected word is highlighted in the rendered text. An NGUI popup appears showing the word's definition from CEDICT. Left/Right arrow keys navigate between words. Clicking away deselects.

### 1. Selection State in Globals

Add to `Features/Globals.cs`:
- `selectedWord` (string), `selectedStringStart` / `selectedStringEnd` (int), `isWordSelected` (bool)
- Single source of truth for highlight renderer, popup, and keyboard nav.

### 2. Click to Select / Click Away

In `Features/Graphic/UICamera/UICameraProcessMousePatch.cs`:
- Detect `Input.GetMouseButtonDown(0)` in the existing Postfix.
- If hovering a word (quad hit): set selection state, extract word + string range.
- If clicking but NOT over `ConversationDragContents` (or no quad hit): clear selection.
- The early `return` when `current.name != "ConversationDragContents"` needs to become a conditional that clears selection on click rather than a hard exit.

### 3. Highlight Rendering via Color Buffer

In `Features/Text/UIFontPrintPatch.cs` postfix:
- Add `BetterList<Color> cols` to postfix parameters.
- After building index map, check `Globals.isWordSelected`.
- Find quads matching `selectedStringStart..selectedStringEnd` via the index map.
- Overwrite those quads' 4 color entries in `cols` with a highlight color.
- This works because `UIFont.Print` populates `cols` by reference and the caller (`UILabel.OnFill`) reads the buffer after Print returns.

### 4. CEDICT Dictionary Loader

New file `Features/Dictionary/CEDICTLoader.cs`:
- Parse CEDICT format: `Traditional Simplified [pinyin] /english def 1/english def 2/`
- `Dictionary<string, CEDICTEntry>` keyed by simplified Chinese.
- `CEDICTEntry`: traditional, simplified, pinyin, list of definitions.
- Load once in `Awake()` from a bundled text file (e.g. `libs/cedict.txt`).
- Static lookup: `CEDICTLoader.Lookup(string simplified) -> CEDICTEntry`.

### 5. NGUI Popup Display

New file `Features/Graphic/Popup/WordPopup.cs`:
- Static class managing a single popup GameObject, created lazily.
- Create a `GameObject("WordPopup")`, parent under UI root (e.g. `ConversationAnchor(Clone)` or UICamera root).
- Add a `UILabel` for content (word + pinyin + definition), grabbing `UIFont` reference from existing TextLabel's UILabel component.
- Position using selected word's vertex positions (offset above/below the word).
- `Show(word, entry)` / `Hide()` methods.
- For background: try grabbing a `UISprite` atlas from existing `Frame` or `Background` under `ConversationAnchor(Clone)`. If unavailable, start with just the label and iterate.

### 6. Keyboard Navigation

In `UICameraProcessMousePatch.Postfix` (or separate `UICamera.Update` patch):
- When `Globals.isWordSelected`, listen for `Input.GetKeyDown(KeyCode.LeftArrow)` / `RightArrow`.
- Navigate left: from `selectedStringStart`, scan left past ZWS to find previous word range. Update selection.
- Navigate right: from `selectedStringEnd`, scan right past ZWS to find next word range. Update selection.
- Escape clears selection.

### Build Order

1. Selection state + click to select/deselect (foundation)
2. Highlight rendering (visual feedback that selection works)
3. CEDICT loader (needed before popup shows anything useful)
4. NGUI popup (needs font refs, CEDICT data, selection state)
5. Keyboard navigation (polish, depends on selection state)

### Open Risk

Creating NGUI widgets dynamically (`UILabel`, `UISprite`, `UIPanel`) at runtime requires NGUI API calls (`NGUITools.AddWidget`, `NGUITools.AddChild`, etc.) from `Assembly-CSharp.dll`. Signatures haven't been confirmed via decompilation. Fallback: clone an existing widget's GameObject and modify it.
