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
        private const int MaxGroupCount_ = 15;
        private const int TypeDivider_ = 100;

        private readonly OverlayTranslationManager _translationManager;

        private readonly Dictionary<TabPage, List<TabPage>> _overlayGroupTabLookup = new();
        private readonly Dictionary<TabPage, TabPage> _overlayTabGroupLookup = new();
        private readonly Dictionary<int, TabPage> _overlayConfigTabLookup = new();
        private readonly Dictionary<TabPage, int> _overlayTabConfigLookup = new();

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

            _overlayGroupsControl.PageRemoving += OverlayGroupsControl_PageRemoving;
            _overlayGroupsControl.PageRemoved += OverlayGroupsControl_PageRemoved;

            Closing += MainForm_Closing;

            EventBroker.Instance.Subscribe<OverlayChangedMessage>(ProcessOverlayChanged);
            EventBroker.Instance.Subscribe<OverlaySavedMessage>(ProcessOverlaySaved);

            LoadTexts();
        }

        private async Task OverlayGroupsControl_PageRemoving(object sender, RemovingEventArgs e)
        {
            if (!_overlayGroupTabLookup.TryGetValue(e.Page, out List<TabPage>? groupPages))
                return;

            if (groupPages.Any(x => x.HasChanges))
            {
                DialogResult unsavedResult = await GetUnsavedChangesResult();

                if (unsavedResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                foreach (TabPage groupPage in groupPages.Where(x => x.HasChanges))
                {
                    if (!_overlayTabConfigLookup.TryGetValue(groupPage, out int overlayIndex))
                        continue;

                    if (unsavedResult == DialogResult.Yes)
                        await Save(overlayIndex);

                    _changedOverlays.Remove(overlayIndex);
                }
            }
        }

        private void OverlayGroupsControl_PageRemoved(object? sender, RemoveEventArgs e)
        {
            if (!_overlayGroupTabLookup.Remove(e.Page, out List<TabPage>? groupPages))
                return;

            foreach (TabPage page in groupPages)
            {
                if (_overlayTabConfigLookup.Remove(page, out int overlayIndex))
                    _overlayConfigTabLookup.Remove(overlayIndex);

                _overlayTabGroupLookup.Remove(page);
            }
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
            OverlayConfigData[] overlayConfigs = OverlayConfigProvider.GetConfigs();

            foreach (IGrouping<int, OverlayConfigData> typeOverlays in overlayConfigs.GroupBy(x => x.OverlaySlot / TypeDivider_))
            {
                foreach (OverlayConfigData[] overlayConfigGroup in typeOverlays.Chunk(MaxGroupCount_))
                {
                    var groupControl = new TabControl();
                    groupControl.PageRemoving += OverlayGroupControl_PageRemoving;
                    groupControl.PageRemoved += OverlayGroupControl_PageRemoved;

                    var groupTab = new TabPage(groupControl)
                    {
                        Title = GetOverlayRangeName(overlayConfigGroup)
                    };
                    _overlayGroupsControl.AddPage(groupTab);

                    _overlayGroupTabLookup[groupTab] = [];

                    foreach (OverlayConfigData overlayConfig in overlayConfigGroup)
                    {
                        IList<OverlaySheetData>? translations = await _translationManager.GetTranslationsAsync(overlayConfig);
                        if (translations == null)
                            continue;

                        if (translations.All(x => x.TextType is null))
                            continue;

                        var overlayTextComponent = new OverlayTextComponent(overlayConfig.OverlaySlot, translations);

                        _overlayDataLookup[overlayConfig.OverlaySlot] = overlayConfig;

                        var tabPage = new TabPage(overlayTextComponent)
                        {
                            Title = GetOverlayName(overlayConfig)
                        };

                        _overlayConfigTabLookup[overlayConfig.OverlaySlot] = tabPage;
                        _overlayTabConfigLookup[tabPage] = overlayConfig.OverlaySlot;
                        _overlayGroupTabLookup[groupTab].Add(tabPage);
                        _overlayTabGroupLookup[tabPage] = groupTab;

                        groupControl.AddPage(tabPage);
                    }
                }
            }

            _loadingLabel.Text = string.Empty;
        }

        private async Task OverlayGroupControl_PageRemoving(object sender, RemovingEventArgs e)
        {
            if (!e.Page.HasChanges)
                return;

            if (!_overlayTabConfigLookup.TryGetValue(e.Page, out int overlayIndex))
                return;

            DialogResult unsavedResult = await GetUnsavedChangesResult();

            if (unsavedResult == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (unsavedResult == DialogResult.Yes)
                await Save(overlayIndex);

            _changedOverlays.Remove(overlayIndex);
        }

        private void OverlayGroupControl_PageRemoved(object? sender, RemoveEventArgs e)
        {
            if (_overlayTabGroupLookup.Remove(e.Page, out TabPage? groupPage))
            {
                if (_overlayGroupTabLookup.TryGetValue(groupPage, out List<TabPage>? groupPages))
                {
                    groupPages.Remove(e.Page);

                    if (groupPages.Count <= 0)
                        _overlayGroupTabLookup.Remove(groupPage);

                    groupPage.HasChanges = groupPages.Any(x => x.HasChanges);
                }
            }

            if (_overlayTabConfigLookup.Remove(e.Page, out int overlayIndex))
                _overlayConfigTabLookup.Remove(overlayIndex);
        }

        private async Task MainForm_Closing(object sender, ClosingEventArgs e)
        {
            if (_changedOverlays.Count <= 0)
                return;

            DialogResult result = await GetUnsavedChangesResult();

            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
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
            if (!_overlayConfigTabLookup.TryGetValue(message.OverlayIndex, out TabPage? overlayPage))
                return;

            if (!_overlayTabGroupLookup.TryGetValue(overlayPage, out TabPage? groupPage))
                return;

            overlayPage.HasChanges = true;
            groupPage.HasChanges = true;

            _changedOverlays.Add(message.OverlayIndex);
        }

        private void ProcessOverlaySaved(OverlaySavedMessage message)
        {
            if (!_overlayConfigTabLookup.TryGetValue(message.OverlayIndex, out TabPage? overlayPage))
                return;

            if (!_overlayTabGroupLookup.TryGetValue(overlayPage, out TabPage? groupPage))
                return;

            if (!_overlayGroupTabLookup.TryGetValue(groupPage, out List<TabPage>? groupPages))
                return;

            Save(message.OverlayIndex).Wait();

            overlayPage.HasChanges = false;
            if (groupPages.All(x => !x.HasChanges))
                groupPage.HasChanges = false;

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

        private string GetOverlayRangeName(OverlayConfigData[] overlayConfigGroup)
        {
            if (overlayConfigGroup.Length <= 0)
                return string.Empty;

            string firstOverlayName = GetOverlayName(overlayConfigGroup[0]);
            string lastOverlayName = GetOverlayName(overlayConfigGroup[^1]);

            return $"{firstOverlayName} - {lastOverlayName}";
        }

        private string GetOverlayName(OverlayConfigData overlayConfig)
        {
            return (overlayConfig.OverlaySlot / TypeDivider_) switch
            {
                0 => $"OVR_{overlayConfig.OverlaySlot:000}",
                1 => $"ONMOVR_{overlayConfig.OverlaySlot - TypeDivider_:000}",
                _ => string.Empty
            };
        }
    }
}
