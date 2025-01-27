# Brightis - TranslationToSource

This tool takes the translations of configured overlays from a google sheet and converts them to `.asm` files, which are used by armips to patch the game.  
It's a command line tool, that needs a bit of setup and can only be executed one the command line.

## Usage

```text
TranslationToSource.exe -s [sheetId] -ci [clientId] -cs [clientSecret]
```

1. The `sheetId` for this project is `16ST1GpUGnfzQkkyA7Y5LqPaeRHxq0L23jmVaQDX_wBU`.
2. The `clientId` and `clientSecret` are an authentication pair that represents a user, who has reading rights to the google sheet. You have to provide that authentication pair yourself for your own user.
3. Use the redirect uri `http://localhost:6001/` for this authentication pair. They must match. You can set it for the authentication pair somewhere in the google developer console.
4. You also have to set up the `overlay_config.json` to include all the overlays you want to fetch from the google sheet and convert to `.asm` patch files. The tool comes with `OVR_008` and `OVR_041` already set up.

## Configuration

For each overlay, you need to specify:

1. `SheetName` - the name of the table for the overlay on the google sheet
2. `SheetMaxRow` - the last row in the configured sheet table, that has text in it
3. `OverlaySlot` - the number of the overlay, that is currently being patched (ex. `8` for `OVR_008`, `41` for `OVR_041`, and so on)
4. `OverlayLength` - the original size of the overlay in bytes (necessary for space calculations during the creation of the .asm patch file)
5. `OverlayType` - one of the 4 overlay types [0 - Prog; 1 - Sub; 2 - Common/(most will use this); 3 - Cnst] and is used to define base address and max available size for the overlay (also necessary for space calculations)
6. `OverlayMode`, which is the kind of text/pointers used in the overlay (0 - inline into assembly instructions; 1 - Direct pointers; 2 - Direct pointers in separate overlay)
7. `PopupJalOffset`, which is the address to a modified PrintPopupText method in the same overlay, if it needs to be patched and `ONMOVR_002` is not available in memory (optional, since it's very specialized)

Once run, the game will now load text differently: Loading from a separate overlay, into a stack of buffers.  
Since it's not an executable piece of code, it's loaded at one of the 4 overlay base adresses, decompressed into memory into a stack, then referenced.  
This **NEW** overlay is used like an extension of an actual overlay. So, despite being at some potentially random place in memory, it has an explicit pointer table at the end related to the code overlay.

### Output

If successful, TranslationToSource will generate the patches into 2 asm files that will be used in [ARMips](https://github.com/Kingcom/armips).
