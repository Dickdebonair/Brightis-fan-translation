using System.Reflection;
using BrightistRenderer.Models.UI.Fonts;
using ImGui.Forms.Factories;
using ImGui.Forms.Models;
using ImGui.Forms.Resources;

namespace BrightistRenderer.UI.Fonts
{
    internal class FontProvider
    {
        public static readonly FontProvider Instance = new();

        private const string RobotoResource_ = "roboto.ttf";
        private const string NotoJpResource_ = "notojp.ttf";

        private const string RobotoName_ = "Roboto";
        private const string NotoJpName_ = "NotoJp";

        private const int FontSize_ = 15;
        
        public void RegisterFont(Font font)
        {
            FontGlyphRange range;
            switch (font)
            {
                case Font.Roboto:
                    range = FontGlyphRange.Latin;
                    FontFactory.RegisterFromResource(RobotoName_, Assembly.GetExecutingAssembly(), RobotoResource_, range);

                    range = FontGlyphRange.ChineseJapaneseKorean | FontGlyphRange.Symbols;
                    FontFactory.RegisterFromResource(NotoJpName_, Assembly.GetExecutingAssembly(), NotoJpResource_, range);
                    break;

                case Font.NotoJp:
                    range = FontGlyphRange.ChineseJapaneseKorean | FontGlyphRange.Symbols;
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
}
