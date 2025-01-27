using BrightistRenderer.Models.Texts.Parsers;
using BrightistRenderer.Models.Texts.Parsers.ControlCodes;
using BrightistRenderer.Models.UI.Components;

namespace BrightistRenderer.UI.Texts.Parsers
{
    internal class StoryTextParser
    {
        public TextBlock[] ParseTextBlocks(IList<CharacterData> characters)
        {
            var result = new List<TextBlock> { new() };

            result[^1].CharacterLines.Add(new List<CharacterData>());

            foreach (CharacterData character in characters)
            {
                result[^1].CharacterLines[^1].Add(character);

                switch (character)
                {
                    case FullStopControlCodeCharacterData:
                        result[^1].IsFullStop = true;
                        result.Add(new TextBlock());
                        result[^1].CharacterLines.Add(new List<CharacterData>());
                        break;

                    case SoftStopControlCodeCharacterData:
                        result.Add(new TextBlock());
                        result[^1].CharacterLines.Add(new List<CharacterData>());
                        break;

                    case LineBreakControlCodeCharacterData:
                        result[^1].CharacterLines.Add(new List<CharacterData>());
                        break;
                }
            }

            return result.ToArray();
        }

        public IList<CharacterData>? GetCharacters(TextBlock[]? textBlocks, int index, out int lineCount)
        {
            lineCount = 0;

            if (textBlocks == null)
                return null;

            var lines = new List<IList<CharacterData>>();

            int blockIndex = index;
            int lineIndex = textBlocks[blockIndex].CharacterLines.Count - 1;
            while (blockIndex >= 0)
            {
                if (lines.Count >= 3)
                    break;

                if (blockIndex == index)
                    lines.AddRange(textBlocks[index].CharacterLines.Reverse());
                else
                {
                    lines.Add(textBlocks[blockIndex].CharacterLines[lineIndex]);

                    lineIndex--;
                    if (lineIndex >= 0)
                        continue;
                }

                blockIndex--;
                if (blockIndex < 0)
                    break;

                if (textBlocks[blockIndex].IsFullStop)
                    break;

                lineIndex = textBlocks[blockIndex].CharacterLines.Count - 1;
            }

            lines.Reverse();

            lineCount = lines.Count;
            return lines.SelectMany(l => l).ToArray();
        }
    }
}
