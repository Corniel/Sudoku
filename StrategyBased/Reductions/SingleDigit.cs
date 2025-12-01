using Sudoku.Houses;

namespace StrategyBased.Reductions;

public static class SingleDigit
{
    public static void XWing(Nodes nodes)
    {
        Lines.Clear();

        foreach (var row in nodes.Rows)
            Lines.AddRange(Hidden.Row(nodes, row, 2));

        XWing(nodes, GetCol);

        Lines.Clear();

        foreach (var col in nodes.Cols)
            Lines.AddRange(Hidden.Col(nodes, col, 2));

        XWing(nodes, GetRow);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void XWing(Nodes nodes, Func<int, House> getHouse)
    {
        foreach(var (one, two) in Lines.Take2())
        {
            if (one.Digit != two.Digit
                || one.Indexes != two.Indexes) continue;

            var merged = one.Cells | two.Cells;

            foreach (var house in one.Indexes.Select(getHouse))
                foreach (var cell in (house.Cells ^ merged) & nodes.Todo)
                    nodes[cell].Digits ^= one.Digit;
        }
    }

    public static void Skyscraper(Nodes nodes)
    {
        Lines.Clear();

        foreach (var row in nodes.Rows)
            Lines.AddRange(Hidden.Row(nodes, row, 2));

        Skyscraper(nodes, GetCol);

        Lines.Clear();

        foreach (var col in nodes.Cols)
            Lines.AddRange(Hidden.Col(nodes, col, 2));

        Skyscraper(nodes, GetRow);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Skyscraper(Nodes nodes, Func<int, House> getHouse)
    {
        foreach (var (one, two) in Lines.Take2())
        {
            // 1 shared line, 2 not shared lines.
            if (one.Digit == two.Digit && (one.Indexes | two.Indexes) is { Count: 3 } walls)
            {
                var shared = PosSet.Empty;

                foreach (var index in walls ^ (one.Indexes & two.Indexes))
                    shared |= getHouse(index).Cells;

                var others = nodes.Todo ^ (one.Peers | two.Peers);

                foreach (var r in shared & (one.Peers | two.Peers))
                    others &= nodes[r].Peers;

                foreach (var pos in others)
                    nodes[pos].Digits ^= one.Digit;
            }
        }
    }

    public static void TwoStringKite(Nodes nodes)
    {
        Lines.Clear();

        foreach (var row in nodes.Rows)
            Lines.AddRange(Hidden.Row(nodes, row, 2));

        Line2.Clear();

        foreach (var col in nodes.Cols)
            Line2.AddRange(Hidden.Col(nodes, col, 2));

        foreach (var row in Lines)
            foreach (var col in Line2)
                TwoStringKite(nodes, row, col);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void TwoStringKite(Nodes nodes, HiddenCells row, HiddenCells col)
    {
        if (row.Digit != col.Digit || (row.Peers | col.Peers) is not { Count: 4 } kite) return;

        foreach (var house in nodes.Houses.Where(h => h is not Row or Col && (h.Cells & kite).Count is 2))
        {
            foreach (var r in row.Peers)
            {
                foreach (var c in col.Peers)
                {
                    PosSet weak = [r, c];

                    if ((house.Cells & weak).Count is 2)
                    {
                        var others = kite ^ weak;
                        var shared = nodes.Todo ^ weak;

                        foreach (var o in others)
                            shared &= nodes[o].Peers;

                        foreach (var o in shared)
                            nodes[o].Digits ^= row.Digit;
                    }
                }
            }
        }
    }

    public static void Crane(Nodes nodes)
    {
        Lines.Clear();
        Line2.Clear();

        foreach (var row in nodes.Rows)
            Lines.AddRange(Hidden.Row(nodes, row, 2));

        foreach (var col in nodes.Cols)
            Lines.AddRange(Hidden.Col(nodes, col, 2));

        foreach (var house in nodes.Houses.Where(h => h is not Row or Col))
            Line2.AddRange(Hidden.House(nodes, house, 2));

        foreach (var row in Lines)
            foreach (var col in Line2)
                Crane(nodes, row, col);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Crane(Nodes nodes, HiddenCells line, HiddenCells house)
    {
        if (line.Digit != house.Digit || (line.Peers | house.Peers) is not { Count: 4 } crane) return;

        foreach (var ln in line.Peers)
        {
            // The weak link are the cells where the col xor the row allign.
            foreach (var hs in house.Peers.Where(o => o.Row == ln.Row ^ o.Col == ln.Col))
            {
                PosSet weak = [ln, hs];

                var others = crane ^ weak;
                var shared = nodes.Todo ^ weak;

                foreach (var o in others)
                    shared &= nodes[o].Peers;

                foreach (var o in shared)
                    nodes[o].Digits ^= line.Digit;
            }
        }
    }


    public static void Swordfish(Nodes nodes)
    {
        Lines.Clear();

        foreach (var row in nodes.Rows)
            Lines.AddRange(Hidden.Row(nodes, row, 2, 3));

        Swordfish(nodes, GetCol);

        Lines.Clear();

        foreach (var col in nodes.Cols)
            Lines.AddRange(Hidden.Col(nodes, col, 2, 3));

        Swordfish(nodes, GetRow);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swordfish(Nodes nodes, Func<int, House> getHouse)
    {
        foreach (var (one, two, thr) in Lines.Take3())
        {
            if (one.Digit == two.Digit && one.Digit == thr.Digit
                && (one.Indexes | two.Indexes | thr.Indexes) is { Count: 3 } indexes)
            {
                var merged = one.Cells | two.Cells | thr.Cells;

                foreach (var house in indexes.Select(getHouse))
                    foreach (var cell in (house.Cells ^ merged) & nodes.Todo)
                        nodes[cell].Digits ^= one.Digit;
            }
        }
    }

    public static void Jellyfish(Nodes nodes)
    {
        Lines.Clear();

        foreach (var row in nodes.Rows)
            Lines.AddRange(Hidden.Row(nodes, row, 2, 4));

        Jellyfish(nodes, GetCol);

        Lines.Clear();

        foreach (var col in nodes.Cols)
            Lines.AddRange(Hidden.Col(nodes, col, 2, 4));

        Jellyfish(nodes, GetRow);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Jellyfish(Nodes nodes, Func<int, House> getHouse)
    {
        foreach (var (one, two, thr, fur) in Lines.Take4())
        {
            if (one.Digit == two.Digit && one.Digit == thr.Digit && one.Digit == fur.Digit
                && (one.Indexes | two.Indexes | thr.Indexes | fur.Indexes) is { Count: 4} indexes)
            {
                var merged = one.Cells | two.Cells | thr.Cells | fur.Cells;

                foreach (var house in indexes.Select(getHouse))
                    foreach (var cell in (house.Cells ^ merged) & nodes.Todo)
                        nodes[cell].Digits ^= one.Digit;
            }
        }
    }

    private static readonly List<HiddenCells> Lines = [];
    private static readonly List<HiddenCells> Line2 = [];

    private static Col GetCol(int index) => Col.All[index];
    private static Row GetRow(int index) => Row.All[index];
}
