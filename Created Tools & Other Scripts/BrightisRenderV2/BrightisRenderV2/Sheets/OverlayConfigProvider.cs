using BrightisRendererV2.Models.Sheets;
using System.Text.Json;

namespace BrightisRendererV2.Sheets;

internal class OverlayConfigProvider
{
    public static OverlayConfigData[] GetConfigs()
    {
        if (!File.Exists("overlay_config.json"))
            return [];

        string json = File.ReadAllText("overlay_config.json");
        OverlayConfigData[]? jsonData = JsonSerializer.Deserialize(json, OverlayConfigDataContext.Instance.OverlayConfigDataArray);

        if (jsonData == null)
            return [];

        return jsonData;
    }
}