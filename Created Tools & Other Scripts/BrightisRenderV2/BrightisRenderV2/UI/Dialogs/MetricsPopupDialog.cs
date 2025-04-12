using BrightisRendererV2.MetricStrategies;
using BrightisRendererV2.Models.Metrics;
using BrightisRendererV2.Models.Sheets;
using BrightisRendererV2.Models.Texts.Layouts;
using BrightisRendererV2.Models.Texts.Parsers;
using BrightisRendererV2.Models.UI.Components;
using BrightisRendererV2.Models.UI.Events.Messages;
using BrightisRendererV2.Sheets;
using BrightisRendererV2.Texts.Layouts;
using BrightisRendererV2.Texts.Parsers;
using BrightisRendererV2.UI.Events;
using BrightisRendererV2.UI.Texts.Previews;
using ImGui.Forms;
using ImGui.Forms.Controls;
using ImGui.Forms.Controls.Tree;
using ImGui.Forms.Models.IO;
using SixLabors.ImageSharp;
using Veldrid;
using Rectangle = Veldrid.Rectangle;

namespace BrightisRendererV2.UI.Dialogs;

internal partial class MetricsPopupDialog
{
    private static readonly Size BoundingBox = new(320, 240);
    private static readonly KeyCommand SaveCommand = new(ModifierKeys.Control, Key.S);

    private readonly OverlayTranslationManager _translationManager;

    private readonly TextParser _parser;

    private readonly PopupMetricStrategy _popupStrategy;

    private readonly Dictionary<(int, long), TreeNode<PopupMetricData>> _textNodeLookup = new();
    private readonly Queue<PopupMetricData> _queue = new();
    private readonly HashSet<int> _changedOverlays = [];

    public MetricsPopupDialog(OverlayTranslationManager translationManager)
    {
        InitializeComponent();
        _parser = TextParserProvider.GetDefault();
        _popupStrategy = new PopupMetricStrategy();
        _treeView.SelectedNodeChanged += _treeView_SelectedNodeChanged;
        _translationManager = translationManager;

        EventBroker.Instance.Subscribe<OverlayPopupChangedMessage>(ProcessOverlayPopupChanged);

        LoadMetrics();
    }

    protected override void UpdateInternal(Rectangle contentRect)
    {
        base.UpdateInternal(contentRect);

        if (SaveCommand.IsPressed())
            Save();

        while (_queue.TryDequeue(out PopupMetricData? metricData))
        {
            var treeNode = new TreeNode<PopupMetricData>
            {
                Text = $"OVR_{metricData.PreviewData.ActiveSheetData.OverlayIndex:000}: 0x{metricData.PreviewData.ActiveSheetData.Offset:X8}",
                Data = metricData
            };

            (int, long) key = (metricData.PreviewData.ActiveSheetData.OverlayIndex, metricData.PreviewData.ActiveSheetData.Offset);
            _textNodeLookup[key] = treeNode;

            _treeView.Nodes.Add(treeNode);
        }
    }

    private void Save()
    {
        foreach (int changedOverlay in _changedOverlays)
            RaiseOverlaySaved(changedOverlay);

        foreach (TreeNode<PopupMetricData> treeNode in _textNodeLookup.Values)
            ToggleNodeChanged(treeNode, false);

        _changedOverlays.Clear();
    }

    private void _treeView_SelectedNodeChanged(object? sender, EventArgs e)
    {
        UpdateMetricDetailTypes(_treeView.SelectedNode.Data.Details);

        RaisePopupTextUpdated(_treeView.SelectedNode.Data.PreviewData);
        RaisePopupMetricUpdated(_treeView.SelectedNode.Data);

        if (_mainLayout.Items[1].Content == null)
            _mainLayout.Items[1] = _editorLayout;
    }

    private void LoadMetrics()
    {
        Task.Run(InitializeMetrics);
    }

    private async Task InitializeMetrics()
    {
        foreach (OverlayConfigData overlayConfig in OverlayConfigProvider.GetConfigs())
        {
            IList<OverlaySheetData>? translations = await _translationManager.GetTranslationsAsync(overlayConfig);
            if (translations == null)
                continue;

            for (var i = 0; i < translations.Count; i++)
            {
                PopupPreviewData? previewData = PopupPreviewProvider.CreatePreviewData(translations, i);
                if (previewData == null)
                    continue;

                EnqueuePreviewMetrics(previewData);
            }
        }

        _loadingLabel.Text = string.Empty;
    }

    private void EnqueuePreviewMetrics(PopupPreviewData previewData)
    {
        IList<MetricDetailData> metricDetails = CreateMetricDetails(previewData);
        if (metricDetails.Count <= 0)
            return;

        PopupMetricData metricData = CreateMetricData(previewData, metricDetails);

        _queue.Enqueue(metricData);
    }

    private IList<MetricDetailData> CreateMetricDetails(PopupPreviewData previewData)
    {
        IList<CharacterData> characters = _parser.Parse(previewData.ActiveSheetData.TranslatedText);

        TextLayouter? layouter = GetTextLayouter(previewData);
        if (layouter == null)
            return Array.Empty<MetricDetailData>();

        TextLayoutData layout = layouter.Create(characters, BoundingBox);
        return _popupStrategy.Validate(layout, characters);
    }

    private TextLayouter? GetTextLayouter(PopupPreviewData previewData)
    {
        return previewData.ActiveSheetData == previewData.PopupSheetData
            ? TextLayouterProvider.GetPopupText()
            : TextLayouterProvider.GetSubPopupText();
    }

    private PopupMetricData CreateMetricData(PopupPreviewData previewData, IList<MetricDetailData> metricDetails)
    {
        return new PopupMetricData
        {
            PreviewData = previewData,
            Details = metricDetails
        };
    }

    private void UpdateMetricDetailTypes(IList<MetricDetailData> metricDetails)
    {
        _detailTypeList.Items.Clear();

        foreach (MetricDetailType detailType in metricDetails.Where(d => d.Level == MetricDetailLevel.Error).Select(d => d.Type))
            _detailTypeList.Items.Add(new Label(detailType.ToString()));
    }

    private void ToggleNodeChanged(TreeNode<PopupMetricData> node, bool isChanged)
    {
        node.TextColor = isChanged ? Color.Orange : new ThemedColor(Color.Black, Color.White);
    }

    private void ProcessOverlayPopupChanged(OverlayPopupChangedMessage message)
    {
        if (message.Source != _popupEditor)
            return;

        // Update node state
        (int, long) key = (message.PreviewData.ActiveSheetData.OverlayIndex, message.PreviewData.ActiveSheetData.Offset);
        if (_textNodeLookup.TryGetValue(key, out TreeNode<PopupMetricData>? metricNode))
            ToggleNodeChanged(metricNode, true);

        // Mark changed overlay
        _changedOverlays.Add(message.PreviewData.ActiveSheetData.OverlayIndex);
        RaiseOverlayChanged(message.PreviewData.ActiveSheetData.OverlayIndex);

        // Revalidate metrics and send update
        IList<MetricDetailData> metricDetails = CreateMetricDetails(message.PreviewData);
        PopupMetricData metricData = CreateMetricData(message.PreviewData, metricDetails);

        UpdateMetricDetailTypes(metricDetails);
        RaisePopupMetricUpdated(metricData);
    }

    private void RaisePopupTextUpdated(PopupPreviewData previewData)
    {
        EventBroker.Instance.Raise(new OverlayPopupUpdatedMessage(_popupEditor, previewData));
    }

    private void RaisePopupMetricUpdated(PopupMetricData metricData)
    {
        EventBroker.Instance.Raise(new OverlayPopupMetricUpdatedMessage(_popupEditor, metricData));
    }

    private void RaiseOverlayChanged(int overlayIndex)
    {
        EventBroker.Instance.Raise(new OverlayChangedMessage(overlayIndex));
    }

    private void RaiseOverlaySaved(int overlayIndex)
    {
        EventBroker.Instance.Raise(new OverlaySavedMessage(overlayIndex));
    }
}