using TranslationToSource.Models.Sheets;

namespace TranslationToSource.Models.Patchers;

internal class OvrTextPatchData
{
    public required OvrSheetData SheetData { get; set; }
    public required OvrTextPatchInstructionsData Patch { get; set; }
}