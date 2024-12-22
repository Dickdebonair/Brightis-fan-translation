using BrightistRenderer.Models.Metrics;
using BrightistRenderer.Models.Texts.Layouts;
using BrightistRenderer.Models.Texts.Parsers;

namespace BrightistRenderer.Metrics
{
    internal class InvalidCharacterMetricStrategy : MetricStrategy
    {
        private const ushort Semicolon = 0x8147;

        public override IList<MetricDetailData> Validate(TextLayoutData layout, IList<CharacterData> characters)
        {
            var result = new List<MetricDetailData>();

            foreach (TextLayoutLineData layoutLine in layout.Lines)
            {
                foreach (TextLayoutCharacterData layoutCharacter in layoutLine.Characters)
                {
                    if (!layoutCharacter.Character.IsVisible)
                        continue;

                    ushort character;
                    switch (layoutCharacter.Character)
                    {
                        case FontCharacterData fontCharacter:
                            character = fontCharacter.Character;
                            break;

                        default:
                            continue;
                    }

                    if (IsInvalidCharacter(character))
                        result.Add(CreateMetricDetail(layoutCharacter, MetricDetailType.InvalidCharacter));
                }
            }

            return result;
        }

        private bool IsInvalidCharacter(ushort character)
        {
            // Invalid characters ;
            return character is Semicolon;
        }
    }
}
