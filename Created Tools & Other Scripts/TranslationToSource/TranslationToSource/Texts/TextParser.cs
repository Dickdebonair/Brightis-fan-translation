using TranslationToSource.Models.Texts;
using TranslationToSource.Models.Texts.ControlCodes;

namespace TranslationToSource.Texts
{
    internal class TextParser
    {
        public IList<CharacterData> Parse(string text)
        {
            var result = new List<CharacterData>();

            var position = 0;
            while (position < text.Length)
            {
                CharacterData? character = GetCharacter(text, position, out int length);
                if (character != null)
                    result.Add(character);

                position += length;
            }

            return result;
        }

        private CharacterData? GetCharacter(string text, int position, out int length)
        {
            if (IsControlCode(text, position, out length))
                return GetControlCode(text, position, length);

            return GetFontCharacter(text, position, out length);
        }

        private bool IsControlCode(string text, int position, out int length)
        {
            length = 2;

            if (text[position] != '<')
                return false;

            int endIndex = text.IndexOf('>', position);
            if (endIndex < 0)
                return false;

            if (text.IndexOf('<', position + 1, endIndex - position - 1) >= 0)
                return false;

            length = endIndex - position + 1;
            return true;
        }

        private ControlCodeCharacterData? GetControlCode(string text, int position, int length)
        {
            if (length <= 2)
                return null;

            int tagStart = position + 1;
            int tagEnd = position + length - 1;

            int colonIndex = text.IndexOf(':', position, length);
            int codeEnd = colonIndex < 0 || colonIndex >= tagEnd ? tagEnd : colonIndex;
            if (!byte.TryParse(text[tagStart..codeEnd], out byte code))
                return null;

            if (colonIndex < 0 || colonIndex >= tagEnd)
                return CreateControlCode(code);

            int argsStart = colonIndex + 1;
            if (argsStart >= tagEnd)
                return null;

            string[] splitArgs = text[argsStart..tagEnd].Split(',');

            var args = new int[splitArgs.Length];
            for (var i = 0; i < args.Length; i++)
            {
                if (!int.TryParse(splitArgs[i], out int intArg))
                    return null;

                args[i] = intArg;
            }

            return CreateControlCode(code, args);
        }

        private ControlCodeCharacterData CreateControlCode(byte code, int[]? args = null)
        {
            switch (code)
            {
                case 4:
                    return args == null ?
                        new FullStopControlCodeCharacterData(code) :
                        new FullStopControlCodeCharacterData(code, args);

                case 5:
                    return args == null ?
                        new SoftStopControlCodeCharacterData(code) :
                        new SoftStopControlCodeCharacterData(code, args);

                case 7:
                    return args == null ?
                        new LineBreakControlCodeCharacterData(code) :
                        new LineBreakControlCodeCharacterData(code, args);

                case 10:
                    return args == null ?
                        new PlayerNameControlCodeCharacterData(code) :
                        new PlayerNameControlCodeCharacterData(code, args);

                case 11:
                    return args == null ?
                        new WaitControlCodeCharacterData(code) :
                        new WaitControlCodeCharacterData(code, args);

                case 13:
                    return args == null ?
                        new TextColorControlCodeCharacterData(code) :
                        new TextColorControlCodeCharacterData(code, args);

                case 14:
                    return args == null ?
                        new JumpControlCodeCharacterData(code) :
                        new JumpControlCodeCharacterData(code, args);

                case 16:
                    return args == null ?
                        new FlagJumpControlCodeCharacterData(code) :
                        new FlagJumpControlCodeCharacterData(code, args);

                case 17:
                    return args == null ?
                        new LabelControlCodeCharacterData(code) :
                        new LabelControlCodeCharacterData(code, args);

                case 19:
                    return args == null ?
                        new FlagSetControlCodeCharacterData(code) :
                        new FlagSetControlCodeCharacterData(code, args);

                default:
                    return args == null ?
                        new ControlCodeCharacterData(code) :
                        new ControlCodeCharacterData(code, args);
            }
        }

        private FontCharacterData? GetFontCharacter(string text, int position, out int length)
        {
            length = 1;

            if (text[position] < 0x20)
                return null;

            return new FontCharacterData
            {
                Character = text[position]
            };
        }
    }
}
