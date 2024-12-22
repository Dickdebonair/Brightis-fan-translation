namespace BrightistRenderer.Models.Texts.Layouts
{
    internal class LayoutContext
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int VisibleX { get; set; }

        public IList<TextLayoutLineData> Lines { get; set; } = new List<TextLayoutLineData>();
        public IList<TextLayoutCharacterData> Characters { get; set; } = new List<TextLayoutCharacterData>();
    }
}
