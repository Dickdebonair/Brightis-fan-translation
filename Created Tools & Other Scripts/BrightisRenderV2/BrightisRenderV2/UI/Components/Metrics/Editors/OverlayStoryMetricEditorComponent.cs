using BrightisRendererV2.UI.Components.Editors;
using BrightisRendererV2.UI.Components.Metrics.Previews;
using BrightisRendererV2.UI.Components.Previews;

namespace BrightisRendererV2.UI.Components.Metrics.Editors;

internal class OverlayStoryMetricEditorComponent : OverlayStoryEditorComponent
{
    protected override OverlayPreviewComponent GetPreviewComponent() =>
        new OverlayStoryMetricPreviewComponent(this);
}