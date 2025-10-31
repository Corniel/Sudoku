namespace Dlx;

/// <summary>Solves a Sudoku using Don Knuth's Dancing Links X algohrithm.</summary>
public static class DlxSolver
{
    public static Cells Solve(Clues clues)
    {
        var nodes = Nodes(clues);
        var rows = new Stack<Node>();
        var cells = Cells.Empty;

        if (!Solve(nodes, rows)) return cells;

        foreach (var cell in rows.Select(r => r.Cell))
            cells[cell.Pos] = cell.Digit;

        return cells;
    }

    private static bool Solve(Nodes nodes, Stack<Node> rows)
    {
        if (nodes.AreSolved) return true;
        if (nodes.NextHeader is not { RowCount: > 0 } header) return false;

        header.Cover();

        for (var row = header.D; row != header; row = row.D)
        {
            for (var col = row.R; col != row; col = col.R)
                col.Head.Cover();

            rows.Push(row);

            if (Solve(nodes, rows)) return true;

            rows.Pop();

            for (var col = row.L; col != row; col = col.L)
                col.Head.Uncover();
        }

        header.Uncover();

        return false;
    }

    /// <summary>Creates a new set of nodes based on the clues.</summary>
    public static Nodes Nodes(Clues clues)
    {
        var nodes = new Nodes();
        var cells = Cells.Empty;

        foreach (var clue in clues)
            cells[clue.Pos] = clue.Digit;

        for (var r = 0; r < _9; r++)
            for (var d = 1; d <= 9; d++)
                nodes.AddHeader(HeadType.Row, r, d);

        for (var c = 0; c < _9; c++)
            for (var d = 1; d <= 9; d++)
                nodes.AddHeader(HeadType.Col, c, d);

        for (var b = 0; b < _9; b++)
            for (var d = 1; d <= 9; d++)
                nodes.AddHeader(HeadType.Box, b, d);

        for (var pos = 0; pos < _9x9; pos++)
            nodes.AddHeader(HeadType.Fill, pos);

        for (var r = 0; r < _9; r++)
        {
            for (var c = 0; c < _9; c++)
            {
                Pos pos = new(r, c);
                var b = (3 * (r / 3)) + (c / 3);

                var min = 1;
                var max = _9;

                var val = cells[pos];
                if (val is not 0)
                {
                    min = val;
                    max = val;
                }

                for (var d = min; d <= max; d++)
                {
                    var cell = new Cell(pos, d);

                    var row = nodes.SetCol(cell, HeadType.Row, r, d);
                    row = nodes.SetCol(cell, HeadType.Col, c, d, row);
                    row = nodes.SetCol(cell, HeadType.Box, b, d, row);
                    _ = nodes.SetCol(cell, HeadType.Fill, pos, 0, row);
                }
            }
        }

        return nodes;
    }
}
