using BrightisRendererV2.Models.Metrics;

namespace BrightisRendererV2.Models.UI.Components;

internal class StoryMetricData
{
    public StoryPreviewData PreviewData { get; set; }
    public int Index { get; set; }
    public IList<MetricDetailData> Details { get; set; }
}