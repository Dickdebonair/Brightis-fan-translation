namespace TranslationToSource.Models.Source.Instructions;

internal record BytesInstruction(byte[] Bytes) : ArmipsInstruction;