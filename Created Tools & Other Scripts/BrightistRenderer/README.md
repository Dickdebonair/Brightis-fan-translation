# BrightistRenderer

a .NET 8.0 self-contained app

This is a tool to edit and preview your translations as they would show in-game *(yes, this also includes text bleeding out of the textboxes, if you typeset your translations like that ^^).*

This tool directly fetches from, and saves back to, the google sheets set up in the accompanying config files.  
Sheets must be set up in a specific format, matching the Excel sheets at the root of this repo.

The BrightistRenderer pairs best with another tool, [TranslationToSource](https://github.com/Dickdebonair/Brightis-fan-translation/tree/325e339ff3da1cc307b1fa0220196643d0a5a71b/Created%20Tools%20%26%20Other%20Scripts/TranslationToSource), which accepts translations desposited into the google sheets, and emits assembly source code ready to be injected into the game.

This tool has been created to simplify testing your work to reflect how it WILL appear in-game, or clarify any uncertainty as to how to format text, it should all be taken care of (hopefully).

## Usage

- Use the arrow buttons atop the rendering to walk through the blocks of text, you created via control codes `4` and `5`.
  - Since control code `4` wipes the current textbox after a button input; lines before it will not be considered "current" and therefore not rendered after that point.
  - Control code `5` advances text normally, without wiping the textbox. after a button input, previous lines will still be rendered, IF they fall into the 3 line-limit.
    - This should indirectly force you to structure the text as it would best be readable in the game.
- It is highly recommended to set up the sheets similar to the one provided at the root of the repo, mainly for ease of translation.
- Pointer information is required, since they are needed for proper reinjection.
  - The sheet will need at least one column to denote the type of text displayed (textbox text, or that popup text like in-game tutorials), which can also be reflected in the tool.
- If 3+ lines are in the current text block (between control codes `4` and `5`), those lines are rendered above the textbox with less visibility. This signals in-game behaviour of pushing out lines from the top of the textbox until the next control code `4` or `5`.
  - It's important to see which lines the user "misses out" on, if they aren't fast at reading and can't read those lines before doing an input to advance the text.

## Setup

1. configure overlays with `overlay_config.json` the same as with "TranslationToSource."  
Unlike [TranslationToSource](https://github.com/Dickdebonair/Brightis-fan-translation/tree/325e339ff3da1cc307b1fa0220196643d0a5a71b/Created%20Tools%20%26%20Other%20Scripts/TranslationToSource), which requires a `sheetId` and `authorization pair`, here you have to provide them via a config.json (different from `overlay_config`) as such:
   - `SheetId`, which is the ID of the google sheet where the translation are located.
   - `ClientId` and `ClientSecret` are an authentication pair that represents you. You must provide that authentication pair yourself for your own user
   - `PlayerName`, which is the player name. It gets rendered when control code `10` is entered.

### Feature: Metric System

The metric system checks various (hardcoded) rules to validate the text for consistency and highlights them.

It provides warnings and errors, but the only distinction between the two is color for now (it's supposed you can ignore warnings, if agreed upon internally, but errors have to be fixed before a patch is made available)

- The rules currently notifies on items such as multiple spaces, and ellipses greater than 3 dots.
- Full text blob are available, so you can fully edit the texts just like in the rest of the app.
- Warning & errors will change in real-time.
- This also supports Popup Texts, Along with standard metrics for text formatting.
- This differentiates from the story metrics in ways such as:
- No leading spaces allowed (since it's not a valid formatting strategy here)
