using System.Diagnostics.CodeAnalysis;
using BrightistRenderer.Models.Metrics;
using BrightistRenderer.Models.Texts.Layouts;
using BrightistRenderer.Models.Texts.Parsers;

namespace BrightistRenderer.Metrics
{
    internal abstract class MetricStrategy
    {
        public abstract IList<MetricDetailData> Validate(TextLayoutData layout, IList<CharacterData> characters);

        protected bool TryGetCharacter(TextLayoutLineData line, int index, out ushort result)
        {
            result = ushort.MaxValue;

            if (!TryGetCharacterData(line, index, out CharacterData? characterData))
                return false;

            return TryGetCharacter(characterData, out result);
        }

        protected bool TryGetCharacterData(TextLayoutLineData line, int index, [NotNullWhen(true)] out CharacterData? characterData)
        {
            characterData = null;

            if (index < 0 || index >= line.Characters.Count)
                return false;

            characterData = line.Characters[index].Character;
            return true;
        }

        protected bool TryGetCharacter(CharacterData character, out ushort result)
        {
            result = ushort.MaxValue;

            if (!character.IsVisible)
                return false;

            if (character is FontCharacterData fontCharacterData)
            {
                result = fontCharacterData.Character;
                return true;
            }

            return false;
        }

        protected MetricDetailData CreateMetricDetail(TextLayoutLineData line, MetricDetailType type,
            MetricDetailLevel level = MetricDetailLevel.Error)
        {
            int boxWidth = Math.Max(line.BoundingBox.Width, 1);
            int boxHeight = Math.Max(line.BoundingBox.Height, 1);

            return new MetricDetailData
            {
                Type = type,
                Level = level,
                BoundingBox = line.BoundingBox with
                {
                    Width = boxWidth,
                    Height = boxHeight
                }
            };
        }

        protected MetricDetailData CreateMetricDetail(TextLayoutCharacterData characterData, MetricDetailType type, 
            MetricDetailLevel level = MetricDetailLevel.Error)
        {
            int boxWidth = Math.Max(characterData.BoundingBox.Width, 1);
            int boxHeight = Math.Max(characterData.BoundingBox.Height, 1);

            return new MetricDetailData
            {
                Type = type,
                Level = level,
                BoundingBox = characterData.BoundingBox with
                {
                    Width = boxWidth,
                    Height = boxHeight
                }
            };
        }

        protected MetricDetailData CreateMetricDetail(TextLayoutCharacterData startCharacterData, TextLayoutCharacterData endCharacterData,
            MetricDetailType type, MetricDetailLevel level = MetricDetailLevel.Error)
        {
            int maxRight = Math.Max(startCharacterData.BoundingBox.Right, endCharacterData.BoundingBox.Right);
            int minLeft = Math.Min(startCharacterData.BoundingBox.Left, endCharacterData.BoundingBox.Left);

            int maxBottom = Math.Max(startCharacterData.BoundingBox.Bottom, endCharacterData.BoundingBox.Bottom);
            int minTop = Math.Min(startCharacterData.BoundingBox.Top, endCharacterData.BoundingBox.Top);

            int width = Math.Max(maxRight - minLeft, 1);
            int height = Math.Max(maxBottom - minTop, 1);

            return new MetricDetailData
            {
                Type = type,
                Level = level,
                BoundingBox = startCharacterData.BoundingBox with
                {
                    Width = width,
                    Height = height
                }
            };
        }
    }
}
