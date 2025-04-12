using BrightisRendererV2.Models.Texts.Fonts;
using BrightisRendererV2.Models.Texts.Fonts.Glyphs;
using BrightisRendererV2.Models.Texts.Layouts;
using BrightisRendererV2.Models.Texts.Parsers;
using BrightisRendererV2.Models.Texts.Parsers.ControlCodes;
using SixLabors.ImageSharp;

namespace BrightisRendererV2.Texts.Layouts;

internal class TextLayouter
{
    protected LayoutOptions Options { get; }
    protected FontData FontData { get; }

    public TextLayouter(LayoutOptions options, FontData font)
    {
        Options = options;
        FontData = font;
    }

    public TextLayoutData Create(IList<CharacterData> characters, Size boundingBox)
    {
        return CreateAlignedLayout(characters, boundingBox);
    }

    private TextLayoutData CreateAlignedLayout(IList<CharacterData> parsedCharacters, Size boundingBox)
    {
        if (parsedCharacters.Count <= 0)
            return new TextLayoutData(Array.Empty<TextLayoutLineData>(), new Rectangle(Options.InitPoint, Size.Empty));

        IList<TextLayoutLineData> layoutLines = CreateLines(parsedCharacters);
        for (var i = 0; i < layoutLines.Count; i++)
        {
            TextLayoutLineData layoutLine = layoutLines[i];

            Point linePoint = GetLinePosition(layoutLine, boundingBox, layoutLines.Sum(l => l.BoundingBox.Height));
            linePoint = linePoint with
            {
                Y = linePoint.Y - i * Options.LineHeight
            };

            var characters = new List<TextLayoutCharacterData>();
            foreach (TextLayoutCharacterData lineCharacter in layoutLine.Characters)
            {
                lineCharacter.BoundingBox = lineCharacter.BoundingBox with
                {
                    X = lineCharacter.BoundingBox.X + linePoint.X,
                    Y = lineCharacter.BoundingBox.Y + linePoint.Y
                };
                lineCharacter.GlyphBoundingBox = lineCharacter.GlyphBoundingBox with
                {
                    X = lineCharacter.GlyphBoundingBox.X + linePoint.X,
                    Y = lineCharacter.GlyphBoundingBox.Y + linePoint.Y
                };

                characters.Add(lineCharacter);
            }

            layoutLine.Characters = characters;
            layoutLine.BoundingBox = layoutLine.BoundingBox with
            {
                X = layoutLine.BoundingBox.X + linePoint.X,
                Y = layoutLine.BoundingBox.Y + linePoint.Y
            };
        }

        var textPoint = new Point(layoutLines.Min(x => x.BoundingBox.X), layoutLines[0].BoundingBox.Y);
        var textSize = new Size(layoutLines.Max(x => x.BoundingBox.Width), layoutLines.Sum(l => l.BoundingBox.Height));

        return new TextLayoutData(layoutLines.AsReadOnly(), new Rectangle(textPoint, textSize));
    }

    protected virtual Point GetLinePosition(TextLayoutLineData currentLine, Size boundingBox, int linesHeight)
    {
        int x = GetLinePositionX(currentLine, boundingBox.Width);
        int y = GetLinePositionY(currentLine, boundingBox.Height, linesHeight);

        return new Point(x, y);
    }

    protected virtual int GetLinePositionX(TextLayoutLineData currentLine, int boundingWidth)
    {
        switch (Options.HorizontalAlignment)
        {
            case HorizontalTextAlignment.Left:
                return Options.InitPoint.X + currentLine.BoundingBox.X;

            case HorizontalTextAlignment.Center:
                return Options.InitPoint.X + currentLine.BoundingBox.X + (boundingWidth - Options.InitPoint.X - currentLine.BoundingBox.Width) / 2;

            case HorizontalTextAlignment.Right:
                return boundingWidth - Options.InitPoint.Y - currentLine.BoundingBox.Width;

            default:
                throw new InvalidOperationException($"Unsupported text alignment {Options.HorizontalAlignment}.");
        }
    }

    protected virtual int GetLinePositionY(TextLayoutLineData currentLine, int boundingHeight, int linesHeight)
    {
        switch (Options.VerticalAlignment)
        {
            case VerticalTextAlignment.Top:
                return Options.InitPoint.Y + currentLine.BoundingBox.Y;

            case VerticalTextAlignment.Center:
                return Options.InitPoint.Y + currentLine.BoundingBox.Y + (boundingHeight - Options.InitPoint.Y - linesHeight) / 2;

            case VerticalTextAlignment.Bottom:
                return boundingHeight - linesHeight - Options.InitPoint.Y + currentLine.BoundingBox.Y;

            default:
                throw new InvalidOperationException($"Unsupported text alignment {Options.VerticalAlignment}.");
        }
    }

    private IList<TextLayoutLineData> CreateLines(IList<CharacterData> parsedCharacters)
    {
        var context = new LayoutContext();

        foreach (CharacterData character in parsedCharacters)
            CreateCharacter(character, context);

        if (context.Characters.Count > 0)
        {
            context.Lines.Add(new TextLayoutLineData
            {
                Characters = context.Characters,
                BoundingBox = new Rectangle(new Point(0, context.Y), new Size(context.VisibleX, GetLineHeight()))
            });
        }
        else if (context.Lines.Count > 0 &&
                 context.Lines[^1].Characters.Count > 0 &&
                 context.Lines[^1].Characters[^1].Character is LineBreakControlCodeCharacterData)
        {
            context.Lines.Add(new TextLayoutLineData
            {
                Characters = Array.Empty<TextLayoutCharacterData>(),
                BoundingBox = new Rectangle(new Point(0, context.Y), new Size(1, GetLineHeight()))
            });
        }

        return context.Lines;
    }

    protected virtual void CreateCharacter(CharacterData character, LayoutContext context)
    {
        var characterLocation = new Point(context.VisibleX, context.Y);

        switch (character)
        {
            case PlayerNameControlCodeCharacterData playerName:
                foreach (CharacterData playerNameCharacter in playerName.PlayerNameCharacters)
                    CreateCharacter(playerNameCharacter, context);

                break;

            case LineBreakControlCodeCharacterData:
            case SoftStopControlCodeCharacterData:
            case FullStopControlCodeCharacterData:
                // Add line break character
                context.Characters.Add(new TextLayoutCharacterData
                {
                    Character = character,
                    BoundingBox = new Rectangle(characterLocation, Size.Empty),
                    GlyphBoundingBox = new Rectangle(characterLocation, Size.Empty)
                });

                // Create line from all current characters
                context.Lines.Add(new TextLayoutLineData
                {
                    Characters = context.Characters,
                    BoundingBox = new Rectangle(new Point(0, context.Y), new Size(context.VisibleX, GetLineHeight()))
                });

                context.X = 0;
                context.Y += GetLineHeight();
                context.VisibleX = 0;

                context.Characters = new List<TextLayoutCharacterData>();
                break;

            case ControlCodeCharacterData:
                break;

            default:
                Rectangle characterBox = GetCharacterBoundingBox(character, ref characterLocation, out bool isVisible);
                int advanceX = characterLocation.X - context.VisibleX + characterBox.Width;

                if (isVisible && Options.LineWidth > 0 && context.X + advanceX > Options.LineWidth)
                {
                    context.Lines.Add(new TextLayoutLineData
                    {
                        Characters = context.Characters,
                        BoundingBox = new Rectangle(new Point(0, context.Y), new Size(context.VisibleX, GetLineHeight()))
                    });

                    context.X = 0;
                    context.Y += GetLineHeight();
                    context.VisibleX = 0;

                    context.Characters = new List<TextLayoutCharacterData>();

                    characterLocation = new Point(context.VisibleX, context.Y);

                    characterBox = GetCharacterBoundingBox(character, ref characterLocation, out isVisible);
                    advanceX = characterLocation.X - context.VisibleX + characterBox.Width;
                }

                Rectangle glyphBox = GetGlyphBoundingBox(character, characterLocation);

                context.X += advanceX;
                if (isVisible)
                    context.VisibleX += advanceX;
                else
                {
                    characterBox = characterBox with
                    {
                        Width = 0,
                        Height = 0
                    };

                    glyphBox = glyphBox with
                    {
                        Width = 0,
                        Height = 0
                    };
                }

                context.Characters.Add(new TextLayoutCharacterData
                {
                    Character = character,
                    BoundingBox = characterBox,
                    GlyphBoundingBox = glyphBox
                });

                break;
        }
    }

    protected virtual Rectangle GetCharacterBoundingBox(CharacterData character, ref Point characterLocation, out bool isVisible)
    {
        isVisible = true;

        switch (character)
        {
            case FontCharacterData fontCharacter:
                CharacterDescriptionData? characterDescription = GetCharacterDescription(fontCharacter);
                if (characterDescription == null)
                    break;

                var glyphWidth = (int)(characterDescription.Width * Options.TextScale);
                var glyphSize = new Size(glyphWidth + Options.TextSpacing, GetLineHeight());

                characterLocation.X += characterDescription.X;
                return new Rectangle(characterLocation, glyphSize);
        }

        return new Rectangle(characterLocation, Size.Empty);
    }

    protected virtual Rectangle GetGlyphBoundingBox(CharacterData character, Point characterLocation)
    {
        switch (character)
        {
            case FontCharacterData fontCharacter:
                GlyphDescriptionData? glyphDescription = GetGlyphDescription(fontCharacter);
                if (glyphDescription == null)
                    break;

                var glyphWidth = (int)(glyphDescription.Width * Options.TextScale);
                var glyphHeight = (int)(glyphDescription.Height * Options.TextScale);

                int glyphX = characterLocation.X + glyphDescription.X;
                int glyphY = characterLocation.Y + glyphDescription.Y;

                return new Rectangle(glyphX, glyphY, glyphWidth, glyphHeight);
        }

        return new Rectangle(characterLocation, Size.Empty);
    }

    private CharacterDescriptionData? GetCharacterDescription(FontCharacterData character)
    {
        return FontData.Glyphs.GetCharacterDescription(character.Character) ??
               FontData.Glyphs.GetCharacterDescription(FontData.FallbackCharacter);
    }

    private GlyphDescriptionData? GetGlyphDescription(FontCharacterData character)
    {
        return FontData.Glyphs.GetGlyphDescription(character.Character) ??
               FontData.Glyphs.GetGlyphDescription(FontData.FallbackCharacter);
    }

    private int GetLineHeight()
    {
        if (Options.LineHeight > 0)
            return Options.LineHeight;

        return FontData.MaxHeight;
    }
}