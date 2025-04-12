using BrightisRendererV2.Models.Texts.Fonts;

namespace BrightisRendererV2.Texts.Fonts;

/* Taken from PCSX Redux:
 * https://github.com/grumpycoders/pcsx-redux/blob/f807850426b53c9e3c7878aafbde6cc0f3f113bf/src/mips/openbios/charset/sjis.c
 *
 * Adjusted to return the data as a byte[] per glyph instead.
 * Adjusted the 'new font table' to be ordered by offset instead of code point.
 */

internal class BiosFontReader
{
    private static readonly (ushort, ushort)[] GlyphRanges =
    {
        (0x8140, 0x0000), (0x8180, 0x003f), (0x81b8, 0x006c), (0x81c8, 0x0074), (0x81da, 0x007b), (0x81f0, 0x008a),
        (0x81fc, 0x0092), (0x824f, 0x0093), (0x8260, 0x009d), (0x8281, 0x00b7), (0x829f, 0x00d1), (0x8340, 0x0124),
        (0x8380, 0x0163), (0x839f, 0x017a), (0x83bf, 0x0192), (0x8440, 0x01aa), (0x8470, 0x01cb), (0x8480, 0x01da),
        (0x849f, 0x01ec),

        (0x889f, 0x0000), (0x8940, 0x005e), (0x899f, 0x00bc), (0x8a40, 0x011a), (0x8a9f, 0x0178), (0x8b40, 0x01d6),
        (0x8b9f, 0x0234), (0x8c40, 0x0292), (0x8c9f, 0x02f0), (0x8d40, 0x034e), (0x8d9f, 0x03ac), (0x8e40, 0x040a),
        (0x8e9f, 0x0468), (0x8f40, 0x04c6), (0x8f9f, 0x0524), (0x9040, 0x0582), (0x909f, 0x05e0), (0x9140, 0x063e),
        (0x919f, 0x069c), (0x9240, 0x06fa), (0x929f, 0x0758), (0x9340, 0x07b6), (0x939f, 0x0814), (0x9440, 0x0872),
        (0x949f, 0x08d0), (0x9540, 0x092e), (0x959f, 0x098c), (0x9640, 0x09ea), (0x969f, 0x0a48), (0x9740, 0x0aa6),
        (0x979f, 0x0b04), (0x9840, 0x0b62),

        (0xffff, 0x0000)
    };

    public static BiosFontGlyphStruct[] Read(Stream rawFont1, Stream rawFont2)
    {
        var result = new List<BiosFontGlyphStruct>();

        for (var i = 0; i < GlyphRanges.Length - 1; i++)
        {
            Stream rawFontStream = i < 36 ? rawFont1 : rawFont2;

            (ushort codePoint, ushort offset) = GlyphRanges[i];
            ushort nextOffset = i switch
            {
                18 => (ushort)(rawFont1.Length / 0x1E - offset),
                50 => (ushort)(rawFont2.Length / 0x1E - offset),
                _ => GlyphRanges[i + 1].Item2
            };

            for (var j = 0; j < nextOffset - offset; j++)
            {
                var glyphData = new byte[0x1E];

                rawFontStream.Position = (offset + j) * 0x1E;
                _ = rawFontStream.Read(glyphData);

                int weight = glyphData.Sum(b => b);
                if (weight <= 0)
                    continue;

                var currentCodePoint = (ushort)(codePoint + j);

                result.Add(new BiosFontGlyphStruct
                {
                    code = currentCodePoint,
                    data = glyphData
                });
            }
        }

        return result.ToArray();
    }
}