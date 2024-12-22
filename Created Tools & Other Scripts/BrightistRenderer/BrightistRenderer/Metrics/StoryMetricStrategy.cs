using BrightistRenderer.Models.Metrics;
using BrightistRenderer.Models.Texts.Layouts;
using BrightistRenderer.Models.Texts.Parsers;

namespace BrightistRenderer.Metrics
{
    internal class StoryMetricStrategy : MetricStrategy
    {
        private readonly InvalidCharacterMetricStrategy _invalidCharacterMetric = new();
        private readonly PunctuationMetricStrategy _punctuationMetric = new();
        private readonly StorySpaceMetricStrategy _spaceMetric = new();
        private readonly TagMetricStrategy _tagMetric = new();

        public override IList<MetricDetailData> Validate(TextLayoutData layout, IList<CharacterData> characters)
        {
            var details = new List<MetricDetailData>();

            details.AddRange(_invalidCharacterMetric.Validate(layout, characters));
            details.AddRange(_punctuationMetric.Validate(layout, characters));
            details.AddRange(_spaceMetric.Validate(layout, characters));
            details.AddRange(_tagMetric.Validate(layout, characters));

            details.AddRange(CreateDetails(layout, characters));

            return details;
        }

        private IList<MetricDetailData> CreateDetails(TextLayoutData layout, IList<CharacterData> characters)
        {
            var result = new List<MetricDetailData>();

            if (layout.Lines.Count > 3)
            {
                for (var i = 0; i < layout.Lines.Count - 3; i++)
                    result.Add(CreateMetricDetail(layout.Lines[i], MetricDetailType.TooManyLines));
            }

            for (int i = Math.Max(0, layout.Lines.Count - 3); i < layout.Lines.Count; i++)
            {
                if (layout.Lines[i].BoundingBox.Width > 286)
                    result.Add(CreateMetricDetail(layout.Lines[i], MetricDetailType.LineTooLong));
            }

            return result;
        }
    }
}
