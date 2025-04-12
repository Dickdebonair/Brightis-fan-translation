using BrightisRendererV2.Models.Texts.Layouts;
using BrightisRendererV2.Models.Texts.Parsers;
using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.Models.UI.Events.Messages;
using BrightisRendererV2.Texts.Layouts;
using BrightisRendererV2.Texts.Parsers;
using BrightisRendererV2.Texts.Renderers;
using BrightisRendererV2.Texts.Screens;
using BrightisRendererV2.UI.Components.Editors;
using BrightisRendererV2.UI.Events;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BrightisRendererV2.UI.Components.Previews;

internal class OverlayPopupPreviewComponent : OverlayPreviewComponent
{
    private readonly TextParser _parser;
    private readonly TextLayouter? _layouter;
    private readonly TextLayouter? _subLayouter;
    private readonly TextRenderer? _renderer;

    private PopupPreviewData? _previewData;

    public OverlayPopupPreviewComponent(OverlayEditorComponent parent)
        : base(parent)
    {
        _parser = TextParserProvider.GetDefault();
        _layouter = TextLayouterProvider.GetPopupText();
        _subLayouter = TextLayouterProvider.GetSubPopupText();
        _renderer = TextRendererProvider.GetPopupText();

        EventBroker.Instance.Subscribe<OverlayPopupUpdatedMessage>(ProcessOverlayPopupUpdated);
        EventBroker.Instance.Subscribe<OverlayPopupChangedMessage>(ProcessOverlayPopupChanged);
    }

    protected override int GetMaxCount() => 1;

    protected override Image<Rgba32>? RenderImage()
    {
        if (_previewData == null)
            return null;

        if (_layouter == null || _subLayouter == null || _renderer == null)
            return null;

        Image<Rgba32> result = ScreenProvider.GetPopupText();

        IList<CharacterData> popupCharacters = _parser.Parse(_previewData.PopupSheetData.TranslatedText);
        IList<CharacterData> subPopupCharacters = _parser.Parse(_previewData.SubPopupSheetData.TranslatedText);

        TextLayoutData popupLayout = _layouter.Create(popupCharacters, result.Size);
        TextLayoutData subPopupLayout = _subLayouter.Create(subPopupCharacters, result.Size);

        _renderer.Render(result, popupLayout);
        _renderer.Render(result, subPopupLayout);

        return result;
    }

    protected override void RaiseStoryIndexUpdated(int index)
    {
        // HINT: Popups don't update index
    }

    private void ProcessOverlayPopupUpdated(OverlayPopupUpdatedMessage message)
    {
        if (message.Target != Parent)
            return;

        _previewData = message.PreviewData;

        UpdateIndex(0);
        UpdatePreview();
    }

    private void ProcessOverlayPopupChanged(OverlayPopupChangedMessage message)
    {
        if (_previewData == null ||
            _previewData.ActiveSheetData.OverlayIndex != message.PreviewData.ActiveSheetData.OverlayIndex ||
            _previewData.ActiveSheetData.Offset != message.PreviewData.ActiveSheetData.Offset)
            return;

        _previewData = message.PreviewData;

        UpdatePreview();
    }
}