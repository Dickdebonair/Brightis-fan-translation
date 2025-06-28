using TranslationToSource.Models.Texts;
using TranslationToSource.Models.Texts.ControlCodes;

namespace TranslationToSource.Texts;

internal class TextCalculator
{
    public int CalculateByteLength(IList<CharacterData> parsedText)
    {
        var length = 0;

        foreach (CharacterData character in parsedText)
        {
            switch (character)
            {
                case ControlCodeCharacterData controlCode:
                    switch (controlCode)
                    {
                        case FlagSetControlCodeCharacterData:
                        case FlagJumpControlCodeCharacterData:
                            length += 2 + controlCode.Arguments.Length;
                            break;

                        default:
                            length += 1 + controlCode.Arguments.Length;
                            break;
                    }
                    break;

                case FontCharacterData fontCharacter:
                    length += char.IsAscii(fontCharacter.Character)?1:2;
                    break;
            }
        }

        return length + 1;
    }
}