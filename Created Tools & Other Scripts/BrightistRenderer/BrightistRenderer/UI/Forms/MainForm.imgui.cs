using BrightistRenderer.UI.Localizations;
using ImGui.Forms.Controls;
using ImGui.Forms.Controls.Layouts;
using System.Numerics;
using ImGui.Forms.Controls.Menu;

namespace BrightistRenderer.UI.Forms
{
    internal partial class MainForm
    {
        private MainMenuBar _mainMenu;

        private MenuBarMenu _metricsMenu;
        private MenuBarButton _metricsStoryMenu;
        private MenuBarButton _metricsPopupMenu;

        private StackLayout _mainLayout;

        private TabControl _overlayTabControl;
        private Label _loadingLabel;

        private void InitializeComponent()
        {
            _metricsStoryMenu = new MenuBarButton(StringResourceProvider.MenuMetricsStoryCaption());
            _metricsPopupMenu = new MenuBarButton(StringResourceProvider.MenuMetricsPopupCaption());

            _metricsMenu = new MenuBarMenu(StringResourceProvider.MenuMetricsCaption())
            {
                Items =
                {
                    _metricsStoryMenu,
                    _metricsPopupMenu
                }
            };

            _mainMenu = new MainMenuBar
            {
                Items =
                {
                    _metricsMenu
                }
            };

            _overlayTabControl = new TabControl();
            _loadingLabel = new Label(StringResourceProvider.FormLoadingCaption());

            _mainLayout = new StackLayout
            {
                Alignment = Alignment.Vertical,
                Size = ImGui.Forms.Models.Size.Parent,
                ItemSpacing = 5,
                Items =
                {
                    _overlayTabControl,
                    _loadingLabel
                }
            };

            Content = _mainLayout;
            MenuBar = _mainMenu;

            Size = new Vector2(1500, 800);
            Title = StringResourceProvider.FormTitleCaption();
        }
    }
}
