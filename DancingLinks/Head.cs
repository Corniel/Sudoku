namespace DancingLinks;

/// <summary>Represents a header node.</summary>
[Mutable]
public sealed class Head : Node
{
    /// <summary>A root header.</summary>
    public static Head Root => new();

    private Head() { Type = HeadType.Root; }

    public Head(HeadType type, int index, int value, Head root)
    {
        Type = type;
        Index = index;
        Value = value;

        Head = this;

        U = this;
        D = this;

        L = root.L;
        R = root;

        root.L.R = this;
        root.L = this;
    }

    /// <summary>The type of the header/column constraint.</summary>
    public HeadType Type { get; }

    /// <summary>The index (row, col, box, or position).</summary>
    public int Index { get; }

    /// <summary>The value (0 for fill).</summary>
    public int Value { get; }

    /// <summary>The number of rows for the header.</summary>
    public int RowCount { get; set; }

    public void Cover()
    {
        R.L = L;
        L.R = R;
        for (var d = D; d != this; d = d.D)
        {
            for (var r = d.R; r != d; r = r.R)
            {
                r.D.U = r.U;
                r.U.D = r.D;
                r.Head.RowCount--;
            }
        }
    }

    public void Uncover()
    {
        for (var u = U; u != this; u = u.U)
        {
            for (var l = u.L; l != u; l = l.L)
            {
                l.Head.RowCount++;
                l.D.U = l;
                l.U.D = l;
            }
        }
        R.L = this;
        L.R = this;
    }
}
