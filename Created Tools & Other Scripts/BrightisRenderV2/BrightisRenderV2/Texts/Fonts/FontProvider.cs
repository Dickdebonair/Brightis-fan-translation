using BrightisRendererV2.Models.Texts.Fonts;
using BrightisRendererV2.Texts.Fonts.Glyphs;
using System.Reflection;

namespace BrightisRendererV2.Texts.Fonts;

internal class FontProvider
{
    private static readonly FontData?[] Fonts = new FontData?[3];

    private static BiosFontGlyphStruct[]? _glyphsData;

    public static FontData? GetLargePopupFont()
    {
        if (Fonts[0] != null)
            return Fonts[0]!;

        BiosFontGlyphStruct[]? glyphsData = ReadGlyphsData();
        if (glyphsData == null)
            return null;

        return Fonts[0] = new FontData
        {
            MaxHeight = 16,
            FallbackCharacter = 0x8148,
            Glyphs = new LargePopupGlyphProvider(glyphsData)
        };
    }

    public static FontData? GetSmallPopupFont()
    {
        if (Fonts[1] != null)
            return Fonts[1]!;

        BiosFontGlyphStruct[]? glyphsData = ReadGlyphsData();
        if (glyphsData == null)
            return null;

        return Fonts[1] = new FontData
        {
            MaxHeight = 13,
            FallbackCharacter = 0x8148,
            Glyphs = new SmallPopupGlyphProvider(glyphsData)
        };
    }

    public static FontData? GetSmallStoryFont()
    {
        if (Fonts[2] != null)
            return Fonts[2]!;

        BiosFontGlyphStruct[]? glyphsData = ReadGlyphsData();
        if (glyphsData == null)
            return null;

        return Fonts[2] = new FontData
        {
            MaxHeight = 13,
            FallbackCharacter = 0x8148,
            Glyphs = new SmallStoryGlyphProvider(glyphsData)
        };
    }

    private static BiosFontGlyphStruct[]? ReadGlyphsData()
    {
        if (_glyphsData != null)
            return _glyphsData;

        Stream? rawFont1 = GetFontStream("font1.raw");
        Stream? rawFont2 = GetFontStream("font2.raw");
        if (rawFont1 == null || rawFont2 == null)
            return null;

        return _glyphsData = BiosFontReader.Read(rawFont1, rawFont2);
    }

    private static Stream? GetFontStream(string resourceName)
    {
        return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
    }
}