using BrightisRendererV2.Models.UI.Fonts;
using ImGui.Forms.Factories;
using ImGui.Forms.Models;
using ImGui.Forms.Resources;
using System.Reflection;

namespace BrightisRendererV2.UI.Fonts;

internal class FontProvider
{
    public static readonly FontProvider Instance = new();

    private const string RobotoResource_ = "BrightisRendererV2.UI.Fonts.Resources.roboto.ttf";
    private const string NotoJpResource_ = "BrightisRendererV2.UI.Fonts.Resources.notojp.ttf";

    private const string RobotoName_ = "roboto";
    private const string NotoJpName_ = "notojp";

    private const int FontSize_ = 15;

    public void RegisterFont(Font font)
    {
        FontGlyphRange range;
        switch (font)
        {
            case Font.Roboto:
                range = FontGlyphRange.Latin;
                FontFactory.RegisterFromResource(RobotoName_, Assembly.GetExecutingAssembly(), RobotoResource_, range);

                range = FontGlyphRange.ChineseJapanese | FontGlyphRange.Korean | FontGlyphRange.Symbols;
                FontFactory.RegisterFromResource(NotoJpName_, Assembly.GetExecutingAssembly(), NotoJpResource_, range);
                break;

            case Font.NotoJp:
                range = FontGlyphRange.ChineseJapanese | FontGlyphRange.Korean | FontGlyphRange.Symbols;
                FontFactory.RegisterFromResource(NotoJpName_, Assembly.GetExecutingAssembly(), NotoJpResource_, range);
                break;

            default:
                throw new InvalidOperationException($"Unsupported font {font}.");
        }
    }

    public FontResource GetFont(Font font)
    {
        switch (font)
        {
            case Font.Roboto:
                return FontFactory.Get(RobotoName_, FontSize_, GetFont(Font.NotoJp));

            case Font.NotoJp:
                return FontFactory.Get(NotoJpName_, FontSize_);

            default:
                throw new InvalidOperationException($"Unsupported font {font}.");
        }
    }
}