namespace TranslationToSource.Models.Source.Instructions;

internal record OpenInstruction(string InputFile, string? OutputFile, long Offset) : ArmipsInstruction
{
    public OpenInstruction(string inputFile, long offset) : this(inputFile, null, offset)
    {
    }
}