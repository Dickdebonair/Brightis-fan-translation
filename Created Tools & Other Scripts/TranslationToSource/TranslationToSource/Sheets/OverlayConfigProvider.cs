using System.Text.Json;
using TranslationToSource.Models.Sheets;

namespace TranslationToSource.Sheets;

internal class OverlayConfigProvider
{
    public static OverlayConfigData[] GetConfigs()
    {
        if (!File.Exists("overlay_config.json"))
            return Array.Empty<OverlayConfigData>();

        string json = File.ReadAllText("overlay_config.json");
        OverlayConfigData[]? jsonData = JsonSerializer.Deserialize(json, OverlayConfigDataContext.Instance.OverlayConfigDataArray);
            
        if (jsonData == null)
            return Array.Empty<OverlayConfigData>();

        return jsonData;
    }
}