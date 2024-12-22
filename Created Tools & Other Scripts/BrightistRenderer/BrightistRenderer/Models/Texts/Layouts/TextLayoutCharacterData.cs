using BrightistRenderer.Models.Texts.Parsers;
using SixLabors.ImageSharp;

namespace BrightistRenderer.Models.Texts.Layouts
{
    internal class TextLayoutCharacterData
    {
        public CharacterData Character { get; set; }
        public Rectangle BoundingBox { get; set; }
        public Rectangle GlyphBoundingBox { get; set; }
    }
}
