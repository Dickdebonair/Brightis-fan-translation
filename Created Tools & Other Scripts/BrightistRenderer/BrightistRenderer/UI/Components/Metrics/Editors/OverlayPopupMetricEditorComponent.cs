using BrightistRenderer.UI.Components.Editors;
using BrightistRenderer.UI.Components.Previews;
using BrightistRenderer.UI.Components.Metrics.Previews;

namespace BrightistRenderer.UI.Components.Metrics.Editors
{
    internal class OverlayPopupMetricEditorComponent : OverlayPopupEditorComponent
    {
        protected override OverlayPreviewComponent GetPreviewComponent() =>
            new OverlayPopupMetricPreviewComponent(this);
    }
}
