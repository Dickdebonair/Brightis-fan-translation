using BrightisRendererV2.Models.Sheets;
using Newtonsoft.Json;
using System.Text.Json;

namespace BrightisRendererV2.Sheets;

internal class OverlayConfigProvider
{
    public static OverlayConfigData[] GetConfigs()
    {
        if (!File.Exists("overlay_config.json"))
            return [];

        string json = File.ReadAllText("overlay_config.json");
        var jsonData = JsonConvert.DeserializeObject<IList<OverlayConfigData>>(json);

        if (jsonData == null)
            return [];

        return jsonData.ToArray();
    }
}