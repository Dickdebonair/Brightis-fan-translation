using GoogleSheetsApiV4.Contract;
using TranslationToSource.Models.Patchers.Layout;
using TranslationToSource.Models.Patchers;
using TranslationToSource.Models.Sheets;
using TranslationToSource.Patchers.Layout;
using TranslationToSource.Source;

namespace TranslationToSource.Patchers;

internal class OverlayIntroPatcher : OverlayPatcher
{
    public async Task<string?> Patch(ISheetManager sheet, OverlayConfigData overlayConfig)
    {
        // Create text patches
        var patcher = new OvrTextPatcher();
        OvrPatchData? assemblyPatches = await patcher.CreatePatchDataAsync(sheet, overlayConfig);
        if (assemblyPatches == null)
            return null;

        // Create text patches layout
        List<OvrSectionData> sections = CreateSections(assemblyPatches);
        AppendUnusedSpace(sections, assemblyPatches, true);

        var ovrPatchLayouter = new OvrPatchLayouter();
        OvrPatchLayoutData? layout = ovrPatchLayouter.Create(assemblyPatches, sections);
        if (layout == null)
        {
            Console.WriteLine("Text could not fit into the overlay!");
            return null;
        }

        // Emit patch source
        var sourceEmitter = new OverlayIntroAssemblySourceEmitter();
        string source = sourceEmitter.EmitTextPatchSource(layout, $"OVR\\{overlayConfig.OverlaySlot:000}.bin");

        return source;
    }
}