using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BrightistRenderer.Models.Texts.Renderers
{
    internal class RenderOptions
    {
        public bool DrawBoundingBoxes { get; set; }

        public int VisibleLines { get; set; }

        public Rgb24 TextColor { get; set; } = Color.Transparent;
    }
}
