using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.UI.Components.Editors;

namespace BrightisRendererV2.Models.UI.Events.Messages;

internal class OverlayPopupMetricUpdatedMessage
{
    public OverlayEditorComponent Target { get; }
    public PopupMetricData MetricData { get; }

    public OverlayPopupMetricUpdatedMessage(OverlayEditorComponent target, PopupMetricData metricData)
    {
        Target = target;
        MetricData = metricData;
    }
}