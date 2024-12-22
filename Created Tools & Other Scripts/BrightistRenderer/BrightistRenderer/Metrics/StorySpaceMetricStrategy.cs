using BrightistRenderer.Models.Metrics;
using System.Diagnostics.CodeAnalysis;

namespace BrightistRenderer.Metrics
{
    internal class StorySpaceMetricStrategy : SpaceMetricStrategy
    {
        protected override bool IsInvalidSpacing(int spaceCount, bool isStartOfLine, bool isEndOfLine, [NotNullWhen(true)] out MetricDetailType? detailType)
        {
            // Detected multiple spaces in the line, not at the beginning
            if (spaceCount > 1 && !isStartOfLine)
            {
                detailType = MetricDetailType.ContinuousSpaces;
                return true;
            }

            // Detected spaces at the end of a line
            if (spaceCount > 0 && isEndOfLine)
            {
                detailType = spaceCount == 1 ? MetricDetailType.MisplacedSpace : MetricDetailType.ContinuousSpaces;
                return true;
            }

            detailType = null;
            return false;
        }
    }
}
