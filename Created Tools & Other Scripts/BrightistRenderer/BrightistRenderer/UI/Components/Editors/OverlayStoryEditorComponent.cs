using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.Models.UI.Events.Messages;
using BrightistRenderer.UI.Components.Previews;
using BrightistRenderer.UI.Events;

namespace BrightistRenderer.UI.Components.Editors
{
    internal class OverlayStoryEditorComponent : OverlayEditorComponent
    {
        protected StoryPreviewData? PreviewData { get; private set; }

        public OverlayStoryEditorComponent()
        {
            EventBroker.Instance.Subscribe<OverlayStoryUpdatedMessage>(ProcessOverlayStoryUpdated);
            EventBroker.Instance.Subscribe<OverlayStoryChangedMessage>(ProcessOverlayStoryChanged);
        }

        protected override OverlayPreviewComponent GetPreviewComponent()
        {
            return new OverlayStoryPreviewComponent(this);
        }

        protected override void ChangeText(string text)
        {
            if (PreviewData == null)
                return;

            PreviewData.SheetData.TranslatedText = text;
        }

        protected override void RaiseOverlayTextChanged()
        {
            if (PreviewData == null)
                return;

            EventBroker.Instance.Raise(new OverlayStoryChangedMessage(this, PreviewData, Preview.Index));
        }

        private void ProcessOverlayStoryUpdated(OverlayStoryUpdatedMessage message)
        {
            if (message.Target != this)
                return;

            PreviewData = message.PreviewData;

            UpdateText(message.PreviewData.SheetData.OriginalText, message.PreviewData.SheetData.TranslatedText);
        }

        private void ProcessOverlayStoryChanged(OverlayStoryChangedMessage message)
        {
            if (message.Source == this || PreviewData == null ||
                message.PreviewData.SheetData.OverlayIndex != PreviewData.SheetData.OverlayIndex ||
                message.PreviewData.SheetData.Offset != PreviewData.SheetData.Offset)
                return;

            UpdateText(message.PreviewData.SheetData.OriginalText, message.PreviewData.SheetData.TranslatedText);
        }
    }
}
