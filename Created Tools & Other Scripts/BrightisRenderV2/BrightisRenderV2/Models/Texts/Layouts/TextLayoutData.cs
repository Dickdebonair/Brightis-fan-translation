using SixLabors.ImageSharp;

namespace BrightisRendererV2.Models.Texts.Layouts;

internal record TextLayoutData(IReadOnlyList<TextLayoutLineData> Lines, Rectangle BoundingBox);