using Kompression.Configuration;
using Kontract.Kompression;

namespace BrightistFiler.Compression
{
    internal class BrightisCompression : ICompression
    {
        private readonly ICompression _compression;

        public BrightisCompression()
        {
            _compression = new KompressionConfiguration()
                .DecodeWith(() => new BrightisLzDecoder())
                .EncodeWith(() => new BrightisLzEncoder())
                .Build();
        }

        public void Decompress(Stream input, Stream output)
        {
            _compression.Decompress(input, output);
        }

        public void Compress(Stream input, Stream output)
        {
            _compression.Compress(input, output);
        }

        public void Dispose()
        {
            _compression.Dispose();
        }
    }
}
