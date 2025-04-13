using BrightisRendererV2.Models.Sheets;
using BrightisRendererV2.Models.UI.Events.Messages;
using BrightisRendererV2.Sheets;
using BrightisRendererV2.UI.Components;
using BrightisRendererV2.UI.Dialogs;
using BrightisRendererV2.UI.Events;
using BrightisRendererV2.UI.Localizations;
using ImGui.Forms;
using ImGui.Forms.Controls;
using ImGui.Forms.Localization;
using ImGui.Forms.Modals;
using Microsoft.Extensions.DependencyInjection;

namespace BrightisRendererV2.UI.Forms;

internal partial class MainForm : Form
{
    private OverlayTranslationManager _translationManager;

    private readonly Dictionary<int, TabPage> _overlayPageLookup = [];
    private readonly Dictionary<int, OverlayConfigData> _overlayDataLookup = [];
    private readonly HashSet<int> _changedOverlays = [];

    private MetricsStoryDialog? _storyMetricsDialog;
    private MetricsPopupDialog? _popupMetricsDialog;

    public MainForm(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _translationManager = serviceProvider.GetService<OverlayTranslationManager>() ?? throw new ArgumentNullException("The OverlayTranslationManager was null");
        _metricsStoryMenu!.Clicked += MetricsPopupMenu_Clicked;
        _metricsPopupMenu!.Clicked += MetricsStoryMenu_Clicked;

        Closing += MainForm_Closing;
        EventBroker.Instance.Subscribe<OverlayChangedMessage>(ProcessOverlayChanged);
        EventBroker.Instance.Subscribe<OverlaySavedMessage>(ProcessOverlaySaved);
        LoadTexts();
    }

    private async void MetricsPopupMenu_Clicked(object? sender, EventArgs e)
    {
        _popupMetricsDialog ??= new MetricsPopupDialog(_translationManager);
        await _popupMetricsDialog.ShowAsync();
    }

    private async void MetricsStoryMenu_Clicked(object? sender, EventArgs e)
    {
        _storyMetricsDialog ??= new MetricsStoryDialog(_translationManager);
        await _storyMetricsDialog.ShowAsync();
    }

    private void LoadTexts()
    {
        Task.Run(InitializeTextsAsync);
    }

    private async Task InitializeTextsAsync()
    {
        var overlayConfigs = OverlayConfigProvider.GetConfigs();
        await _translationManager.LoadInitialTranslations(overlayConfigs);

        foreach (OverlayConfigData overlayConfig in overlayConfigs)
        {
            IList<OverlaySheetData>? translations = await _translationManager.GetTranslationsAsync(overlayConfig);
            if (translations == null)
                continue;

            var overlayTextComponent = new OverlayTextComponent(overlayConfig.OverlaySlot, translations);
            var tabPage = new TabPage(overlayTextComponent)
            {
                Title = $"OVR_{overlayConfig.OverlaySlot:000}"
            };

            _overlayDataLookup[overlayConfig.OverlaySlot] = overlayConfig;
            _overlayPageLookup[overlayConfig.OverlaySlot] = tabPage;
            _overlayTabControl.AddPage(tabPage);
        }

        _loadingLabel.Text = string.Empty;
    }

    private async Task MainForm_Closing(object arg1, ClosingEventArgs arg2)
    {
        if (_changedOverlays.Count <= 0)
            return;

        DialogResult result = await GetUnsavedChangesResult();

        if (result == DialogResult.Cancel)
        {
            arg2.Cancel = true;
            return;
        }

        if (result == DialogResult.Yes)
        {
            foreach (int overlayIndex in _changedOverlays)
                Save(overlayIndex).Wait();
        }
    }

    private void ProcessOverlayChanged(OverlayChangedMessage message)
    {
        if (!_overlayPageLookup.TryGetValue(message.OverlayIndex, out TabPage? selectedPage))
            return;

        selectedPage.HasChanges = true;

        _changedOverlays.Add(message.OverlayIndex);
    }

    private void ProcessOverlaySaved(OverlaySavedMessage message)
    {
        if (!_overlayPageLookup.TryGetValue(message.OverlayIndex, out TabPage? selectedPage))
            return;

        Save(message.OverlayIndex).Wait();

        selectedPage.HasChanges = false;

        _changedOverlays.Remove(message.OverlayIndex);
    }

    private async Task Save(int overlayIndex)
    {
        if (!_overlayDataLookup.TryGetValue(overlayIndex, out OverlayConfigData? data))
            return;

        await _translationManager.UpdateTranslationsAsync(data);
    }

    private async Task<DialogResult> GetUnsavedChangesResult()
    {
        LocalizedString caption = StringResourceProvider.UnsavedChangesCaption();
        LocalizedString text = StringResourceProvider.UnsavedChangesText();
        return await MessageBox.ShowYesNoCancelAsync(caption, text);
    }
}