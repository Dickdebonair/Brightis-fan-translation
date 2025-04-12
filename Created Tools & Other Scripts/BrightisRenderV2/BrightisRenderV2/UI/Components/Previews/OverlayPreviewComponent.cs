using BrightisRendererV2.UI.Components.Editors;
using ImGui.Forms.Controls;
using ImGui.Forms.Controls.Base;
using ImGui.Forms.Controls.Layouts;
using ImGui.Forms.Resources;
using ImGuiNET;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Rectangle = Veldrid.Rectangle;
using Size = ImGui.Forms.Models.Size;

namespace BrightisRendererV2.UI.Components.Previews;

internal abstract class OverlayPreviewComponent : Component
{
    private readonly StackLayout _mainLayout;
    private readonly StackLayout _headerLayout;
    private readonly StackLayout _buttonLayout;

    private readonly ArrowButton _previousButton;
    private readonly ArrowButton _nextButton;
    private readonly ZoomablePictureBox _preview;

    protected OverlayEditorComponent Parent { get; }
    public int Index { get; private set; }

    public OverlayPreviewComponent(OverlayEditorComponent parent)
    {
        Parent = parent;

        _previousButton = new ArrowButton(ImGuiDir.Left);
        _nextButton = new ArrowButton(ImGuiDir.Right);
        _preview = new ZoomablePictureBox();

        _buttonLayout = new StackLayout
        {
            Alignment = Alignment.Horizontal,
            ItemSpacing = 5,
            Size = Size.Content,
            Items =
            {
                _previousButton,
                _nextButton
            }
        };

        _headerLayout = new StackLayout
        {
            Alignment = Alignment.Horizontal,
            ItemSpacing = 5,
            Size = Size.WidthAlign,
            Items =
            {
                _buttonLayout
            }
        };

        _mainLayout = new StackLayout
        {
            Alignment = Alignment.Vertical,
            ItemSpacing = 5,
            Items =
            {
                _headerLayout,
                _preview
            }
        };

        _previousButton.Clicked += _previousButton_Clicked;
        _nextButton.Clicked += _nextButton_Clicked;
    }

    public override Size GetSize() => Size.Parent;

    protected override void UpdateInternal(Rectangle contentRect)
    {
        _mainLayout.Update(contentRect);
    }

    protected abstract int GetMaxCount();

    protected abstract Image<Rgba32>? RenderImage();

    protected void UpdateIndex(int index)
    {
        Index = Math.Max(0, Math.Min(GetMaxCount() - 1, index));

        UpdateArrowButtons();
    }

    protected void AddRightAlignedComponent(Component component)
    {
        _headerLayout.Items.Add(new StackItem(component) { Size = Size.WidthAlign });
    }

    private void UpdateArrowButtons()
    {
        _previousButton.Enabled = Index > 0;
        _nextButton.Enabled = Index < GetMaxCount() - 1;
    }

    protected void UpdatePreview()
    {
        _preview.Image = null;

        int maxCount = GetMaxCount();
        if (maxCount <= 0 || Index < 0 || Index >= maxCount)
            return;

        Image<Rgba32>? image = RenderImage();
        if (image == null)
        {
            _preview.Image = null;
            return;
        }

        _preview.Image = ImageResource.FromImage(image);
    }

    protected abstract void RaiseStoryIndexUpdated(int index);

    private void _previousButton_Clicked(object? sender, EventArgs e)
    {
        RaiseStoryIndexUpdated(Index - 1);
    }

    private void _nextButton_Clicked(object? sender, EventArgs e)
    {
        RaiseStoryIndexUpdated(Index + 1);
    }
}