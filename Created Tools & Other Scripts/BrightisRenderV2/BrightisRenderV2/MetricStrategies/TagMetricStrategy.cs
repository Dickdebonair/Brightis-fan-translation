using BrightisRendererV2.Models.Metrics;
using BrightisRendererV2.Models.Texts.Layouts;
using BrightisRendererV2.Models.Texts.Parsers;

namespace BrightisRendererV2.MetricStrategies;

internal class TagMetricStrategy : MetricStrategy
{
    private const ushort SmallerThan = 0x8183;
    private const ushort GreaterThan = 0x8184;

    public override IList<MetricDetailData> Validate(TextLayoutData layout, IList<CharacterData> characters)
    {
        var result = new List<MetricDetailData>();

        foreach (TextLayoutLineData layoutLine in layout.Lines)
        {
            foreach (TextLayoutCharacterData layoutCharacter in layoutLine.Characters)
            {
                if (layoutCharacter.Character is not FontCharacterData fontCharacter)
                    continue;

                switch (fontCharacter.Character)
                {
                    case SmallerThan:
                        result.Add(CreateMetricDetail(layoutCharacter, MetricDetailType.InvalidOpenTag));
                        break;

                    case GreaterThan:
                        result.Add(CreateMetricDetail(layoutCharacter, MetricDetailType.InvalidCloseTag));
                        break;
                }
            }
        }

        return result;
    }
}