# ChineseLanguageEngage

Monorepo for game mods that add Chinese language-learning support. Each game has its own subdirectory containing a runtime mod and a text preprocessor.

## Structure

| Directory | Purpose |
|-----------|---------|
| `cedict/` | Shared CEDICT dictionary data (used at runtime by all mods) |
| `shadowrun-returns/mod/` | BepInEx plugin for Shadowrun Returns (Unity 4.2.0f4) |
| `shadowrun-returns/preprocessor/` | Python pipeline for Shadowrun Returns `.po` files |

## Shared Dependencies

- **CEDICT** — CC-CEDICT dictionary file, loaded at runtime by each game's mod for word lookup/popup.
- **pinyiniser** — External Python library (separate repo) used by all preprocessors for tokenization, ZWS insertion, and pinyin annotation.

## Per-Game Layout

Each game directory contains:
- `mod/` — The runtime mod (language/framework varies per game). Open the `.sln` or equivalent to work on it.
- `preprocessor/` — Python scripts that pre-process the game's text files (format is game-specific).

## Conventions

- Game directory names use kebab-case (e.g. `shadowrun-returns`, `baldurs-gate-2`).
- Each `mod/` is self-contained with its own solution/project files.
- Preprocessors depend on `pinyiniser` and may reference `cedict/` for dictionary data.
