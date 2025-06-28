using System.Text.Json.Serialization;

namespace TranslationToSource.Models.Config
{
    class UserConfigData
    {
        public UserCredentialsConfig? Credentials { get; set; }
    }

    [JsonSerializable(typeof(UserConfigData))]
    partial class UserConfigDataContext : JsonSerializerContext
    {
        public static readonly UserConfigDataContext Instance = new();
    }
}
