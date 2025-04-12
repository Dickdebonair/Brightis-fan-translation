using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.UI.Components.Editors;

namespace BrightisRendererV2.Models.UI.Events.Messages;

internal class OverlayPopupUpdatedMessage
{
    public OverlayEditorComponent Target { get; }
    public PopupPreviewData PreviewData { get; set; }

    public OverlayPopupUpdatedMessage(OverlayEditorComponent target, PopupPreviewData previewData)
    {
        Target = target;
        PreviewData = previewData;
    }
}