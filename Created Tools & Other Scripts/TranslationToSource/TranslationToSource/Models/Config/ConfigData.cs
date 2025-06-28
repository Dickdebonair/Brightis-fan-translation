using System.Text.Json.Serialization;

namespace TranslationToSource.Models.Config
{
    class ConfigData
    {
        public required CredentialsConfig Credentials { get; set; }
    }

    [JsonSerializable(typeof(ConfigData))]
    partial class ConfigDataContext : JsonSerializerContext
    {
        public static readonly ConfigDataContext Instance = new();
    }
}
