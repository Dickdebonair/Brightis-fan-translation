using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.UI.Components.Editors;

namespace BrightisRendererV2.Models.UI.Events.Messages;

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