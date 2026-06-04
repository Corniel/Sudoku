namespace Dlx;

/// <summary>A node in DLX node set.</summary>
/// <remarks>
/// This node keeps track of both left and right and up and down.
/// </remarks>
[Mutable]
[Inheritable]
public class Node
{
    protected Node() : this(default) { }

    public Node(Cell cell)
    {
        Cell = cell;
        L = this;
        R = this;
        U = this;
        D = this;
    }

    public Head Head { get; set; } = null!;

    public Cell Cell { get; }

    /// <summary>Left node.</summary>
    public Node L { get; set; }

    /// <summary>Right node.</summary>
    public Node R { get; set; }

    /// <summary>Up node.</summary>
    public Node U { get; set; }

    /// <summary>Down node.</summary>
    public Node D { get; set; }
}
