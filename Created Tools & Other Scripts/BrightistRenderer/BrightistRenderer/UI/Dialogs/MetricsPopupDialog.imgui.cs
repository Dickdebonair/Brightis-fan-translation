using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.UI.Localizations;
using ImGui.Forms.Controls.Layouts;
using ImGui.Forms.Controls.Tree;
using ImGui.Forms.Modals;
using ImGui.Forms.Models;
using System.Numerics;
using BrightistRenderer.UI.Components.Metrics.Editors;
using ImGui.Forms.Controls;

namespace BrightistRenderer.UI.Dialogs
{
    internal partial class MetricsPopupDialog : Modal
    {
        private StackLayout _treeLayout;
        private StackLayout _mainLayout;
        private StackLayout _editorLayout;

        private TreeView<PopupMetricData> _treeView;
        private Label _loadingLabel;

        private ImGui.Forms.Controls.Lists.List<Label> _detailTypeList;

        private OverlayPopupMetricEditorComponent _popupEditor;

        protected void InitializeComponent()
        {
            _loadingLabel = new Label(StringResourceProvider.MetricsLoadingCaption());
            _treeView = new TreeView<PopupMetricData>();

            _detailTypeList = new ImGui.Forms.Controls.Lists.List<Label>
            {
                Alignment = Alignment.Horizontal,
                Size = Size.WidthAlign,
                ItemSpacing = 5,
                Padding = Vector2.Zero,
                IsSelectable = false
            };

            _popupEditor = new OverlayPopupMetricEditorComponent();

            _treeLayout = new StackLayout
            {
                Alignment = Alignment.Vertical,
                Size = new Size(SizeValue.Relative(.3f), SizeValue.Parent),
                ItemSpacing = 5,
                Items =
                {
                    _treeView,
                    _loadingLabel
                }
            };

            _editorLayout = new StackLayout
            {
                Alignment = Alignment.Vertical,
                Size = new Size(SizeValue.Relative(.7f), SizeValue.Parent),
                ItemSpacing = 5,
                Items =
                {
                    _detailTypeList,
                    _popupEditor
                }
            };

            _mainLayout = new StackLayout
            {
                Alignment = Alignment.Horizontal,
                ItemSpacing = 5,
                Items =
                {
                    _treeLayout,
                    new StackItem(null) { Size = new Size(SizeValue.Relative(.7f), SizeValue.Parent) }
                }
            };

            Content = _mainLayout;
            Caption = StringResourceProvider.DialogPopupMetricCaption();
            Size = new Size(SizeValue.Relative(.75f), SizeValue.Relative(.75f));
        }
    }
}
