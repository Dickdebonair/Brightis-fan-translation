namespace BrightisRendererV2.Models.Texts.Parsers;

internal class FontCharacterData : CharacterData
{
    public ushort Character { get; init; }
    public override bool IsVisible => true;
}