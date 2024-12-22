using BrightistRenderer.Models.Texts.Fonts;
using BrightistRenderer.Models.Texts.Fonts.Glyphs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BrightistRenderer.Texts.Fonts.Glyphs
{
    internal abstract class GlyphProvider
    {
        private readonly Dictionary<ushort, byte[]> _glyphLookup = new();
        private readonly Dictionary<ushort, GlyphData> _glyphCache = new();

        public GlyphProvider(BiosFontGlyphStruct[] glyphsData)
        {
            foreach (BiosFontGlyphStruct glyphData in glyphsData)
            {
                if (_glyphLookup.ContainsKey(glyphData.code))
                    continue;

                _glyphLookup[glyphData.code] = glyphData.data;
            }
        }

        public GlyphData? Get(ushort code, Rgb24 textColor)
        {
            if (_glyphCache.TryGetValue(code, out GlyphData? cachedGlyph))
                return cachedGlyph;

            Image<Rgba32> glyph = CreateEmptyGlyph();
            if (_glyphLookup.TryGetValue(code, out byte[]? glyphData))
                DrawGlyph(glyph, glyphData, textColor);

            if (code is not 0x8140 && glyphData == null)
                return null;

            return _glyphCache[code] = new GlyphData
            {
                CodePoint = code,
                Glyph = glyph,
                CharacterDescription = GetCharacterDescription(code),
                GlyphDescription = GetGlyphDescription(code)
            };
        }

        public CharacterDescriptionData? GetCharacterDescription(ushort code)
        {
            if (_glyphCache.TryGetValue(code, out GlyphData? cachedGlyph))
                return cachedGlyph.CharacterDescription;

            if (!_glyphLookup.ContainsKey(code) && code is not 0x8140)
                return null;

            return CreateCharacterDescription(code);
        }

        public GlyphDescriptionData? GetGlyphDescription(ushort code)
        {
            if (_glyphCache.TryGetValue(code, out GlyphData? cachedGlyph))
                return cachedGlyph.GlyphDescription;

            if (!_glyphLookup.ContainsKey(code) && code is not 0x8140)
                return null;

            return CreateGlyphDescription(code);
        }

        protected abstract CharacterDescriptionData CreateCharacterDescription(ushort code);
        protected abstract GlyphDescriptionData CreateGlyphDescription(ushort code);

        protected abstract Image<Rgba32> CreateEmptyGlyph();

        protected abstract void DrawGlyph(Image<Rgba32> glyph, byte[] data, Rgb24 textColor);
    }
}
