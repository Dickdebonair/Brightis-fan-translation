using BrightistRenderer.Models.Texts.Layouts;
using BrightistRenderer.Models.Texts.Parsers;
using BrightistRenderer.Models.Texts.Parsers.ControlCodes;
using BrightistRenderer.Models.UI.Components;
using BrightistRenderer.Models.UI.Events.Messages;
using BrightistRenderer.Models.UI.Texts;
using BrightistRenderer.Texts.Layouts;
using BrightistRenderer.Texts.Parsers;
using BrightistRenderer.Texts.Renderers;
using BrightistRenderer.Texts.Screens;
using BrightistRenderer.UI.Components.Editors;
using BrightistRenderer.UI.Events;
using BrightistRenderer.UI.Texts.Interpreters;
using BrightistRenderer.UI.Texts.Parsers;
using ImGui.Forms.Controls;
using ImGui.Forms.Controls.Layouts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Size = ImGui.Forms.Models.Size;

namespace BrightistRenderer.UI.Components.Previews
{
    internal class OverlayStoryPreviewComponent : OverlayPreviewComponent
    {
        private readonly ComboBox<FlagData> _flagComboBox;
        private readonly CheckBox _flagCheckbox;
        private readonly StackLayout _flagLayout;

        private readonly PlayerStateData _state = new();

        private readonly TextParser _parser;
        private readonly StoryTextParser _storyParser;
        private readonly TextInterpreter _interpreter;
        private readonly TextRenderer? _renderer;

        private TextBlock[]? _characterBlocks;

        protected StoryPreviewData? PreviewData { get; private set; }

        public OverlayStoryPreviewComponent(OverlayEditorComponent parent)
            : base(parent)
        {
            _flagComboBox = new ComboBox<FlagData> { MaxShowItems = 3 };
            _flagCheckbox = new CheckBox();

            _flagLayout = new StackLayout
            {
                Alignment = Alignment.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                ItemSpacing = 5,
                Size = Size.WidthAlign,
                Items =
                {
                    _flagComboBox,
                    _flagCheckbox
                }
            };

            AddRightAlignedComponent(_flagLayout);

            _parser = TextParserProvider.GetDefault();
            _storyParser = new StoryTextParser();
            _interpreter = new TextInterpreter(_state);
            _renderer = TextRendererProvider.GetStoryText();

            _flagComboBox.SelectedItemChanged += _flagComboBox_SelectedItemChanged;
            _flagCheckbox.CheckChanged += _flagCheckbox_CheckChanged;

            EventBroker.Instance.Subscribe<OverlayStoryUpdatedMessage>(ProcessOverlayTextUpdated);
            EventBroker.Instance.Subscribe<OverlayStoryChangedMessage>(ProcessOverlayTextChanged);
            EventBroker.Instance.Subscribe<OverlayStoryIndexUpdatedMessage>(ProcessOverlayStoryIndexUpdated);
        }

        private void _flagComboBox_SelectedItemChanged(object? sender, EventArgs e)
        {
            _flagCheckbox.Checked = _flagComboBox.SelectedItem.Content.IsSet;
        }

        private void _flagCheckbox_CheckChanged(object? sender, EventArgs e)
        {
            if (_flagComboBox.SelectedItem == null)
                return;

            _flagComboBox.SelectedItem.Content.IsSet = _flagCheckbox.Checked;
            _state.Flags[_flagComboBox.SelectedItem.Content.Index] = _flagCheckbox.Checked;

            if (PreviewData != null)
            {
                InitializeTextBlocks(PreviewData.SheetData.TranslatedText);
                RaiseStoryIndexUpdated(Index);
            }
        }

        protected override int GetMaxCount() => _characterBlocks?.Length ?? 0;

        protected override Image<Rgba32>? RenderImage()
        {
            if (_renderer is null)
                return null;

            IList<CharacterData>? characters = _storyParser.GetCharacters(_characterBlocks, Index, out int lineCount);
            if (characters == null)
                return null;

            TextLayouter? layouter = TextLayouterProvider.GetStoryText(lineCount);
            if (layouter == null)
                return null;

            Image<Rgba32> screen = ScreenProvider.GetStoryText();

            TextLayoutData layout = layouter.Create(characters, screen.Size);
            _renderer.Render(screen, layout);

            return screen;
        }

        protected override void RaiseStoryIndexUpdated(int index)
        {
            if (PreviewData == null)
                return;

            EventBroker.Instance.Raise(new OverlayStoryIndexUpdatedMessage(Parent, PreviewData, index));
        }

        private void InitializeTextBlocks(string text)
        {
            IList<CharacterData> characters = _parser.Parse(text);
            characters = _interpreter.Interpret(characters);

            _characterBlocks = Array.Empty<TextBlock>();
            if (characters.Count <= 0)
                return;

            _characterBlocks = _storyParser.ParseTextBlocks(characters);
        }

        private void ResetFlags(string text)
        {
            int[] flags = GetFlags(text);

            _state.Flags.Clear();
            _flagComboBox.Items.Clear();

            foreach (int flag in flags)
            {
                _state.Flags[flag] = false;
                _flagComboBox.Items.Add(new DropDownItem<FlagData>(new FlagData
                {
                    Index = flag,
                    IsSet = false
                }, $"{flag}"));
            }

            _flagComboBox.SelectedItem = _flagComboBox.Items.Count <= 0 ? null : _flagComboBox.Items[0];

            _flagCheckbox.Checked = false;
            _flagCheckbox.Enabled = _flagComboBox.SelectedItem != null;
        }

        private void UpdateFlags(string text)
        {
            int[] flags = GetFlags(text);

            foreach (int removedFlag in _state.Flags.Keys.Except(flags))
            {
                _state.Flags.Remove(removedFlag);

                DropDownItem<FlagData>? removedItem = _flagComboBox.Items.FirstOrDefault(f => f.Content.Index == removedFlag);
                if (removedItem == null)
                    continue;

                if (_flagComboBox.SelectedItem == removedItem)
                    _flagComboBox.SelectedItem = null;

                _flagComboBox.Items.Remove(removedItem);
            }

            foreach (int flag in flags)
            {
                if (_state.Flags.ContainsKey(flag))
                    continue;

                _state.Flags[flag] = false;
                _flagComboBox.Items.Add(new DropDownItem<FlagData>(new FlagData
                {
                    Index = flag,
                    IsSet = false
                }, $"{flag}"));
            }

            _flagComboBox.SelectedItem ??= _flagComboBox.Items.Count <= 0 ? null : _flagComboBox.Items[0];

            _flagCheckbox.Checked = _flagComboBox.SelectedItem != null && _flagComboBox.SelectedItem.Content.IsSet;
            _flagCheckbox.Enabled = _flagComboBox.SelectedItem != null;
        }

        private int[] GetFlags(string text)
        {
            IList<CharacterData> characters = _parser.Parse(text);
            return characters
                .Where(c => c is FlagJumpControlCodeCharacterData)
                .Cast<FlagJumpControlCodeCharacterData>()
                .Select(c => c.Flag)
                .ToArray();
        }

        private void ProcessOverlayTextUpdated(OverlayStoryUpdatedMessage message)
        {
            if (message.Target != Parent)
                return;

            PreviewData = message.PreviewData;

            ResetFlags(message.PreviewData.SheetData.TranslatedText);
            InitializeTextBlocks(message.PreviewData.SheetData.TranslatedText);
            RaiseStoryIndexUpdated(0);
        }

        private void ProcessOverlayTextChanged(OverlayStoryChangedMessage message)
        {
            if (PreviewData == null ||
                PreviewData.SheetData.OverlayIndex != message.PreviewData.SheetData.OverlayIndex ||
                PreviewData.SheetData.Offset != message.PreviewData.SheetData.Offset)
                return;

            PreviewData = message.PreviewData;

            UpdateFlags(message.PreviewData.SheetData.TranslatedText);
            InitializeTextBlocks(message.PreviewData.SheetData.TranslatedText);
            RaiseStoryIndexUpdated(Index);
        }

        private void ProcessOverlayStoryIndexUpdated(OverlayStoryIndexUpdatedMessage message)
        {
            if (message.Target != Parent)
                return;

            UpdateIndex(message.Index);

            UpdatePreview();
        }
    }
}
