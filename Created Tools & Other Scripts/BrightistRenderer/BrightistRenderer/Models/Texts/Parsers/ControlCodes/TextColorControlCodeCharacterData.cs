using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace BrightistRenderer.Models.Texts.Parsers.ControlCodes
{
    internal class TextColorControlCodeCharacterData : ControlCodeCharacterData
    {
        public Rgb24 TextColor => Arguments.Length <= 2 ?
            Color.Transparent :
            Color.FromRgb((byte)Arguments[0], (byte)Arguments[1], (byte)Arguments[2]);

        public TextColorControlCodeCharacterData(byte code) : base(code)
        {
        }

        public TextColorControlCodeCharacterData(byte code, int[] args) : base(code, args)
        {
        }
    }
}
