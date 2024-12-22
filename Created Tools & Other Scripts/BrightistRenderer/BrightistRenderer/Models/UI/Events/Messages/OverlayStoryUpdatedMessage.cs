using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.UI.Components.Editors;

namespace BrightistRenderer.Models.UI.Events.Messages
{
    internal class OverlayStoryUpdatedMessage
    {
        public OverlayEditorComponent Target { get; }
        public StoryPreviewData PreviewData { get; }

        public OverlayStoryUpdatedMessage(OverlayEditorComponent target, StoryPreviewData previewData)
        {
            Target = target;
            PreviewData = previewData;
        }
    }
}
