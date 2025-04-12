using BrightisRendererV2.Models.Texts.Fonts;
using BrightisRendererV2.Models.Texts.Fonts.Glyphs;
using Komponent.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BrightisRendererV2.Texts.Fonts.Glyphs;

internal class SmallStoryGlyphProvider : GlyphProvider
{
    private const int GlyphWidth_ = 13;
    private const int GlyphHeight_ = 13;

    public SmallStoryGlyphProvider(BiosFontGlyphStruct[] glyphsData) : base(glyphsData)
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
        var iVar13 = 0;
        var y = 0;
        do
        {
            var x = 0;
            var iVar14 = 0;
            do
            {
                var iVar10 = 0;
                var iVar12 = 0;
                do
                {
                    var iVar11 = 0;
                    var uVar4 = iVar13 + iVar12;
                    do
                    {
                        var uVar6 = iVar14 + iVar11;

                        var pixelIndex = uVar6;
                        if (uVar6 < 0)
                            pixelIndex = uVar6 + 3;

                        var uVar8 = 0x8000 >> (pixelIndex / 4 & 0x1f);

                        pixelIndex = uVar4;
                        if (uVar4 < 0)
                            pixelIndex = uVar4 + 3;

                        var glyphPixelValue = data[pixelIndex / 4 * 2 + 0] << 8 |
                                              data[pixelIndex / 4 * 2 + 1];
                        var cVar7 = (glyphPixelValue & uVar8) != 0 ? 1 : 0;

                        if ((uVar6 & 3) < 2 && 3 < uVar6)
                        {
                            glyphPixelValue = data[pixelIndex / 4 * 2 + 0] << 8 |
                                              data[pixelIndex / 4 * 2 + 1];
                            if ((glyphPixelValue & uVar8 << 1) != 0)
                                cVar7++;

                            uVar8 = (uVar8 << 1 & 0xffff) >> 1;
                        }

                        if ((uVar4 & 3) < 2 && 3 < uVar4)
                        {
                            glyphPixelValue = data[(pixelIndex / 4 - 1) * 2 + 0] << 8 |
                                              data[(pixelIndex / 4 - 1) * 2 + 1];
                            if ((glyphPixelValue & uVar8) != 0)
                                cVar7++;

                            glyphPixelValue = data[(pixelIndex / 4 - 1) * 2 + 0] << 8 |
                                              data[(pixelIndex / 4 - 1) * 2 + 1];
                            if ((uVar6 & 3) < 2 && 3 < uVar6 && (glyphPixelValue & uVar8 << 1) != 0)
                                cVar7++;
                        }

                        if (cVar7 != 0)
                            iVar10++;

                        iVar11++;
                    } while (iVar11 < 5);

                    iVar12++;
                } while (iVar12 < 5);

                iVar10 *= 300;
                if (iVar10 < 0)
                    iVar10 += 0xff;

                iVar10 >>= 8;
                if (iVar10 > 0x1f)
                    iVar10 = 0x1f;

                Rgba32 color = Color.Transparent;
                if (iVar10 != 0)
                {
                    var colorValue = (uint)(textColor.R * iVar10 >> 5);
                    colorValue += (uint)(textColor.G * (ushort)iVar10 & 0xFFE0);
                    colorValue += (uint)(textColor.B * iVar10 >> 5 << 10);

                    var r = (byte)Conversion.UpscaleBitDepth((int)(colorValue & 0x1F), 5);
                    var g = (byte)Conversion.UpscaleBitDepth((int)(colorValue >> 5 & 0x1F), 5);
                    var b = (byte)Conversion.UpscaleBitDepth((int)(colorValue >> 10 & 0x1F), 5);

                    color = new Rgba32(r, g, b, 255);
                }

                glyph[x, y] = color;

                x += 1;
                iVar14 += 5;
            } while (x < 0xc);

            y += 1;
            iVar13 += 5;
        } while (y < 0xc);
    }
}