using BrightistRenderer.UI.Components.Previews;
using ImGui.Forms.Controls.Base;
using ImGui.Forms.Controls.Layouts;
using ImGui.Forms.Controls.Text.Editor;
using ImGui.Forms.Models;
using Veldrid;

namespace BrightistRenderer.UI.Components.Editors
{
    internal abstract class OverlayEditorComponent : Component
    {
        private readonly StackLayout _mainLayout;
        private readonly StackLayout _textLayout;

        private readonly TextEditor _originalTextEditor;
        private readonly TextEditor _translatedTextEditor;

        protected OverlayPreviewComponent Preview { get; private set; }

        public OverlayEditorComponent()
        {
            _originalTextEditor = new TextEditor { IsReadOnly = true };
            _translatedTextEditor = new TextEditor();

            Preview = GetPreviewComponent();

            _textLayout = new StackLayout
            {
                Alignment = Alignment.Vertical,
                ItemSpacing = 5,
                Items =
                {
                    _originalTextEditor,
                    _translatedTextEditor
                }
            };

            _mainLayout = new StackLayout
            {
                Alignment = Alignment.Horizontal,
                ItemSpacing = 5,
                Items =
                {
                    _textLayout,
                    Preview
                }
            };

            _translatedTextEditor.TextChanged += _translatedTextEditor_TextChanged;
        }

        public override Size GetSize() => Size.Parent;

        protected override void UpdateInternal(Rectangle contentRect)
        {
            _mainLayout.Update(contentRect);
        }

        protected abstract OverlayPreviewComponent GetPreviewComponent();

        protected abstract void ChangeText(string text);
        protected abstract void RaiseOverlayTextChanged();

        protected void UpdateText(string originalText, string translatedText)
        {
            _originalTextEditor.SetText(originalText);
            _translatedTextEditor.SetText(translatedText);
        }

        private void _translatedTextEditor_TextChanged(object? sender, string e)
        {
            ChangeText(_translatedTextEditor.GetText());
            RaiseOverlayTextChanged();
        }
    }
}
