namespace TranslationToSource.Models.Texts.ControlCodes
{
    internal class ControlCodeCharacterData : CharacterData
    {
        public byte Code { get; }
        public int[] Arguments { get; }

        public override bool IsVisible => false;

        public ControlCodeCharacterData(byte code)
        {
            Code = code;
            Arguments = Array.Empty<int>();
        }

        public ControlCodeCharacterData(byte code, int[] args)
        {
            Code = code;
            Arguments = args;
        }
    }
}
