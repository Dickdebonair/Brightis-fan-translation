using BrightistRenderer.Models.Texts.Fonts;
using BrightistRenderer.Models.Texts.Fonts.Glyphs;
using Komponent.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BrightistRenderer.Texts.Fonts.Glyphs
{
    internal class SmallPopupGlyphProvider : GlyphProvider
    {
        private const int GlyphWidth_ = 13;
        private const int GlyphHeight_ = 13;

        public SmallPopupGlyphProvider(BiosFontGlyphStruct[] glyphsData) : base(glyphsData)
        {
        }

        protected override CharacterDescriptionData CreateCharacterDescription(ushort code)
        {
            return new CharacterDescriptionData
            {
                X = GetCharacterX(code),
                Width = GetCharacterWidth(code)
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
                        var expandedValue = (byte)Conversion.UpscaleBitDepth(iVar10, 5);
                        color = new Rgba32(expandedValue, expandedValue, expandedValue, 255);
                    }

                    glyph[x, y] = color;

                    x += 1;
                    iVar14 += 5;
                } while (x < 0xc);

                y += 1;
                iVar13 += 5;
            } while (y < 0xc);
        }

        private int GetCharacterWidth(ushort code)
        {
            return GlyphWidth_;
        }

        private int GetCharacterX(ushort code)
        {
            return 0;
        }
    }
}
