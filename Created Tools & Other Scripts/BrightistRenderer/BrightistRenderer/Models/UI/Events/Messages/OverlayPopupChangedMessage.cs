using BrightistRenderer.Models.Sheets;
using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.UI.Components.Editors;

namespace BrightistRenderer.Models.UI.Events.Messages
{
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
}
