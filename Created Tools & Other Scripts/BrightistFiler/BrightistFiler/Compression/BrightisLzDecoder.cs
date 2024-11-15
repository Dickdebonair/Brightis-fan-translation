using Kompression.IO;
using Kontract.Kompression.Configuration;

namespace BrightistFiler.Compression
{
    internal class BrightisLzDecoder : IDecoder
    {
        public void Decode(Stream input, Stream output)
        {
            var circularBuffer = new CircularBuffer(0x1FFF);

            int flag = input.ReadByte();
            while (input.Position < input.Length && flag != 0)
            {
                if ((flag & 0x80) != 0)
                {
                    // Lempel Ziv (size min 0x4; max infinite) (disp min 0x0; max 0x1FFF)
                    int size = flag >> 5;
                    int distance = ((flag & 0x1F) << 8) | input.ReadByte();

                    int newFlag;
                    while (((newFlag = input.ReadByte()) & 0xE0) == 0x60)
                        size += newFlag & 0x1F;

                    input.Position--;

                    circularBuffer.Copy(output, distance, size);
                }
                else if ((flag & 0x40) != 0)
                {
                    // Run Length Encoding (min 0x4; max 0x1003)
                    int runLength = flag & 0xF;
                    if ((flag & 0x10) != 0)
                        runLength = runLength << 8 | input.ReadByte();

                    runLength += 4;
                    var runValue = (byte)input.ReadByte();

                    for (var i = 0; i < runLength; i++)
                    {
                        output.WriteByte(runValue);
                        circularBuffer.WriteByte(runValue);
                    }
                }
                else
                {
                    // Copy raw bytes (max 0x1FFF)
                    int rawCount = flag & 0x1F;

                    if ((flag & 0x20) != 0)
                        rawCount = rawCount << 8 | input.ReadByte();

                    for (var i = 0; i < rawCount; i++)
                    {
                        int rawByte = input.ReadByte();

                        output.WriteByte((byte)rawByte);
                        circularBuffer.WriteByte((byte)rawByte);
                    }
                }

                flag = input.ReadByte();
            }
        }

        public void Dispose() { }
    }
}
