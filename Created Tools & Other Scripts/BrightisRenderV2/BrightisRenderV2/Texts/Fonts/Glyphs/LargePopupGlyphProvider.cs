using BrightisRendererV2.Models.Texts.Fonts;
using BrightisRendererV2.Models.Texts.Fonts.Glyphs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BrightisRendererV2.Texts.Fonts.Glyphs;

internal class LargePopupGlyphProvider : GlyphProvider
{
    private const int GlyphWidth_ = 16;
    private const int GlyphHeight_ = 16;

    public LargePopupGlyphProvider(BiosFontGlyphStruct[] glyphsData) : base(glyphsData)
    {
    }

    protected override CharacterDescriptionData CreateCharacterDescription(ushort code)
    {
        return new CharacterDescriptionData
        {
            X = VariableWidthProvider.GetCharacterX(code, 0),
            Width = VariableWidthProvider.GetCharacterWidth(code, GlyphWidth_)
        };
    }

    protected override GlyphDescriptionData CreateGlyphDescription(ushort code)
    {
        return new GlyphDescriptionData
        {
            X = 0,
            Y = 0,
            Width = GlyphWidth_,
            Height = GlyphHeight_
        };
    }

    protected override Image<Rgba32> CreateEmptyGlyph()
    {
        return new Image<Rgba32>(GlyphWidth_, GlyphHeight_);
    }

    protected override void DrawGlyph(Image<Rgba32> glyph, byte[] data, Rgb24 textColor)
    {
        var previousColor = new Rgba32(0, 0, 0);
        for (var glyphInnerIndex = 0; glyphInnerIndex < data.Length * 8; glyphInnerIndex++)
        {
            int glyphInnerX = glyphInnerIndex % GlyphWidth_;
            int glyphInnerY = glyphInnerIndex / GlyphHeight_;

            int byteValue = data[glyphInnerIndex / 8];
            int shiftValue = 7 - glyphInnerIndex % 8;
            int bit = byteValue >> shiftValue & 0x1;

            Rgba32 color = Color.Transparent;
            if (bit == 1)
                color = new Rgba32(214, 214, 214, 255); // 0xEB5A
            else if (glyphInnerX != 0 && previousColor.R == 214)
                color = new Rgba32(41, 41, 66, 255); // 0xA0A5

            glyph[glyphInnerX, glyphInnerY] = color;
            previousColor = color;
        }
    }
}