namespace TranslationToSource.Models.Texts.ControlCodes;

internal class PlayerNameControlCodeCharacterData : ControlCodeCharacterData
{
    public override bool IsVisible => true;

    public PlayerNameControlCodeCharacterData(byte code) : base(code)
    {
    }

    public PlayerNameControlCodeCharacterData(byte code, int[] args) : base(code, args)
    {
    }
}