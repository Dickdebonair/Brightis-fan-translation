using BrightistRenderer.Models.Texts.Fonts;
using BrightistRenderer.Models.Texts.Layouts;
using BrightistRenderer.Texts.Fonts;
using SixLabors.ImageSharp;

namespace BrightistRenderer.Texts.Layouts
{
    internal class TextLayouterProvider
    {
        private static TextLayouter?[] _layouters = new TextLayouter?[2];

        public static TextLayouter? GetStoryText(int lineCount)
        {
            int initY = lineCount <= 3 ? 179 : 179 - (lineCount - 3) * 15;

            var options = new LayoutOptions
            {
                VerticalAlignment = VerticalTextAlignment.Top,
                HorizontalAlignment = HorizontalTextAlignment.Left,
                InitPoint = new Point(16, initY),
                LineHeight = 15,
                LineWidth = -1,
                TextScale = 1f,
                TextSpacing = 0
            };

            FontData? font = FontProvider.GetSmallStoryFont();
            if (font == null)
                return null;

            return new TextLayouter(options, font);
        }

        public static TextLayouter? GetPopupText()
        {
            if (_layouters[0] != null)
                return _layouters[0]!;

            var options = new LayoutOptions
            {
                VerticalAlignment = VerticalTextAlignment.Top,
                HorizontalAlignment = HorizontalTextAlignment.Left,
                InitPoint = new Point(192, 104),
                LineHeight = 15,
                LineWidth = -1,
                TextScale = 1f,
                TextSpacing = 0
            };

            FontData? font = FontProvider.GetLargePopupFont();
            if (font == null)
                return null;

            return _layouters[0] = new TextLayouter(options, font);
        }

        public static TextLayouter? GetSubPopupText()
        {
            if (_layouters[1] != null)
                return _layouters[1]!;

            var options = new LayoutOptions
            {
                VerticalAlignment = VerticalTextAlignment.Top,
                HorizontalAlignment = HorizontalTextAlignment.Left,
                InitPoint = new Point(208, 120),
                LineHeight = 15,
                LineWidth = -1,
                TextScale = 1f,
                TextSpacing = 0
            };

            FontData? font = FontProvider.GetLargePopupFont();
            if (font == null)
                return null;

            return _layouters[1] = new TextLayouter(options, font);
        }
    }
}
