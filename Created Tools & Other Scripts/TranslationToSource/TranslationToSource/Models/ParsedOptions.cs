namespace TranslationToSource.Models;

internal class ParsedOptions(bool isHelp)
{
    public bool IsHelp { get; } = isHelp;
}