using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.UI.Components.Editors;

namespace BrightistRenderer.Models.UI.Events.Messages
{
    internal class OverlayStoryChangedMessage
    {
        public OverlayEditorComponent Source { get; }
        public StoryPreviewData PreviewData { get; }
        public int Index { get; }

        public OverlayStoryChangedMessage(OverlayEditorComponent source, StoryPreviewData previewData, int index)
        {
            Source = source;
            PreviewData = previewData;
            Index = index;
        }
    }
}
