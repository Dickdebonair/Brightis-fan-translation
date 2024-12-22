using BrightistRenderer.Models.Texts.Parsers;
using BrightistRenderer.UI.Configuration;

namespace BrightistRenderer.Texts.Parsers
{
    internal class TextParserProvider
    {
        private static TextParser?[] _parsers = new TextParser?[1];

        public static TextParser GetDefault()
        {
            if (_parsers[0] != null)
                return _parsers[0]!;

            var options = new ParserOptions
            {
                PlayerName = ConfigProvider.Instance.GetPlayerName()
            };

            return _parsers[0] = new TextParser(options);
        }
    }
}
