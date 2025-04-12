using BrightisRendererV2.Models.Texts.Parsers;
using SixLabors.ImageSharp;

namespace BrightisRendererV2.Models.Texts.Layouts;

internal class TextLayoutCharacterData
{
    public CharacterData Character { get; set; }
    public Rectangle BoundingBox { get; set; }
    public Rectangle GlyphBoundingBox { get; set; }
}