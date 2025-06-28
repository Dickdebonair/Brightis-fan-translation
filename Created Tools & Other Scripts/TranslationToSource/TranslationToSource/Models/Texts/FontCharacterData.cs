namespace TranslationToSource.Models.Texts;

internal class FontCharacterData : CharacterData
{
    public required char Character { get; init; }
    public override bool IsVisible => true;
}