using TranslationToSource.Models.Source.Instructions;

namespace TranslationToSource.Models.Patchers;

internal class OvrTextPatchInstructionsData
{
    public required ArmipsInstruction[] Instructions { get; set; }
    public required int Length { get; set; }
}