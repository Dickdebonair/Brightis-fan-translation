using BrightistRenderer.Models.Sheets;
using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.Models.UI.Events.Messages;
using BrightistRenderer.UI.Components.Editors;
using BrightistRenderer.UI.Events;
using BrightistRenderer.UI.Texts.Previews;
using ImGui.Forms;
using ImGui.Forms.Controls.Base;
using ImGui.Forms.Controls.Layouts;
using ImGui.Forms.Controls.Tree;
using ImGui.Forms.Models;
using ImGui.Forms.Models.IO;
using Veldrid;

namespace BrightistRenderer.UI.Components
{
    internal class OverlayTextComponent : Component
    {
        private static readonly KeyCommand SaveCommand = new(ModifierKeys.Control, Key.S);

        private readonly StackLayout _mainlayout;

        private readonly IList<OverlaySheetData> _texts;
        private readonly TreeView<OverlaySheetData> _textTreeView;

        private readonly OverlayStoryEditorComponent _storyEditor;
        private readonly OverlayPopupEditorComponent _popupEditor;

        private readonly int _overlayIndex;

        public OverlayTextComponent(int overlayIndex, IList<OverlaySheetData> texts)
        {
            _overlayIndex = overlayIndex;

            _texts = texts;
            _textTreeView = new TreeView<OverlaySheetData>
            {
                Size = new Size(SizeValue.Relative(.3f), SizeValue.Parent)
            };

            _storyEditor = new OverlayStoryEditorComponent();
            _popupEditor = new OverlayPopupEditorComponent();

            _mainlayout = new StackLayout
            {
                Alignment = Alignment.Horizontal,
                Size = Size.Parent,
                ItemSpacing = 5,
                Items =
                {
                    _textTreeView,
                    new StackItem(null) { Size = Size.WidthAlign }
                }
            };

            _textTreeView.SelectedNodeChanged += _textTreeView_SelectedNodeChanged;

            EventBroker.Instance.Subscribe<OverlayStoryChangedMessage>(ProcessOverlayStoryChanged);
            EventBroker.Instance.Subscribe<OverlayPopupChangedMessage>(ProcessOverlayPopupChanged);

            LoadTreeViewTexts(_textTreeView, texts);
        }

        public override Size GetSize() => Size.Parent;

        protected override void UpdateInternal(Rectangle contentRect)
        {
            _mainlayout.Update(contentRect);

            if (!Application.Instance.MainForm.HasOpenModals() && SaveCommand.IsPressed())
                Save();
        }

        protected override void SetTabInactiveCore()
        {
            _textTreeView.SetTabInactive();
            base.SetTabInactiveCore();
        }

        private void LoadTreeViewTexts(TreeView<OverlaySheetData> treeView, IList<OverlaySheetData> sheetData)
        {
            foreach (OverlaySheetData overlaySheet in sheetData)
            {
                treeView.Nodes.Add(new TreeNode<OverlaySheetData>
                {
                    Data = overlaySheet,
                    Text = $"{overlaySheet.Offset:X8}"
                });
            }
        }

        private void Save()
        {
            RaiseOverlaySaved(_overlayIndex);
        }

        private void _textTreeView_SelectedNodeChanged(object? sender, EventArgs e)
        {
            switch (_textTreeView.SelectedNode.Data.TextType)
            {
                case TextType.TextBox:
                    var storyPreviewData = new StoryPreviewData
                    {
                        SheetData = _textTreeView.SelectedNode.Data
                    };

                    _mainlayout.Items[1] = _storyEditor;
                    RaiseOverlayStoryUpdated(storyPreviewData);
                    break;

                case TextType.Popup:
                    int nodeIndex = _textTreeView.Nodes.IndexOf(_textTreeView.SelectedNode);
                    if (nodeIndex < 0)
                        break;

                    PopupPreviewData? popupPreviewData = PopupPreviewProvider.CreatePreviewData(_texts, nodeIndex);
                    if (popupPreviewData == null)
                        break;

                    _mainlayout.Items[1] = _popupEditor;
                    RaiseOverlayPopupUpdated(popupPreviewData);
                    break;
            }
        }

        private void ProcessOverlayStoryChanged(OverlayStoryChangedMessage message)
        {
            if (message.Source != _storyEditor)
                return;

            RaiseOverlayChanged(_overlayIndex);
        }

        private void ProcessOverlayPopupChanged(OverlayPopupChangedMessage message)
        {
            if (message.Source != _popupEditor)
                return;

            RaiseOverlayChanged(_overlayIndex);
        }

        private void RaiseOverlayStoryUpdated(StoryPreviewData previewData)
        {
            EventBroker.Instance.Raise(new OverlayStoryUpdatedMessage(_storyEditor, previewData));
        }

        private void RaiseOverlayPopupUpdated(PopupPreviewData previewData)
        {
            EventBroker.Instance.Raise(new OverlayPopupUpdatedMessage(_popupEditor, previewData));
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
}
