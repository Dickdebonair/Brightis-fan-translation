using BrightisRendererV2.UI.Components.Editors;
using BrightisRendererV2.UI.Components.Metrics.Previews;
using BrightisRendererV2.UI.Components.Previews;

namespace BrightisRendererV2.UI.Components.Metrics.Editors;

internal class OverlayPopupMetricEditorComponent : OverlayPopupEditorComponent
{
    protected override OverlayPreviewComponent GetPreviewComponent() =>
        new OverlayPopupMetricPreviewComponent(this);
}