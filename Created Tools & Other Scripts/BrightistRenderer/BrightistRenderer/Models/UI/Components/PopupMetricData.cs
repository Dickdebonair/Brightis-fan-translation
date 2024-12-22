using BrightistRenderer.Models.Metrics;

namespace BrightistRenderer.Models.UI.Components
{
    internal class PopupMetricData
    {
        public PopupPreviewData PreviewData { get; set; }
        public IList<MetricDetailData> Details { get; set; }
    }
}
