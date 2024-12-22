using BrightistRenderer.Models.Metrics;
using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.Models.UI.Events.Messages;
using BrightistRenderer.UI.Components.Editors;
using BrightistRenderer.UI.Components.Previews;
using BrightistRenderer.UI.Events;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace BrightistRenderer.UI.Components.Metrics.Previews
{
    internal class OverlayStoryMetricPreviewComponent : OverlayStoryPreviewComponent
    {
        private StoryMetricData? _metricData;

        public OverlayStoryMetricPreviewComponent(OverlayEditorComponent parent) : base(parent)
        {
            EventBroker.Instance.Subscribe<OverlayStoryMetricUpdatedMessage>(ProcessStoryMetricUpdated);
        }

        protected override Image<Rgba32>? RenderImage()
        {
            Image<Rgba32>? preview = base.RenderImage();
            if (preview == null || _metricData == null || Index != _metricData.Index)
                return preview;

            foreach (MetricDetailData detail in _metricData.Details)
            {
                switch (detail.Level)
                {
                    case MetricDetailLevel.Error:
                        preview.Mutate(x => x
                            .Fill(Color.Red.WithAlpha(.5f), detail.BoundingBox)
                            .Draw(Color.Red, 1, detail.BoundingBox));
                        break;

                    case MetricDetailLevel.Warn:
                        preview.Mutate(x => x
                            .Fill(Color.Gold.WithAlpha(.5f), detail.BoundingBox)
                            .Draw(Color.Gold, 1, detail.BoundingBox));
                        break;
                }
            }

            return preview;
        }

        private void ProcessStoryMetricUpdated(OverlayStoryMetricUpdatedMessage message)
        {
            if (message.Target != Parent)
                return;

            _metricData = message.MetricData;

            UpdatePreview();
        }
    }
}
