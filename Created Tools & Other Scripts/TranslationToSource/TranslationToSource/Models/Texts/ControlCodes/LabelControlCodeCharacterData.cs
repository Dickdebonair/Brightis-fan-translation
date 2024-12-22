namespace TranslationToSource.Models.Texts.ControlCodes
{
    internal class LabelControlCodeCharacterData : ControlCodeCharacterData
    {
        public int LabelIndex => Arguments.Length <= 0 ? -1 : Arguments[0];

        public LabelControlCodeCharacterData(byte code) : base(code)
        {
        }

        public LabelControlCodeCharacterData(byte code, int[] args) : base(code, args)
        {
        }
    }
}
