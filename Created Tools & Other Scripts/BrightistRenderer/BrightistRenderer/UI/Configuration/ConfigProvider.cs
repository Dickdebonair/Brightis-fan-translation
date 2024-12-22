using BrightistRenderer.Models.UI.Configuration;
using System.Text.Json;

namespace BrightistRenderer.UI.Configuration
{
    internal class ConfigProvider
    {
        public static readonly ConfigProvider Instance = new();

        private readonly ConfigData _config;

        public ConfigProvider()
        {
            _config = InitializeConfig();
        }

        public string GetSheetId() => _config.SheetId;
        public string GetClientId() => _config.ClientId;
        public string GetClientSecret() => _config.ClientSecret;
        public string GetPlayerName() => _config.PlayerName;

        private ConfigData InitializeConfig()
        {
            if (!File.Exists("config.json"))
                throw new InvalidOperationException("Configuration 'config.json' not found.");

            string json = File.ReadAllText("config.json");
            ConfigData? jsonData = JsonSerializer.Deserialize(json, ConfigDataContext.Default.ConfigData);
            if (jsonData == null)
                throw new InvalidOperationException("Configuration 'config.json' has invalid format.");

            return jsonData;
        }
    }
}
