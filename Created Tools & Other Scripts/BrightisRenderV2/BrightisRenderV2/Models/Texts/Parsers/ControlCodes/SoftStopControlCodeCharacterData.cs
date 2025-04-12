namespace BrightisRendererV2.Models.Texts.Parsers.ControlCodes;

internal class SoftStopControlCodeCharacterData : ControlCodeCharacterData
{
    public SoftStopControlCodeCharacterData(byte code) : base(code)
    {
    }

    public SoftStopControlCodeCharacterData(byte code, int[] args) : base(code, args)
    {
    }
}