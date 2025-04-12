using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.Models.UI.Events.Messages;
using BrightisRendererV2.UI.Components.Previews;
using BrightisRendererV2.UI.Events;

namespace BrightisRendererV2.UI.Components.Editors;

internal class OverlayPopupEditorComponent : OverlayEditorComponent
{
    private PopupPreviewData? _previewData;

    public OverlayPopupEditorComponent()
    {
        EventBroker.Instance.Subscribe<OverlayPopupUpdatedMessage>(ProcessOverlayPopupUpdated);
        EventBroker.Instance.Subscribe<OverlayPopupChangedMessage>(ProcessOverlayPopupChanged);
    }

    protected override OverlayPreviewComponent GetPreviewComponent()
    {
        return new OverlayPopupPreviewComponent(this);
    }

    protected override void ChangeText(string text)
    {
        if (_previewData == null)
            return;

        _previewData.ActiveSheetData.TranslatedText = text;
    }

    protected override void RaiseOverlayTextChanged()
    {
        if (_previewData == null)
            return;

        EventBroker.Instance.Raise(new OverlayPopupChangedMessage(this, _previewData));
    }

    private void ProcessOverlayPopupUpdated(OverlayPopupUpdatedMessage message)
    {
        if (message.Target != this)
            return;

        _previewData = message.PreviewData;

        UpdateText(_previewData.ActiveSheetData.OriginalText, _previewData.ActiveSheetData.TranslatedText);
    }

    private void ProcessOverlayPopupChanged(OverlayPopupChangedMessage message)
    {
        if (message.Source == this || _previewData == null ||
            message.PreviewData.ActiveSheetData.OverlayIndex != _previewData.ActiveSheetData.OverlayIndex ||
            message.PreviewData.ActiveSheetData.Offset != _previewData.ActiveSheetData.Offset)
            return;

        UpdateText(message.PreviewData.ActiveSheetData.OriginalText, message.PreviewData.ActiveSheetData.TranslatedText);
    }
}