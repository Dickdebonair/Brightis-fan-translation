namespace BrightisRendererV2.Models.UI.Events.Messages;

internal class OverlaySavedMessage
{
    public int OverlayIndex { get; }

    public OverlaySavedMessage(int overlayIndex)
    {
        OverlayIndex = overlayIndex;
    }
}