using BrightistRenderer.Texts.Fonts.Glyphs;

namespace BrightistRenderer.Models.Texts.Fonts
{
    internal class FontData
    {
        public int MaxHeight { get; set; }
        public ushort FallbackCharacter { get; set; }
        public GlyphProvider Glyphs { get; set; }
    }
}
