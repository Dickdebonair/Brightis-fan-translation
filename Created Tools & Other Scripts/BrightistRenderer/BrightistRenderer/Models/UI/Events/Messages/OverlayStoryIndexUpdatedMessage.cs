using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.UI.Components.Editors;

namespace BrightistRenderer.Models.UI.Events.Messages
{
    internal class OverlayStoryIndexUpdatedMessage
    {
        public OverlayEditorComponent Target { get; }
        public StoryPreviewData PreviewData { get; }
        public int Index { get; }

        public OverlayStoryIndexUpdatedMessage(OverlayEditorComponent target, StoryPreviewData previewData, int index)
        {
            Target = target;
            PreviewData = previewData;
            Index = index;
        }
    }
}
