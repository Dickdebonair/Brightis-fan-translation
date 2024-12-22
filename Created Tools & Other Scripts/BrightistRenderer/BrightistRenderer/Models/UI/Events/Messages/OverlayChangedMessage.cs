namespace BrightistRenderer.Models.UI.Events.Messages
{
    internal class OverlayChangedMessage
    {
        public int OverlayIndex { get; }

        public OverlayChangedMessage(int overlayIndex)
        {
            OverlayIndex = overlayIndex;
        }
    }
}
