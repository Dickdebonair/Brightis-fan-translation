using BrightistRenderer.UI.Components.Editors;
using BrightistRenderer.UI.Components.Previews;
using BrightistRenderer.UI.Components.Metrics.Previews;

namespace BrightistRenderer.UI.Components.Metrics.Editors
{
    internal class OverlayStoryMetricEditorComponent : OverlayStoryEditorComponent
    {
        protected override OverlayPreviewComponent GetPreviewComponent() =>
            new OverlayStoryMetricPreviewComponent(this);
    }
}
