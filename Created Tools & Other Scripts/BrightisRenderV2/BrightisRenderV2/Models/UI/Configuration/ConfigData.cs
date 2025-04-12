using System.Text.Json.Serialization;

namespace BrightisRendererV2.Models.UI.Configuration;

internal class ConfigData
{
    public string SheetId { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string PlayerName { get; set; }
}

[JsonSerializable(typeof(ConfigData))]
internal partial class ConfigDataContext : JsonSerializerContext
{ }