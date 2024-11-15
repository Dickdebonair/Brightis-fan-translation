using Kontract.Kompression;

namespace BrightistFiler.Compression
{
    internal class BrightisPriceCalculator : IPriceCalculator
    {
        public int CalculateLiteralPrice(int value, int literalRunLength, bool firstLiteralRun)
        {
            literalRunLength %= 0x1FFF;

            switch (literalRunLength)
            {
                case 1:
                case 0x20:
                    return 16;

                default:
                    return 8;
            }
        }

        public int CalculateMatchPrice(int displacement, int length, int matchRunLength, int firstValue)
        {
            if (displacement == 0)
            {
                // Run Length Pricing
                return length > 0x13 ? 24 : 16;
            }

            // Lempel Ziv Pricing
            if (length <= 7)
                return 16;

            length -= 7;

            int extraLength = length / 0x1F * 8;
            if (length % 0x1F > 0)
                extraLength += 8;

            return 16 + extraLength;
        }
    }
}
