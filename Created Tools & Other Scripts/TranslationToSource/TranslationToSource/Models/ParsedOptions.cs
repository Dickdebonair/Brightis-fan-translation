namespace TranslationToSource.Models;

internal class ParsedOptions
{
    public string? SheetId { get; }

    public string? ClientId { get; }

    public string? ClientSecret { get; }

    public bool IsHelp { get; }

    public ParsedOptions(string? sheetId, string? clientId, string? clientSecret, bool isHelp)
    {
        SheetId = sheetId;
        ClientId = clientId;
        ClientSecret = clientSecret;
        IsHelp = isHelp;
    }

    public ParsedOptions(bool isHelp)
    {
        IsHelp = isHelp;
    }
}