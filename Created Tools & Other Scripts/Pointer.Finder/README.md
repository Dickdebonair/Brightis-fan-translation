# Pointer.Finder POC

This C# application is an early proof of concept aimed at automating the process of identifying pointers in text values like `0x1580****`. I’m building on the logic from `onepiecefreak3` to extract `SHIFT-JIS` encoded data from .bin files.

The goal is to simplify and streamline the process, which is why I’m converting hex values into string class counterparts and storing them in a string array. This array mirrors the original data in the stream buffer, making it easier to debug and inspect. It also simplifies the logic for finding a `80` hex value and verifying if a `15` precedes it.

Once a `1580` group is identified, I can easily reference the array indices to reconstruct a string using the two hex values before the `1580`. After finding all the relevant groups and converting them into `0x8015***` values, I can apply the same logic from `onepiecefreak3's` LinQPad scripts to extract the original Japanese text. To facilitate translation, I’m using DeepL's API for a rough translation of these Japanese characters.

## Next Steps: Automation, Automation, Automation!
The next phase is to automate this process as much as possible, reducing the time and effort required to complete a rough translation and recreate the .bin files. This will help advance the project into playtesting and refine the translations.

Additionally, I’m working on creating a Google Sheet that follows the format used by onepiecefreak3's TranslationToSource application. Once the sheet is set up, I can run the application’s logic to automatically generate the assembly code for the PlayStation 1. This assembly code can then be converted back into the .bin files using well-established tools.

## Debugging

This codebase is mainly inteded to run either from VS Code or Studio's debug for now or maybe forever. Who knows. 

However, to run this applicatiion either configure VS Code Workspace to reopen it in a `devcontainer` for the most streamline process. The VS Code will auto install all extensions and the dotnet SDK is not required on the host machine. All code is debugged, compiled, in the docker container. Works on all systems that support docker.

I am lazy and ensure to include the .vscode folder. The launch.json contains all information on debuggin the application. It will require a `local.env` at the root of the project, or the same level as the `.sln` file.

The contents are as follows:
```
deeplenv=THIS_IS_YOUR_DEEPL_API_KEY
```

You just need to get a DeepL api key for the client.