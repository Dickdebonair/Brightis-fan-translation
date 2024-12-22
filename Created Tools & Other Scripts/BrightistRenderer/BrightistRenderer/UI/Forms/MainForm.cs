using BrightistRenderer.Models.Sheets;
using BrightistRenderer.Models.UI.Events.Messages;
using BrightistRenderer.Sheets;
using BrightistRenderer.UI.Components;
using BrightistRenderer.UI.Dialogs;
using BrightistRenderer.UI.Events;
using BrightistRenderer.UI.Localizations;
using ImGui.Forms;
using ImGui.Forms.Controls;
using ImGui.Forms.Localization;
using ImGui.Forms.Modals;

namespace BrightistRenderer.UI.Forms
{
    internal partial class MainForm : Form
    {
        private readonly OverlayTranslationManager _translationManager;

        private readonly Dictionary<int, TabPage> _overlayPageLookup = new();
        private readonly Dictionary<int, OverlayConfigData> _overlayDataLookup = new();
        private readonly HashSet<int> _changedOverlays = new();

        private MetricsStoryDialog? _storyMetricsDialog;
        private MetricsPopupDialog? _popupMetricsDialog;

        public MainForm()
        {
            InitializeComponent();

            _translationManager = OverlayTranslationManager.Create();

            _metricsStoryMenu!.Clicked += _metricsStoryMenu_Clicked;
            _metricsPopupMenu!.Clicked += _metricsPopupMenu_Clicked;

                Closing += MainForm_Closing;

            EventBroker.Instance.Subscribe<OverlayChangedMessage>(ProcessOverlayChanged);
            EventBroker.Instance.Subscribe<OverlaySavedMessage>(ProcessOverlaySaved);

            LoadTexts();
        }

        private async void _metricsPopupMenu_Clicked(object? sender, EventArgs e)
        {
            _popupMetricsDialog ??= new MetricsPopupDialog();
            await _popupMetricsDialog.ShowAsync();
        }

        private async void _metricsStoryMenu_Clicked(object? sender, EventArgs e)
        {
            _storyMetricsDialog ??= new MetricsStoryDialog();
            await _storyMetricsDialog.ShowAsync();
        }

        private void LoadTexts()
        {
            Task.Run(InitializeTexts);
        }

        private async Task InitializeTexts()
        {
            foreach (OverlayConfigData overlayConfig in OverlayConfigProvider.GetConfigs())
            {
                IList<OverlaySheetData>? translations = await _translationManager.GetTranslationsAsync(overlayConfig);
                if (translations == null)
                    continue;

                var overlayTextComponent = new OverlayTextComponent(overlayConfig.OverlaySlot, translations);

                _overlayDataLookup[overlayConfig.OverlaySlot] = overlayConfig;

                var tabPage = new TabPage(overlayTextComponent)
                {
                    Title = $"OVR_{overlayConfig.OverlaySlot:000}"
                };
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
}
