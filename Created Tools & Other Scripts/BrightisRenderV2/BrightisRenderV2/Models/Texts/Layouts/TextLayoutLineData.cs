using SixLabors.ImageSharp;

namespace BrightisRendererV2.Models.Texts.Layouts;

internal class TextLayoutLineData
{
    public IList<TextLayoutCharacterData> Characters { get; set; }
    public Rectangle BoundingBox { get; set; }
}