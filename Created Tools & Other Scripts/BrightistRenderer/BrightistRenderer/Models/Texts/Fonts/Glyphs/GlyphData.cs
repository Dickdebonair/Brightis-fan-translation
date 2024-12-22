using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BrightistRenderer.Models.Texts.Fonts.Glyphs
{
    internal class GlyphData
    {
        public ushort CodePoint { get; set; }
        public Image<Rgba32> Glyph { get; set; }
        public CharacterDescriptionData? CharacterDescription { get; set; }
        public GlyphDescriptionData? GlyphDescription { get; set; }
    }
}
