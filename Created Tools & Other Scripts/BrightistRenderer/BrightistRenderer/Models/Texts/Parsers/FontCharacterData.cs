namespace BrightistRenderer.Models.Texts.Parsers
{
    internal class FontCharacterData : CharacterData
    {
        public required ushort Character { get; init; }
        public override bool IsVisible => true;
    }
}
