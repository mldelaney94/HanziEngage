# ChineseLanguageEngage

A collection of game mods that add Chinese language-learning support. Each mod hooks into a game's text rendering to enable per-word interaction, hover detection, and dictionary lookup — turning gameplay into immersive reading practice.

Text is pre-processed by game-specific Python pipelines that use [pinyiniser](https://github.com/mldelaney94/pinyiniser) to segment Chinese text, insert zero-width spaces at word boundaries, and generate pinyin annotations. At runtime, each mod consumes the pre-processed text and provides interactive features powered by [CC-CEDICT](https://cc-cedict.org/).

## Games

| Game | Mod | Preprocessor | Status |
|------|-----|--------------|--------|
| [Shadowrun Returns](https://store.steampowered.com/app/234650/Shadowrun_Returns/) | [BepInEx plugin](shadowrun-returns/mod/) | [Python pipeline](shadowrun-returns/preprocessor/) | In development |

## Repository Structure

```
├── cedict/                          Shared CC-CEDICT dictionary data
├── shadowrun-returns/
│   ├── mod/                         BepInEx plugin (C#, .NET 3.5)
│   └── preprocessor/                .po file processor (Python)
```

## Getting Started

### Preprocessor

```bash
cd shadowrun-returns/preprocessor
pip install -r requirements.txt
python main.py
```

### Mod

Open `shadowrun-returns/mod/ShadowrunReturnsLanguageEngage.sln` in Visual Studio 2022, or build from CLI:

```bash
cd shadowrun-returns/mod
dotnet restore
dotnet build
```

Requires `libs/Assembly-CSharp.dll` copied from the game's `Managed/` folder.

## Related Projects

- [pinyiniser](https://github.com/mldelaney94/pinyiniser) — Python library for Chinese text segmentation and pinyin generation, used by all preprocessors.
