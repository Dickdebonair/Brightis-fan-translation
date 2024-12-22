namespace TranslationToSource.Models.Texts.ControlCodes
{
    internal class FlagJumpControlCodeCharacterData : ControlCodeCharacterData
    {
        public int Flag => Arguments.Length <= 0 ? -1 : Arguments[0];
        public int LabelIndex => Arguments.Length <= 1 ? -1 : Arguments[1];

        public FlagJumpControlCodeCharacterData(byte code) : base(code)
        {
        }

        public FlagJumpControlCodeCharacterData(byte code, int[] args) : base(code, args)
        {
        }
    }
}
