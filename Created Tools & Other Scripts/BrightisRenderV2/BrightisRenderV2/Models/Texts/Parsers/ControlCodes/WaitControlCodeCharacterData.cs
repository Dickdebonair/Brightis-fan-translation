namespace BrightisRendererV2.Models.Texts.Parsers.ControlCodes;

internal class WaitControlCodeCharacterData : ControlCodeCharacterData
{
    public int FrameCount => Arguments.Length <= 0 ? -1 : Arguments[0];

    public WaitControlCodeCharacterData(byte code) : base(code)
    {
    }

    public WaitControlCodeCharacterData(byte code, int[] args) : base(code, args)
    {
    }
}