using TranslationToSource.Models.Patchers;

namespace TranslationToSource.Patchers
{
    internal class OvrRangeProvider
    {
        public OvrRangeData CreateRangeData(OvrType type, int overlaySize)
        {
            return new OvrRangeData
            {
                OverlayBaseAddress = GetOverlayBaseAddress(type),
                OverlaySize = overlaySize,
                OverlayMaxSize = GetOverlayMaxSize(type)
            };
        }

        private long GetOverlayBaseAddress(OvrType type)
        {
            switch (type)
            {
                case OvrType.ProgOverlay:
                    return 0x80153eb0;

                case OvrType.SubOverlay:
                    return 0x80156ca4;

                case OvrType.CommonOverlay:
                    return 0x80158138;

                case OvrType.CnstOverlay:
                    return 0x8015f328;

                default:
                    throw new InvalidOperationException($"Unsupported overlay type '{type}'.");
            }
        }

        private int GetOverlayMaxSize(OvrType type)
        {
            switch (type)
            {
                case OvrType.ProgOverlay:
                    return 0x2df4;

                case OvrType.SubOverlay:
                    return 0x1494;

                case OvrType.CommonOverlay:
                    return 0x71f0;

                case OvrType.CnstOverlay:
                    return 0x33bf;

                default:
                    throw new InvalidOperationException($"Unsupported overlay type '{type}'.");
            }
        }
    }
}
