using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text.Json;
using TranslationToSource.Models.Config;
using TranslationToSource.Reports;

namespace TranslationToSource.Config
{
    class ConfigManager
    {
        public static readonly ConfigManager Instance = new();

        private ConfigData _config { get; set; }
        private UserConfigData? _userConfig { get; set; }

        public string SheetId
        {
            get
            {
                return _userConfig?.Credentials?.SheetId
                       ?? _config.Credentials.SheetId;
            }
        }

        public string ClientId
        {
            get
            {
                return _userConfig?.Credentials?.ClientId
                       ?? _config.Credentials.ClientId;
            }
        }

        public string ClientSecret
        {
            get
            {
                return _userConfig?.Credentials?.ClientSecret
                       ?? _config.Credentials.ClientSecret;
            }
        }

        public ConfigManager()
        {
            _config = GetConfigData();
            _userConfig = GetConfigUserData();
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
            {
                ConsoleReport.Instance.WriteSectionName("Could Not Find Config.json");
                ConsoleReport.Instance.WriteInfo($"Could not find the Config.json");
                throw new FileNotFoundException("Could not find config.json");
            }

            string json = File.ReadAllText(path);

            try
            {
                var config = JsonSerializer.Deserialize<ConfigData>(json);

                ReportIfValueIsBad("Crendentials ClientID", String.IsNullOrWhiteSpace(config.Credentials.ClientId));
                ReportIfValueIsBad("Crendentials ClientID", String.IsNullOrWhiteSpace(config.Credentials.ClientSecret));

                return config;
            }
            catch (InvalidDataException)
            {
                throw;
            }
            catch (Exception e)
            {
                ConsoleReport.Instance.WriteSectionName("Error Parsing Config.json");
                ConsoleReport.Instance.WriteInfo($"There is an error trying to parse your config.json file. It is not in the correct format");
                ConsoleReport.Instance.WriteInfo(e.Message);
                throw;
            }
        }

        private static void ReportIfValueIsBad(string propName, bool check) {
            if (check)
            {
                ConsoleReport.Instance.WriteSectionName($"{propName} is incorrect, empty or bad");
                throw new InvalidDataException("Bad input");
            }
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
