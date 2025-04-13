using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrightisRendererV2.Models.Sheets;

internal class OverlayConfigData
{
    public string SheetName { get; set; }
    public int SheetMaxRow { get; set; }
    public int OverlaySlot { get; set; }
}