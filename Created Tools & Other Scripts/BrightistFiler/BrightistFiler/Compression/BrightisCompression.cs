using Kompression.Configuration;
using Kompression.Contract;

namespace BrightistFiler.Compression
{
    internal class BrightisCompression : ICompression
    {
        private readonly ICompression _compression;

        public BrightisCompression()
        {
            _compression = new CompressionConfigurationBuilder()
                .Decode.With(() => new BrightisLzDecoder())
                .Encode.With(() => new BrightisLzEncoder())
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
