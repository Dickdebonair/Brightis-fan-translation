using System.Text.Json.Serialization;

namespace BrightistRenderer.Models.Sheets
{
    internal class OverlayConfigData
    {
        public required string SheetName { get; set; }
        public required int SheetMaxRow { get; set; }
        public required int OverlaySlot { get; set; }
    }

    [JsonSerializable(typeof(OverlayConfigData[]), GenerationMode = JsonSourceGenerationMode.Metadata)]
    partial class OverlayConfigDataContext : JsonSerializerContext
    {
        public static readonly OverlayConfigDataContext Instance = new();
    }
}
