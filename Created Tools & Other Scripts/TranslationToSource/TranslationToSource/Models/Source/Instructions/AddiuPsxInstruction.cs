namespace TranslationToSource.Models.Source.Instructions
{
    internal record AddiuPsxInstruction(PsxRegister Destination, PsxRegister Source, short Value) : PsxInstruction;
}
