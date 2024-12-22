namespace TranslationToSource.Models.Texts.ControlCodes
{
    internal class TextColorControlCodeCharacterData : ControlCodeCharacterData
    {
        public TextColorControlCodeCharacterData(byte code) : base(code)
        {
        }

        public TextColorControlCodeCharacterData(byte code, int[] args) : base(code, args)
        {
        }
    }
}
