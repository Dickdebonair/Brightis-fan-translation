using BrightistRenderer.Models.Metrics;

namespace BrightistRenderer.Models.UI.Components
{
    internal class StoryMetricData
    {
        public StoryPreviewData PreviewData { get; set; }
        public int Index { get; set; }
        public IList<MetricDetailData> Details { get; set; }
    }
}
