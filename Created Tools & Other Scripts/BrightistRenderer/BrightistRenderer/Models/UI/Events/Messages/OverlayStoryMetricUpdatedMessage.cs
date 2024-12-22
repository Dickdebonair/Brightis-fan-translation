using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.UI.Components.Editors;

namespace BrightistRenderer.Models.UI.Events.Messages
{
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
}
