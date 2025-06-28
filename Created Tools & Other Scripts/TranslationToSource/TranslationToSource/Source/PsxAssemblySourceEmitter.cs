using TranslationToSource.Models.Source.Instructions;

namespace TranslationToSource.Source;

internal class PsxAssemblySourceEmitter : AssemblySourceEmitter
{
    public string Emit(PsxInstruction instruction) => Emit((ArmipsInstruction)instruction);

    public override string Emit(ArmipsInstruction instruction)
    {
        switch (instruction)
        {
            case AddiuPsxInstruction addInstruction:
                return Emit(addInstruction);

            case JalPsxInstruction jalInstruction:
                return Emit(jalInstruction);

            default:
                return base.Emit(instruction);
        }
    }

    public string Emit(AddiuPsxInstruction addiuInstruction)
    {
        string destString = addiuInstruction.Destination.ToString().ToLower();
        string sourceString = addiuInstruction.Source.ToString().ToLower();

        short value = addiuInstruction.Value;
        string valueString = value < 0 ? $"-0x{Math.Abs(value):X4}" : $"0x{value:X4}";

        return $"addiu {destString}, {sourceString}, {valueString}";
    }

    public string Emit(JalPsxInstruction jalInstruction)
    {
        return $"jal 0x{jalInstruction.FunctionOffset:X8}";
    }
}