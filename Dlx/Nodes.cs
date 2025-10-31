namespace Dlx;

[Mutable]
public sealed class Nodes
{
    public Head Root { get; } = Head.Root;

    /// <summary>Gets the header with the lowest row count.</summary>
    /// <remarks>
    /// We stop if we have a row count of 1 (or 0). This can lead to continuing
    /// while there is an inconsistancy, but in these cases are so rare, that
    /// this is faster.
    /// </remarks>
    public Head NextHeader
    {
        get
        {
            var best = int.MaxValue;
            var smll = Root;
            var curr = (Head)Root.R;

            while (curr != Root && best > 1)
            {
                if (curr.RowCount < best)
                {
                    smll = curr;
                    best = curr.RowCount;
                }
                curr = (Head)curr.R;
            }
            return smll;
        }
    }

    /// <summary>Indicates that the nodes have been solved to a solution.</summary>
    public bool AreSolved => Root == Root.R;

    /// <summary>Adds a header.</summary>
    public void AddHeader(HeadType type, int index, int value = 0)
        => Headers[(int)type][index][value] = new(type, index, value, Root);

    /// <summary>Set a column as attached on the current row.</summary>
    public Node SetCol(Cell cell, HeadType type, int index, int value, Node? row = null)
    {
        var head = Headers[(int)type][index][value];
        var node = new Node() { Cell = cell };
        row ??= node;
        node.L = row;
        node.R = row.R;
        row.R.L = node;
        row.R = node;
        node.Head = head;
        node.U = head;
        node.D = head.D;
        head.D.U = node;
        head.D = node;
        head.RowCount++;
        return node;
    }

    /// <summary>A lookup for the defined headers.</summary>
    /// <remarks>
    /// Specialized lookup to minimize the access times.
    /// </remarks>
    private readonly Head[][][] Headers = Init();

    private static Head[][][] Init()
    {
        var headers = new Head[1 + (int)HeadType.Fill][][];
        headers[(int)HeadType.Fill] = new Head[_9x9][];

        for (var type = HeadType.Row; type <= HeadType.Box; type++)
        {
            headers[(int)type] = new Head[_9][];
            for (var index = 0; index < _9; index++)
                headers[(int)type][index] = new Head[1 + _9];
        }

        for (var index = 0; index < _9x9; index++)
            headers[(int)HeadType.Fill][index] = new Head[1];

        return headers;
    }
}
