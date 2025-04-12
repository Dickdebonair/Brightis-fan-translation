using SixLabors.ImageSharp;

namespace BrightisRendererV2.Models.Metrics;

public class MetricDetailData
{
    public MetricDetailType Type { get; set; }
    public MetricDetailLevel Level { get; set; }
    public Rectangle BoundingBox { get; set; }
}