using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Reflection;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace BrightistRenderer.Texts.Screens
{
    internal class ScreenProvider
    {
        public static Image<Rgba32> GetStoryText()
        {
            var result = new Image<Rgba32>(320, 240);
            result.Mutate(i => i.Clear(Color.Black));

            Stream? resourceStream = GetFontStream("story_bg.png");
            if (resourceStream == null)
                return result;

            Image<Rgba32> screenImage = Image.Load<Rgba32>(resourceStream);
            result.Mutate(i => i.DrawImage(screenImage, new Point(10, 171), 1f));

            return result;
        }

        public static Image<Rgba32> GetPopupText()
        {
            var result = new Image<Rgba32>(320, 240);
            result.Mutate(i => i.Clear(Color.Black));

            return result;
        }

        private static Stream? GetFontStream(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }
    }
}
