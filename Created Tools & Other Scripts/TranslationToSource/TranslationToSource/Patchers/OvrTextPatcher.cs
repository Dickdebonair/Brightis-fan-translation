using System.Text;
using GoogleSheetsApiV4.Contract;
using TranslationToSource.Models.Patchers;
using TranslationToSource.Models.Sheets;
using TranslationToSource.Models.Source.Instructions;
using TranslationToSource.Models.Texts;
using TranslationToSource.Models.Texts.ControlCodes;
using TranslationToSource.Sheets;
using TranslationToSource.Texts;

namespace TranslationToSource.Patchers
{
    internal class OvrTextPatcher
    {
        private readonly TextParser _textParser = new();
        private readonly TextCalculator _textCalculator = new();
        private readonly OvrRangeProvider _rangeProvider = new();

        public async Task<OvrPatchData?> CreatePatchDataAsync(ISheetManager sheet, OverlayConfigData overlayConfig)
        {
            var translationManager = new OvrTranslationManager(sheet);

            var textPatches = new List<OvrTextPatchData>();

            IList<OvrSheetData>? sheetData = await translationManager.GetTranslationsAsync(overlayConfig);
            if (sheetData == null)
                return null;

            foreach (OvrSheetData translation in sheetData)
            {
                IList<CharacterData> patchText = _textParser.Parse(translation?.TranslatedText);
                IList<ArmipsInstruction> patchInstructions = CreateTextInstructions(patchText);

                textPatches.Add(new OvrTextPatchData
                {
                    SheetData = translation,
                    Patch = new OvrTextPatchInstructionsData
                    {
                        Length = (_textCalculator.CalculateByteLength(patchText) + 3) & ~3,
                        Instructions = patchInstructions.ToArray()
                    }
                });
            }

            return new OvrPatchData
            {
                Config = overlayConfig,
                OverlayRange = _rangeProvider.CreateRangeData(overlayConfig.OverlayType, overlayConfig.OverlayLength),
                TextPatches = textPatches.ToArray()
            };
        }

        private IList<ArmipsInstruction> CreateTextInstructions(IList<CharacterData> parsedText)
        {
            var result = new List<ArmipsInstruction>();

            var lineCache = new StringBuilder();
            var codeBytes = new List<byte>();

            foreach (CharacterData character in parsedText)
            {
                switch (character)
                {
                    case ControlCodeCharacterData controlCode:
                        if (lineCache.Length > 0)
                        {
                            result.Add(new AsciiInstruction(lineCache.ToString()));
                            lineCache.Clear();
                        }

                        codeBytes.Add(controlCode.Code);

                        switch (controlCode)
                        {
                            case FlagJumpControlCodeCharacterData flagJump:
                                codeBytes.Add((byte)(flagJump.Flag >> 8));
                                codeBytes.Add((byte)flagJump.Flag);
                                codeBytes.Add((byte)flagJump.LabelIndex);
                                break;

                            case FlagSetControlCodeCharacterData flagSet:
                                codeBytes.Add((byte)(flagSet.Flag >> 8));
                                codeBytes.Add((byte)flagSet.Flag);
                                break;

                            default:
                                codeBytes.AddRange(controlCode.Arguments.Select(arg => (byte)arg));
                                break;
                        }
                        break;

                    case FontCharacterData fontCharacter:
                        if (codeBytes.Count > 0)
                        {
                            result.Add(new BytesInstruction(codeBytes.ToArray()));
                            codeBytes.Clear();
                        }

                        lineCache.Append(fontCharacter.Character);
                        break;

                    default:
                        throw new InvalidOperationException($"Unsupported character '{character.GetType().Name}'.");
                }
            }

            codeBytes.Add(0);

            if (lineCache.Length > 0)
                result.Add(new AsciiInstruction(lineCache.ToString()));

            result.Add(new BytesInstruction(codeBytes.ToArray()));

            return result;
        }
    }
}
