namespace BrightisRendererV2.Texts.Fonts.Glyphs;

internal class VariableWidthProvider
{
    private static readonly int[] VariableWidths = new[]
    {
        12,  7, 12, 12,  3,  3, 12,  7,  7, 10,  8, 12, 12, 12, 12, 12,
        10, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
        12, 11, 12,  7, 12, 12, 12, 12, 12, 12, 11,  6, 12, 12, 11,  6,
        11,  6, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 11, 11, 12, 12,
        12, 12, 11, 12, 10, 10, 12, 12, 12, 12, 12, 12, 12,  5,  7, 12,
        12, 10, 12, 12, 11, 11, 11, 10, 11, 12, 12, 12, 12, 12, 12, 12,

        10,  8, 10, 10, 10, 10, 10, 10, 10, 10,  0,  0,  0,  0,  0,  0,

        0, 11, 10, 10, 10, 10, 10, 11, 10,  7, 10, 10, 10, 11, 10, 11,
        10, 11, 10, 10, 10, 10, 11, 12, 10, 10, 11,  0,  0,  0,  0,  0,

        0,  0, 10, 10, 10, 10, 10,  9, 10, 10,  7,  7, 10,  7, 10, 10,
        10, 10, 10,  9,  9,  9, 10, 10, 11, 10, 10, 10,  0,  0,  0,  0
    };

    private static readonly int[] PreDistance = new[]
    {
        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,

        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,

        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,

        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  2,  0,
        0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
    };

    public static int GetCharacterWidth(ushort code, int defaultWidth)
    {
        int lookupIndex = GetLookupIndex(code);
        return lookupIndex >= 0 ? VariableWidths[lookupIndex] : defaultWidth;
    }

    public static int GetCharacterX(ushort code, int defaultDistance)
    {
        int lookupIndex = GetLookupIndex(code);
        return lookupIndex >= 0 ? PreDistance[lookupIndex] : defaultDistance;
    }

    private static int GetLookupIndex(ushort code)
    {
        if (code < 0x824f)
            return code - 0x813f;

        if (code < 0x829f)
            return code - 0x81ef;

        return -1;
    }
}