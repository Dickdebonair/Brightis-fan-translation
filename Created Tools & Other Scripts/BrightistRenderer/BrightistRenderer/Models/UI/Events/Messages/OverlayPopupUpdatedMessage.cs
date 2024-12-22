using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.UI.Components.Editors;

namespace BrightistRenderer.Models.UI.Events.Messages
{
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
}
