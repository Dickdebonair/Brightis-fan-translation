using System.Diagnostics.CodeAnalysis;
using BrightistRenderer.Models.Metrics;
using BrightistRenderer.Models.Texts.Layouts;
using BrightistRenderer.Models.Texts.Parsers;
using SharpGen.Runtime;
using Vulkan;

namespace BrightistRenderer.Metrics
{
    internal class SpaceMetricStrategy : MetricStrategy
    {
        private const ushort Space = 0x8140;

        public override IList<MetricDetailData> Validate(TextLayoutData layout, IList<CharacterData> characters)
        {
            var result = new List<MetricDetailData>();

            MetricDetailType? detailType;
            foreach (TextLayoutLineData layoutLine in layout.Lines)
            {
                TextLayoutCharacterData? startCharacter = null;
                TextLayoutCharacterData? endCharacter = null;

                var spaceCount = 0;

                var isStartOfLine = true;
                foreach (TextLayoutCharacterData layoutCharacter in layoutLine.Characters)
                {
                    if (layoutCharacter.Character is not FontCharacterData fontCharacter)
                        continue;

                    if (layoutCharacter.BoundingBox.Width <= 0)
                        continue;

                    if (fontCharacter.Character is not Space)
                    {
                        if (IsInvalidSpacing(spaceCount, isStartOfLine, false, out detailType))
                            result.Add(CreateMetricDetail(startCharacter!, endCharacter!, detailType.Value));

                        startCharacter = null;
                        endCharacter = null;

                        spaceCount = 0;

                        isStartOfLine = false;
                        continue;
                    }

                    if (spaceCount <= 0)
                        startCharacter = layoutCharacter;
                    endCharacter = layoutCharacter;

                    spaceCount++;
                }

                if (IsInvalidSpacing(spaceCount, isStartOfLine, true, out detailType))
                    result.Add(CreateMetricDetail(startCharacter!, endCharacter!, detailType.Value));
            }

            return result;
        }

        protected virtual bool IsInvalidSpacing(int spaceCount, bool isStartOfLine, bool isEndOfLine, [NotNullWhen(true)] out MetricDetailType? detailType)
        {
            switch (spaceCount)
            {
                // Detected a single space before the visible line
                case 1 when isStartOfLine || isEndOfLine:
                    detailType = MetricDetailType.MisplacedSpace;
                    return true;

                // Detected multiple spaces anywhere in the line
                case > 1:
                    detailType = MetricDetailType.ContinuousSpaces;
                    return true;

                default:
                    detailType = null;
                    return false;
            }
        }
    }
}
