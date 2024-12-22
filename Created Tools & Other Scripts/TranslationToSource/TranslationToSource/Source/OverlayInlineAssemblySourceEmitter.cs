using System.Text;
using TranslationToSource.Models.Patchers.Layout;
using TranslationToSource.Models.Sheets;
using TranslationToSource.Models.Source.Instructions;
using TranslationToSource.Models.Source;

namespace TranslationToSource.Source
{
    internal class OverlayInlineAssemblySourceEmitter : PsxAssemblySourceEmitter
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
                    string addiuSource = Emit(new AddiuPsxInstruction(PsxRegister.A0, PsxRegister.A0, (short)(textPatch.Offset - 0x80160000)));

                    result.AppendLine(offsetSource);
                    result.AppendLine($"\t{addiuSource}");
                }

                // Patch print instructions
                foreach (long printOffset in textPatch.Patch.SheetData.PrintOffsets)
                {
                    switch (textPatch.Patch.SheetData.TextType)
                    {
                        case TextType.RightPopup:
                            string offsetSource = Emit(new SourceOffsetInstruction(printOffset));
                            string jalSource = Emit(new JalPsxInstruction(0x80155D3C));

                            result.AppendLine(offsetSource);
                            result.AppendLine($"\t{jalSource}");
                            break;
                    }
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
}
