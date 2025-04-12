using System.Text.Json;
using BrightistRenderer.Models.Sheets;

namespace BrightistRenderer.Sheets
{
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
}
