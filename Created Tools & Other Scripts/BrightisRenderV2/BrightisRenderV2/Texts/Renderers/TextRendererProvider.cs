using BrightisRendererV2.Models.Texts.Fonts;
using BrightisRendererV2.Models.Texts.Renderers;
using BrightisRendererV2.Texts.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BrightisRendererV2.Texts.Renderers;

internal class TextRendererProvider
{
    private static TextRenderer?[] _renderers = new TextRenderer?[2];

    public static TextRenderer? GetStoryText()
    {
        if (_renderers[0] != null)
            return _renderers[0]!;

        FontData? font = FontProvider.GetSmallStoryFont();
        if (font == null)
            return null;

        var options = new RenderOptions
        {
            DrawBoundingBoxes = false,
            TextColor = new Rgb24(0x20, 0x20, 0x20),
            VisibleLines = 3
        };

        return _renderers[0] = new TextRenderer(font, options);
    }

    public static TextRenderer? GetPopupText()
    {
        if (_renderers[1] != null)
            return _renderers[1]!;

        FontData? font = FontProvider.GetLargePopupFont();
        if (font == null)
            return null;

        var options = new RenderOptions
        {
            DrawBoundingBoxes = false,
            TextColor = Color.Transparent,
            VisibleLines = 1
        };

        return _renderers[1] = new TextRenderer(font, options);
    }
}