using TranslationToSource.Models.Patchers;
using TranslationToSource.Models.Patchers.Layout;

namespace TranslationToSource.Patchers.Layout;

internal class PatchPacker
{
    public (OvrTextPatchData, int)[] Pack(IList<OvrTextPatchData> textPatches, int maxCapacity)
    {
        var result = new List<(OvrTextPatchData, int)>();

        IEnumerable<OvrTextPatchData> orderedPatches = textPatches.OrderByDescending(p => p.Patch.Length);

        var rootNode = new PatchLayoutNode(maxCapacity);
        foreach (OvrTextPatchData patch in orderedPatches)
        {
            PatchLayoutNode? foundNode = FindNode(rootNode, patch.Patch.Length);
            if (foundNode == null)
                continue;

            SplitNode(foundNode, patch.Patch.Length);
            result.Add((patch, foundNode.Index));
        }

        return result.ToArray();
    }

    private PatchLayoutNode? FindNode(PatchLayoutNode node, int value)
    {
        if (!node.IsOccupied)
            return value <= node.Length ? node : null;

        return FindNode(node.RightNode, value);
    }

    private void SplitNode(PatchLayoutNode node, int value)
    {
        node.IsOccupied = true;

        node.RightNode = new PatchLayoutNode(node.Length - value)
        {
            Index = node.Index + value
        };
    }
}