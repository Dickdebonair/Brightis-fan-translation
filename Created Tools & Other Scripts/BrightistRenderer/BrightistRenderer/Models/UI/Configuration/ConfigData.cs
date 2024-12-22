using System.Text.Json.Serialization;

namespace BrightistRenderer.Models.UI.Configuration
{
    internal class ConfigData
    {
        public string SheetId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string PlayerName { get; set; }
    }

    [JsonSerializable(typeof(ConfigData))]
    partial class ConfigDataContext : JsonSerializerContext { }
}
