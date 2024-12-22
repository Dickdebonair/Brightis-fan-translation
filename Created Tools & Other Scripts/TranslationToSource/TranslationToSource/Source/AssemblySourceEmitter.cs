using TranslationToSource.Models.Source;
using TranslationToSource.Models.Source.Instructions;

namespace TranslationToSource.Source
{
    internal class AssemblySourceEmitter
    {
        public virtual string Emit(ArmipsInstruction instruction)
        {
            switch (instruction)
            {
                case SourceOffsetInstruction offsetInstruction:
                    return Emit(offsetInstruction);

                case AsciiInstruction asciiInstruction:
                    return Emit(asciiInstruction);

                case BytesInstruction bytesInstruction:
                    return Emit(bytesInstruction);

                case WordsInstruction wordInstruction:
                    return Emit(wordInstruction);

                case ArchitectureInstruction architectureInstruction:
                    return Emit(architectureInstruction);

                case OpenInstruction openInstruction:
                    return Emit(openInstruction);

                case CloseInstruction closeInstruction:
                    return Emit(closeInstruction);

                default:
                    throw new InvalidOperationException($"Unsupported instruction type {instruction.GetType().Name}.");
            }
        }

        public string Emit(SourceOffsetInstruction offsetInstruction)
        {
            return $".org 0x{offsetInstruction.Offset:X8}";
        }

        public string Emit(AsciiInstruction asciiInstruction)
        {
            return $".ascii \"{asciiInstruction.Text.Replace("\"", "\\\"")}\"";
        }

        public string Emit(BytesInstruction bytesInstruction)
        {
            return $".byte {string.Join(", ", bytesInstruction.Bytes)}";
        }

        public string Emit(WordsInstruction wordsInstruction)
        {
            return $".word {string.Join(", ", wordsInstruction.Values.Select(v => $"0x{v:X8}"))}";
        }

        public string Emit(ArchitectureInstruction architectureInstruction)
        {
            switch (architectureInstruction.Architecture)
            {
                case ArmipsArchitecture.Psx:
                    return ".psx";

                default:
                    throw new InvalidOperationException($"Unsupported architecture '{architectureInstruction.Architecture}'.");
            }
        }

        public string Emit(OpenInstruction openInstruction)
        {
            if (openInstruction.OutputFile == null)
                return $".open \"{openInstruction.InputFile}\", 0x{openInstruction.Offset:X8}";

            return $".open \"{openInstruction.InputFile}\", \"{openInstruction.OutputFile}\", 0x{openInstruction.Offset:X8}";
        }

        public string Emit(CloseInstruction closeInstruction)
        {
            return ".close";
        }
    }
}
