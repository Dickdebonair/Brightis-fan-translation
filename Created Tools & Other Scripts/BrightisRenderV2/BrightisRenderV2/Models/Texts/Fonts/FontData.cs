using BrightisRendererV2.Texts.Fonts.Glyphs;

namespace BrightisRendererV2.Models.Texts.Fonts;

internal class FontData
{
    public int MaxHeight { get; set; }
    public ushort FallbackCharacter { get; set; }
    public GlyphProvider Glyphs { get; set; }
}