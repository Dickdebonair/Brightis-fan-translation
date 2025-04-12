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
using BrightisRendererV2.UI.Texts.Parsers;
using ImGui.Forms;
using ImGui.Forms.Controls;
using ImGui.Forms.Controls.Tree;
using ImGui.Forms.Models.IO;
using SixLabors.ImageSharp;
using Veldrid;
using Rectangle = Veldrid.Rectangle;

namespace BrightisRendererV2.UI.Dialogs;

internal partial class MetricsStoryDialog
{
    private static readonly Size BoundingBox = new(320, 230);
    private static readonly KeyCommand SaveCommand = new(ModifierKeys.Control, Key.S);

    private readonly OverlayTranslationManager _translationManager;

    private readonly TextParser _parser;
    private readonly StoryTextParser _storyParser;

    private readonly StoryMetricStrategy _storyStrategy;

    private readonly Dictionary<(int, long, int), TreeNode<StoryMetricData>> _textNodeLookup = new();
    private readonly Queue<StoryMetricData> _queue = new();
    private readonly HashSet<int> _changedOverlays = [];

    public MetricsStoryDialog(OverlayTranslationManager translationManager)
    {
        InitializeComponent();

        _translationManager = translationManager;

        _parser = TextParserProvider.GetDefault();
        _storyParser = new StoryTextParser();

        _storyStrategy = new StoryMetricStrategy();

        _treeView.SelectedNodeChanged += _treeView_SelectedNodeChanged;

        EventBroker.Instance.Subscribe<OverlayStoryChangedMessage>(ProcessOverlayStoryChanged);

        LoadMetrics();
    }

    protected override void UpdateInternal(Rectangle contentRect)
    {
        base.UpdateInternal(contentRect);

        if (SaveCommand.IsPressed())
            Save();

        while (_queue.TryDequeue(out StoryMetricData? metricData))
        {
            var treeNode = new TreeNode<StoryMetricData>
            {
                Text = $"OVR_{metricData.PreviewData.SheetData.OverlayIndex:000}: 0x{metricData.PreviewData.SheetData.Offset:X8} {metricData.Index}",
                Data = metricData
            };

            (int, long, int) key = (metricData.PreviewData.SheetData.OverlayIndex, metricData.PreviewData.SheetData.Offset, metricData.Index);
            _textNodeLookup[key] = treeNode;

            _treeView.Nodes.Add(treeNode);
        }
    }

    private void Save()
    {
        foreach (int changedOverlay in _changedOverlays)
            RaiseOverlaySaved(changedOverlay);

        foreach (TreeNode<StoryMetricData> treeNode in _textNodeLookup.Values)
            ToggleNodeChanged(treeNode, false);

        _changedOverlays.Clear();
    }

    private void _treeView_SelectedNodeChanged(object? sender, EventArgs e)
    {
        UpdateMetricDetailTypes(_treeView.SelectedNode.Data.Details);

        RaiseStoryTextUpdated(_treeView.SelectedNode.Data.PreviewData);
        RaiseStoryIndexUpdated(_treeView.SelectedNode.Data.PreviewData, _treeView.SelectedNode.Data.Index);
        RaiseStoryMetricUpdated(_treeView.SelectedNode.Data);

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

            foreach (OverlaySheetData translation in translations)
                EnqueueTextBlockMetrics(translation);
        }

        _loadingLabel.Text = string.Empty;
    }

    private void EnqueueTextBlockMetrics(OverlaySheetData translation)
    {
        IList<CharacterData> characters = _parser.Parse(translation.TranslatedText);
        TextBlock[] textBlocks = _storyParser.ParseTextBlocks(characters);

        for (var i = 0; i < textBlocks.Length; i++)
        {
            var previewData = new StoryPreviewData { SheetData = translation };
            EnqueueTextBlockMetric(previewData, textBlocks, i);
        }
    }

    private void EnqueueTextBlockMetric(StoryPreviewData previewData, TextBlock[] textBlocks, int index)
    {
        IList<MetricDetailData> metricDetails = CreateMetricDetails(textBlocks, index);

        if (metricDetails.Count <= 0)
            return;

        StoryMetricData metricData = CreateMetricData(previewData, index, metricDetails);
        _queue.Enqueue(metricData);
    }

    private IList<MetricDetailData> CreateMetricDetails(TextBlock[] textBlocks, int index)
    {
        IList<CharacterData>? blockCharacters = _storyParser.GetCharacters(textBlocks, index, out int lineCount);
        if (blockCharacters == null)
            return Array.Empty<MetricDetailData>();

        TextLayouter? layouter = TextLayouterProvider.GetStoryText(lineCount);
        if (layouter == null)
            return Array.Empty<MetricDetailData>();

        TextLayoutData layout = layouter.Create(blockCharacters, BoundingBox);
        return _storyStrategy.Validate(layout, blockCharacters);
    }

    private StoryMetricData CreateMetricData(StoryPreviewData previewData, int index, IList<MetricDetailData> metricDetails)
    {
        return new StoryMetricData
        {
            PreviewData = previewData,
            Index = index,
            Details = metricDetails
        };
    }

    private void UpdateMetricDetailTypes(IList<MetricDetailData> metricDetails)
    {
        _detailTypeList.Items.Clear();

        foreach (MetricDetailType detailType in metricDetails.Where(d => d.Level == MetricDetailLevel.Error).Select(d => d.Type))
            _detailTypeList.Items.Add(new Label(detailType.ToString()));
    }

    private void ToggleNodeChanged(TreeNode<StoryMetricData> node, bool isChanged)
    {
        node.TextColor = isChanged ? Color.Orange : new ThemedColor(Color.Black, Color.White);
    }

    private void ProcessOverlayStoryChanged(OverlayStoryChangedMessage message)
    {
        if (message.Source != _storyEditor)
            return;

        (int, long, int) key = (message.PreviewData.SheetData.OverlayIndex, message.PreviewData.SheetData.Offset, message.Index);
        if (_textNodeLookup.TryGetValue(key, out TreeNode<StoryMetricData>? metricNode))
            ToggleNodeChanged(metricNode, true);

        _changedOverlays.Add(message.PreviewData.SheetData.OverlayIndex);
        RaiseOverlayChanged(message.PreviewData.SheetData.OverlayIndex);

        // Revalidate metrics and send update
        IList<CharacterData> characters = _parser.Parse(message.PreviewData.SheetData.TranslatedText);
        TextBlock[] textBlocks = _storyParser.ParseTextBlocks(characters);

        IList<MetricDetailData> metricDetails = CreateMetricDetails(textBlocks, message.Index);
        StoryMetricData metricData = CreateMetricData(message.PreviewData, message.Index, metricDetails);

        UpdateMetricDetailTypes(metricDetails);
        RaiseStoryMetricUpdated(metricData);
    }

    private void RaiseStoryTextUpdated(StoryPreviewData previewData)
    {
        EventBroker.Instance.Raise(new OverlayStoryUpdatedMessage(_storyEditor, previewData));
    }

    private void RaiseStoryIndexUpdated(StoryPreviewData previewData, int index)
    {
        EventBroker.Instance.Raise(new OverlayStoryIndexUpdatedMessage(_storyEditor, previewData, index));
    }

    private void RaiseStoryMetricUpdated(StoryMetricData metricData)
    {
        EventBroker.Instance.Raise(new OverlayStoryMetricUpdatedMessage(_storyEditor, metricData));
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