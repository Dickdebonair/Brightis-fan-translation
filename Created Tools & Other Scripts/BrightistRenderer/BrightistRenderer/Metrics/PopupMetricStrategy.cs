using BrightistRenderer.Models.Metrics;
using BrightistRenderer.Models.Texts.Layouts;
using BrightistRenderer.Models.Texts.Parsers;
using SixLabors.ImageSharp;

namespace BrightistRenderer.Metrics
{
    internal class PopupMetricStrategy : MetricStrategy
    {
        private readonly InvalidCharacterMetricStrategy _invalidCharacterMetric = new();
        private readonly SpaceMetricStrategy _spaceMetric = new();

        public override IList<MetricDetailData> Validate(TextLayoutData layout, IList<CharacterData> characters)
        {
            var details = new List<MetricDetailData>();

            details.AddRange(_invalidCharacterMetric.Validate(layout, characters));
            details.AddRange(_spaceMetric.Validate(layout, characters));

            details.AddRange(CreateDetails(layout, characters));

            return details;
        }

        private IList<MetricDetailData> CreateDetails(TextLayoutData layout, IList<CharacterData> characters)
        {
            var result = new List<MetricDetailData>();

            if (layout.Lines.Count >= 1)
            {
                Rectangle boundingBox = layout.Lines[0].BoundingBox;
                if (boundingBox.Right > 320)
                    result.Add(CreateMetricDetail(layout.Lines[0], MetricDetailType.LineTooLong));
            }

            return result;
        }
    }
}
