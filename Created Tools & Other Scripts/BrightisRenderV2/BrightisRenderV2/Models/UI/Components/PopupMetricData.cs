using BrightisRendererV2.Models.Metrics;

namespace BrightisRendererV2.Models.UI.Components;

internal class PopupMetricData
{
    public PopupPreviewData PreviewData { get; set; }
    public IList<MetricDetailData> Details { get; set; }
}