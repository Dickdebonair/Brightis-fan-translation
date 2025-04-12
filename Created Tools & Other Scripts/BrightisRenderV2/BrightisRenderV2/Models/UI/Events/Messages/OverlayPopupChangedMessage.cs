using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.UI.Components.Editors;

namespace BrightisRendererV2.Models.UI.Events.Messages;

internal class OverlayPopupChangedMessage
{
    public OverlayEditorComponent Source { get; }
    public PopupPreviewData PreviewData { get; }

    public OverlayPopupChangedMessage(OverlayEditorComponent source, PopupPreviewData previewData)
    {
        Source = source;
        PreviewData = previewData;
    }
}