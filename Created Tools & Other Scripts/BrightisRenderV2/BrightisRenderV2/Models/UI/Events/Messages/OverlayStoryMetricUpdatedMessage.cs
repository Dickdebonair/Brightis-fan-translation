using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.UI.Components.Editors;

namespace BrightisRendererV2.Models.UI.Events.Messages;

internal class OverlayStoryMetricUpdatedMessage
{
    public OverlayEditorComponent Target { get; }
    public StoryMetricData MetricData { get; }

    public OverlayStoryMetricUpdatedMessage(OverlayEditorComponent target, StoryMetricData metricData)
    {
        Target = target;
        MetricData = metricData;
    }
}