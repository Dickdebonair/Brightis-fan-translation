using System.Text.Json;
using TranslationToSource.Models.Config;

namespace TranslationToSource.Config
{
    class ConfigManager
    {
        public static readonly ConfigManager Instance = new();

        private readonly ConfigData _config;
        private readonly UserConfigData? _userConfig;

        public ConfigManager()
        {
            _config = GetConfigData();
            _userConfig = GetConfigUserData();
        }

        public string GetSheetId()
        {
            return _userConfig?.Credentials?.SheetId
                   ?? _config.Credentials.SheetId;
        }

        public string GetClientId()
        {
            return _userConfig?.Credentials?.ClientId
                   ?? _config.Credentials.ClientId;
        }

        public string GetClientSecret()
        {
            return _userConfig?.Credentials?.ClientSecret
                   ?? _config.Credentials.ClientSecret;
        }

        private static UserConfigData? GetConfigUserData()
        {
            string path = GetConfigUserPath();
            if (!File.Exists(path))
                return null;

            string json = File.ReadAllText(path);

            UserConfigData? config = JsonSerializer.Deserialize(json, UserConfigDataContext.Instance.UserConfigData);

            return config;
        }

        private static ConfigData GetConfigData()
        {
            string path = GetConfigPath();
            if (!File.Exists(path))
                throw new FileNotFoundException($"Could not find config.json.");

            string json = File.ReadAllText(path);

            ConfigData? config = JsonSerializer.Deserialize(json, ConfigDataContext.Instance.ConfigData);
            if (config is null)
                throw new InvalidOperationException("Could not deserialize config.json.");

            return config;
        }

        private static string GetConfigUserPath()
        {
            string path = GetConfigPath() + ".user";

            return path;
        }

        private static string GetConfigPath()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(dir, "config.json");

            return path;
        }
    }
}
