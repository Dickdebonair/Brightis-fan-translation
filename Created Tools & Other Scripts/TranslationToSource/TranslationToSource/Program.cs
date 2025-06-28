using GoogleSheetsApiV4;
using GoogleSheetsApiV4.Contract;
using GoogleSheetsApiV4.Contract.DataClasses;
using TranslationToSource;
using TranslationToSource.Models;
using TranslationToSource.Models.Patchers;
using TranslationToSource.Models.Sheets;
using TranslationToSource.Patchers;
using TranslationToSource.Sheets;

// Parse options
var optionsParser = new OptionsParser();
ParsedOptions options = optionsParser.Parse(args);

if (options.IsHelp)
{
    Console.WriteLine("This tool takes translation from the Brightis Google Spreadsheet and emits them as assembly patches.");
    Console.WriteLine("Use the tool as such:");
    Console.WriteLine("  TranslationToSource.exe -s [sheetId] -ci [clientId] -cs [clientSecret]");
    return;
}

if (options.SheetId == null)
{
    Console.WriteLine("No sheet ID given. Pass one by using the argument '-s'.");
    return;
}
if (options.ClientId == null)
{
    Console.WriteLine("No client ID given. Pass one by using the argument '-ci'.");
    return;
}
if (options.ClientSecret == null)
{
    Console.WriteLine("No client secret given. Pass one by using the argument '-cs'.");
    return;
}

// Access google sheet
IOAuth2TokenStorage tokenStorage = new OAuth2TokenStorage();
ICodeFlowManager codeFlow = OAuth2CodeFlowManager.Create(Scope.Write, options.ClientId, options.ClientSecret, tokenStorage);
ISheetManager sheet = GoogleApiConnector.Instance.CreateSheetManager(options.SheetId, codeFlow);

foreach (OverlayConfigData config in OverlayConfigProvider.GetConfigs())
{
    string? source = null;
    switch (config.OverlayMode)
    {
        case OvrMode.Inline:
            var patcher = new OverlayInlinePatcher();
            source = await patcher.Patch(sheet, config);
            break;

        case OvrMode.Pointer:
            var patcher1 = new OverlayPointerPatcher();
            source = await patcher1.Patch(sheet, config);
            break;

        case OvrMode.PointerExtension:
            var patcher2 = new OverlayPointerExtensionPatcher();
            source = await patcher2.Patch(sheet, config);
            break;
    }

    if (source == null)
        continue;

    if (!Directory.Exists("output"))
        Directory.CreateDirectory("output");

    File.WriteAllText($"output\\brightis_{config.OverlaySlot:000}_translations.asm", source);
}
