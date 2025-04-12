using SixLabors.ImageSharp;

namespace BrightisRendererV2.Models.Texts.Layouts;

internal class LayoutOptions
{
    public HorizontalTextAlignment HorizontalAlignment { get; init; } = HorizontalTextAlignment.Left;
    public VerticalTextAlignment VerticalAlignment { get; init; } = VerticalTextAlignment.Top;
    public Point InitPoint { get; init; }
    public int LineHeight { get; init; }
    public int LineWidth { get; init; }
    public float TextScale { get; init; } = 1f;
    public int TextSpacing { get; init; } = 1;
}