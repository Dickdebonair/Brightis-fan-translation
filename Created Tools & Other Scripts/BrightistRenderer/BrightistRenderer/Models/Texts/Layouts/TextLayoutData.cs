using SixLabors.ImageSharp;

namespace BrightistRenderer.Models.Texts.Layouts
{
    internal record TextLayoutData(IReadOnlyList<TextLayoutLineData> Lines, Rectangle BoundingBox);
}
