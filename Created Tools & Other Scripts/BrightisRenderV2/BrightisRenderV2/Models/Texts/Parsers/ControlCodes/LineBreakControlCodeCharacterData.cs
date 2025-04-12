namespace BrightisRendererV2.Models.Texts.Parsers.ControlCodes;

internal class LineBreakControlCodeCharacterData : ControlCodeCharacterData
{
    public LineBreakControlCodeCharacterData(byte code) : base(code)
    {
    }

    public LineBreakControlCodeCharacterData(byte code, int[] args) : base(code, args)
    {
    }
}