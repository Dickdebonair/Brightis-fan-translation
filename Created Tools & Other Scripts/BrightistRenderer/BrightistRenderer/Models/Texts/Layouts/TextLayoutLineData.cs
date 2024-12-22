using SixLabors.ImageSharp;

namespace BrightistRenderer.Models.Texts.Layouts
{
    internal class TextLayoutLineData
    {
        public IList<TextLayoutCharacterData> Characters { get; set; }
        public Rectangle BoundingBox { get; set; }
    }
}
