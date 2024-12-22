namespace BrightistRenderer.Models.Texts.Parsers.ControlCodes
{
    internal class PlayerNameControlCodeCharacterData : ControlCodeCharacterData
    {
        public IList<CharacterData> PlayerNameCharacters { get; }

        public override bool IsVisible => true;

        public PlayerNameControlCodeCharacterData(IList<CharacterData> playerName, byte code) : base(code)
        {
            PlayerNameCharacters = playerName;
        }

        public PlayerNameControlCodeCharacterData(IList<CharacterData> playerName, byte code, int[] args) : base(code, args)
        {
            PlayerNameCharacters = playerName;
        }
    }
}
