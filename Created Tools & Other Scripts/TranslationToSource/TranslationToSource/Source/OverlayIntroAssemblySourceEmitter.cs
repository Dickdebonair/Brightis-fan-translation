using System.Text;
using TranslationToSource.Models.Patchers.Layout;
using TranslationToSource.Models.Source.Instructions;
using TranslationToSource.Models.Source;

namespace TranslationToSource.Source;

internal class OverlayIntroAssemblySourceEmitter : PsxAssemblySourceEmitter
{
    public string EmitTextPatchSource(OvrPatchLayoutData patchLayout, string origFileName)
    {
        var result = new StringBuilder();

        string architectureSource = Emit(new ArchitectureInstruction(ArmipsArchitecture.Psx));
        string openSource = Emit(new OpenInstruction(origFileName, patchLayout.OverlayRange.OverlayBaseAddress));

        result.AppendLine(architectureSource);
        result.AppendLine(openSource);

        result.AppendLine();

        foreach (OvrTextPatchLayoutData textPatch in patchLayout.TextPatches)
        {
            // Patch data offset instructions
            foreach (long dataOffset in textPatch.Patch.SheetData.DataOffsets)
            {
                string offsetSource = Emit(new SourceOffsetInstruction(dataOffset));
                string wordsSource = Emit(new WordsInstruction(new[] { textPatch.Offset }));

                result.AppendLine(offsetSource);
                result.AppendLine($"\t{wordsSource}");
            }

            // Patch text blob
            string offsetSource1 = Emit(new SourceOffsetInstruction(textPatch.Offset));
            result.AppendLine(offsetSource1);

            foreach (ArmipsInstruction instruction in textPatch.Patch.Patch.Instructions)
                result.AppendLine($"\t{Emit(instruction)}");

            result.AppendLine();
        }

        result.Append(Emit(new CloseInstruction()));

        return result.ToString();
    }
}