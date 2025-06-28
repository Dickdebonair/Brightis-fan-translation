using TranslationToSource.Models.Patchers.Layout;
using TranslationToSource.Models.Patchers;
using TranslationToSource.Models.Texts;
using TranslationToSource.Texts;

namespace TranslationToSource.Patchers;

internal abstract class OverlayPatcher
{
    protected TextParser TextParser { get; } = new();
    protected TextCalculator TextCalculator { get; } = new();

    protected List<OvrSectionData> CreateSections(OvrPatchData patchData)
    {
        var result = new List<OvrSectionData>();

        OvrTextPatchData[] orderedPatches = patchData.TextPatches.OrderBy(p => p.SheetData.Offset).ToArray();

        var currentOffset = 0L;
        var currentLength = 0;
        for (var i = 0; i < orderedPatches.Length; i++)
        {
            IList<CharacterData> originalCharacters = TextParser.Parse(orderedPatches[i].SheetData.OriginalText);

            int originalLength = TextCalculator.CalculateByteLength(originalCharacters);
            originalLength = (originalLength + 3) & ~3;

            if (i == 0)
            {
                currentOffset = orderedPatches[i].SheetData.Offset;
                currentLength = originalLength;
            }
            else
            {
                if (currentOffset + currentLength != orderedPatches[i].SheetData.Offset)
                {
                    result.Add(new OvrSectionData
                    {
                        Offset = currentOffset,
                        Length = currentLength
                    });

                    currentOffset = orderedPatches[i].SheetData.Offset;
                    currentLength = originalLength;
                }
                else
                {
                    currentLength += originalLength;
                }
            }
        }

        result.Add(new OvrSectionData
        {
            Offset = currentOffset,
            Length = currentLength
        });

        return result;
    }

    protected void AppendUnusedSpace(List<OvrSectionData> sections, OvrPatchData patchData, bool unlimitedSpace = false)
    {
        OvrSectionData lastSection = sections[^1];

        long sectionEndOffset = lastSection.Offset + lastSection.Length;
        long overlayEndOffset = (patchData.OverlayRange.OverlayBaseAddress + patchData.OverlayRange.OverlaySize + 3) & ~3;
        long overlayMaxEndOffset = (patchData.OverlayRange.OverlayBaseAddress + patchData.OverlayRange.OverlayMaxSize + 3) & ~3;

        if (sectionEndOffset != overlayEndOffset)
        {
            sections.Add(new OvrSectionData
            {
                Offset = overlayEndOffset,
                Length = unlimitedSpace ? -1 : (int)(overlayMaxEndOffset - overlayEndOffset)
            });
        }
        else
        {
            sections[^1] = new OvrSectionData
            {
                Offset = sections[^1].Offset,
                Length = unlimitedSpace ? -1 : (int)(overlayMaxEndOffset - sections[^1].Offset)
            };
        }
    }
}