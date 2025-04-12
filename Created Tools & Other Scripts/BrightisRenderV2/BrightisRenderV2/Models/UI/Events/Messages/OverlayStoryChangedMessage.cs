using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.UI.Components.Editors;

namespace BrightisRendererV2.Models.UI.Events.Messages;

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