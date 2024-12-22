namespace TranslationToSource.Models.Texts.ControlCodes
{
    internal class JumpControlCodeCharacterData : ControlCodeCharacterData
    {
        public int LabelIndex => Arguments.Length <= 0 ? -1 : Arguments[0];

        public JumpControlCodeCharacterData(byte code) : base(code)
        {
        }

        public JumpControlCodeCharacterData(byte code, int[] args) : base(code, args)
        {
        }
    }
}
