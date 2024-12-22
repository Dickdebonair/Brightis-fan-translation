using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrightistRenderer.Models.Sheets
{
    internal class OverlayConfigData
    {
        public required string SheetName { get; set; }
        public required int SheetMaxRow { get; set; }
        public required int OverlaySlot { get; set; }
        [JsonConverter(typeof(HexIntConverter))]
        public required int OverlayLength { get; set; }
        public required OverlayType OverlayType { get; set; }
        public required OverlayMode OverlayMode { get; set; }
        [JsonConverter(typeof(HexLongConverter))]
        public long? PopupJalOffset { get; set; }
    }

    [JsonSerializable(typeof(OverlayConfigData[]), GenerationMode = JsonSourceGenerationMode.Metadata)]
    partial class OverlayConfigDataContext : JsonSerializerContext
    {
        public static readonly OverlayConfigDataContext Instance = new();
    }

    internal class HexLongConverter : JsonConverter<long>
    {
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            if (value == null || !value.StartsWith("0x"))
                return 0;

            return Convert.ToInt64(value, 16);
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    internal class HexIntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            if (value == null || !value.StartsWith("0x"))
                return 0;

            return Convert.ToInt32(value, 16);
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
