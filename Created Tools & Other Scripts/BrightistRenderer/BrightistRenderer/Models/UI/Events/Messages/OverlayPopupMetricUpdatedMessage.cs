using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.UI.Components.Editors;

namespace BrightistRenderer.Models.UI.Events.Messages
{
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
}
