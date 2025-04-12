using BrightisRendererV2.Models.Texts.Parsers;

namespace BrightisRendererV2.Models.UI.Components;

internal class TextBlock
{
    public IList<IList<CharacterData>> CharacterLines { get; } = new List<IList<CharacterData>>();
    public bool IsFullStop { get; set; }
}