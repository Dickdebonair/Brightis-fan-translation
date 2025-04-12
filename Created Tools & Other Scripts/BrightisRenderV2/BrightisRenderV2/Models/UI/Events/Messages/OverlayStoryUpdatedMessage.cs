using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.UI.Components.Editors;

namespace BrightisRendererV2.Models.UI.Events.Messages;

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