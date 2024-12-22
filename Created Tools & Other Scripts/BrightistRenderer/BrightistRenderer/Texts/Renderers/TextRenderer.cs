using BrightistRenderer.Models.Texts.Fonts;
using BrightistRenderer.Models.Texts.Fonts.Glyphs;
using BrightistRenderer.Models.Texts.Layouts;
using BrightistRenderer.Models.Texts.Parsers;
using BrightistRenderer.Models.Texts.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace BrightistRenderer.Texts.Renderers
{
    internal class TextRenderer
    {
        private readonly FontData _fontData;
        private readonly RenderOptions _options;

        public TextRenderer(FontData fontData, RenderOptions options)
        {
            _fontData = fontData;
            _options = options;
        }

        public void Render(Image<Rgba32> image, TextLayoutData textLayout)
        {
            var bufferImage = new Image<Rgba32>(image.Width, image.Height);

            RenderLines(bufferImage, textLayout.Lines);

            if (_options.DrawBoundingBoxes)
                bufferImage.Mutate(x => x.Draw(Color.Red, 1f, textLayout.BoundingBox));

            image.Mutate(x => x.DrawImage(bufferImage, 1f));
        }

        private void RenderLines(Image<Rgba32> image, IReadOnlyList<TextLayoutLineData> lines)
        {
            for (var i = 0; i < lines.Count; i++)
            {
                bool isLineVisible = _options.VisibleLines <= 0 || lines.Count - i <= _options.VisibleLines;

                RenderLine(image, lines[i], isLineVisible);
            }
        }

        private void RenderLine(Image<Rgba32> image, TextLayoutLineData line, bool isLineVisible)
        {
            foreach (TextLayoutCharacterData character in line.Characters)
            {
                RenderCharacter(image, character, isLineVisible);

                if (_options.DrawBoundingBoxes)
                {
                    image.Mutate(x => x
                        .Fill(Color.RebeccaPurple.WithAlpha(.5f), character.BoundingBox)
                        .Draw(Color.RebeccaPurple, 1f, character.BoundingBox));
                }
            }

            if (_options.DrawBoundingBoxes)
                image.Mutate(x => x.Draw(Color.PaleVioletRed, 1f, line.BoundingBox));
        }

        private void RenderCharacter(Image<Rgba32> image, TextLayoutCharacterData character, bool isLineVisible)
        {
            if (character.GlyphBoundingBox.Width == 0 || character.GlyphBoundingBox.Height == 0)
                return;

            DrawCharacter(image, character, isLineVisible);
        }

        protected virtual void DrawCharacter(Image<Rgba32> image, TextLayoutCharacterData character, bool isLineVisible)
        {
            switch (character.Character)
            {
                case FontCharacterData fontCharacter:
                    GlyphData? fontGlyph = GetGlyph(fontCharacter);
                    if (fontGlyph?.GlyphDescription == null)
                        break;

                    if (fontGlyph.GlyphDescription.Width == 0 || fontGlyph.GlyphDescription.Height == 0)
                        break;

                    image.Mutate(x => x.DrawImage(fontGlyph.Glyph, character.GlyphBoundingBox.Location, isLineVisible ? 1f : .25f));

                    break;
            }
        }

        private GlyphData? GetGlyph(FontCharacterData character)
        {
            return _fontData.Glyphs.Get(character.Character, _options.TextColor) ??
                   _fontData.Glyphs.Get(_fontData.FallbackCharacter, _options.TextColor);
        }
    }
}
