using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationToSource.Models.Patchers.Layout;

internal class PatchLayoutNode
{
    /// <summary>
    /// The length of the node.
    /// </summary>
    public int Length{ get; }

    /// <summary>
    /// The index this node is set in the area.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Is this node is already occupied by an area.
    /// </summary>
    public bool IsOccupied { get; set; }

    /// <summary>
    /// The right headed node.
    /// </summary>
    public PatchLayoutNode RightNode { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="PatchLayoutNode"/>.
    /// </summary>
    /// <param name="length"></param>
    public PatchLayoutNode(int length)
    {
        Length = length;
    }
}