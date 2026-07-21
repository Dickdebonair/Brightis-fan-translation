using Kompression.Contract.Configuration;
using Kompression.Contract.DataClasses.Encoder.LempelZiv;
using Kompression.Contract.Encoder;

namespace BrightistFiler.Compression
{
    internal class BrightisLzEncoder : ILempelZivEncoder
    {
        public void Configure(ILempelZivEncoderOptionsBuilder matchOptions)
        {
            matchOptions
                .CalculatePricesWith(() => new BrightisPriceCalculator())
                .FindPatternMatches().WithinLimitations(0x4, -1, 0x0, 0x1FFF)
                .AndFindRunLength().WithinLimitations(0x4, 0x1003);
        }

        public void Encode(Stream input, Stream output, IEnumerable<LempelZivMatch> matches)
        {
            foreach (LempelZivMatch match in matches)
            {
                // Write raw data
                if (input.Position < match.Position)
                {
                    long rawLength = match.Position - input.Position;
                    WriteRawData(input, output, rawLength);
                }

                if (match.Displacement == 0)
                    // Write RLE data
                    WriteRleData(input, output, match);
                else
                    // Write Lempel Ziv data
                    WriteLzData(input, output, match);
            }

            // Write remaining raw data
            long finalRawLength = input.Length - input.Position;
            if (finalRawLength != 0)
                WriteRawData(input, output, finalRawLength);

            // Write end flag
            output.WriteByte(0);
        }

        private static void WriteRawData(Stream input, Stream output, long rawLength)
        {
            while (rawLength > 0x1FFF)
            {
                output.WriteByte(0x3F);
                output.WriteByte(0xFF);

                for (var i = 0; i < 0x1FFF; i++)
                    output.WriteByte((byte)input.ReadByte());

                rawLength -= 0x1FFF;
            }

            if (rawLength > 0x1F)
                output.WriteByte((byte)(0x20 | (rawLength >> 8)));

            output.WriteByte((byte)rawLength);

            for (var i = 0; i < rawLength; i++)
                output.WriteByte((byte)input.ReadByte());
        }

        private static void WriteRleData(Stream input, Stream output, LempelZivMatch match)
        {
            int length = match.Length - 4;
            if (length > 0xF)
            {
                output.WriteByte((byte)(0x50 | (length >> 8)));
                output.WriteByte((byte)length);
            }
            else
            {
                output.WriteByte((byte)(0x40 | length));
            }

            output.WriteByte((byte)input.ReadByte());
            input.Position += match.Length - 1;
        }

        private static void WriteLzData(Stream input, Stream output, LempelZivMatch match)
        {
            output.WriteByte((byte)((Math.Min(0x7, match.Length) << 5) | match.Displacement >> 8));
            output.WriteByte((byte)match.Displacement);

            if (match.Length > 7)
            {
                int length = match.Length - 7;
                while (length > 0)
                {
                    output.WriteByte((byte)(0x60 | Math.Min(0x1F, length)));
                    length -= 0x1F;
                }
            }

            input.Position += match.Length;
        }
    }
}
