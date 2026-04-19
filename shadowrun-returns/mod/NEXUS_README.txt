# Shadowrun Returns — Language Engage (Chinese)

**BepInEx 6** plugin for **Shadowrun Returns** that adds **Chinese language-learning** support on top of the game’s Chinese localization.

This mod is aimed at learners who play with **Simplified Chinese** text.

---

## What it does

- **Dictionary popup** — When you interact with a Chinese word, the mod will show a popup that displays the CEDict definition for that word.
- **Pinyin string** — strings now have PinYin underneath for easy lookup in a dictionary.


---

## Installation

1. Install **BepInEx 6** for this game according to the BepInEx documentation (Unity Mono / x86 layout as appropriate for Shadowrun Returns).
2. Copy **`ShadowrunReturnsLanguageEngage.dll`** into:
   - `Shadowrun Returns\BepInEx\plugins\`
3. Copy the **`cedict_ts.u8`** dictionary into the **same** `plugins` folder (latest version be acquired here https://www.mdbg.net/chinese/dictionary?page=cedict).
4. To add pinyin, **`zh_deadmanswitch.mo`** maps to the cn localisation
5. Launch the game; check **`BepInEx/LogOutput.log`** if something fails (missing dictionary, wrong BepInEx branch, etc.).

---

## Uninstallation

- Remove **`ShadowrunReturnsLanguageEngage.dll`** from `BepInEx/plugins/`.
- Remove **`cedict_ts.u8`**
- Restore vanilla `.mo` files

---

## Credits / license

- **BepInEx**, **Harmony / HarmonyX** — injection and patching.
- **CEDICT / CC-CEDICT** — dictionary data is **not** bundled here; you supply your own `cedict_ts.u8` under its license.
- Mod source and preprocessor live in the **ChineseLanguageEngage** monorepo (game-specific folder: `shadowrun-returns`).

---

## Github

https://github.com/mldelaney94/HanziEngage/tree/master/shadowrun-returns