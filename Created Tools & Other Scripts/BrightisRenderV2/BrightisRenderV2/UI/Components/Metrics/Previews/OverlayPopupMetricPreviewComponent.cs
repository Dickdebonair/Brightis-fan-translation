using BrightisRendererV2.Models.Metrics;
using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.Models.UI.Events.Messages;
using BrightisRendererV2.UI.Components.Editors;
using BrightisRendererV2.UI.Components.Previews;
using BrightisRendererV2.UI.Events;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace BrightisRendererV2.UI.Components.Metrics.Previews;

internal class OverlayPopupMetricPreviewComponent : OverlayPopupPreviewComponent
{
    private PopupMetricData? _metricData;

    public OverlayPopupMetricPreviewComponent(OverlayEditorComponent parent) : base(parent)
    {
        EventBroker.Instance.Subscribe<OverlayPopupMetricUpdatedMessage>(ProcessPopupMetricUpdated);
    }

    protected override Image<Rgba32>? RenderImage()
    {
        Image<Rgba32>? preview = base.RenderImage();
        if (preview == null || _metricData == null)
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

    private void ProcessPopupMetricUpdated(OverlayPopupMetricUpdatedMessage message)
    {
        if (message.Target != Parent)
            return;

        _metricData = message.MetricData;

        UpdatePreview();
    }
}