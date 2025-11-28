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

    public static void TwoStringKite(Nodes nodes) { }

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

    private static Col GetCol(int index) => Col.All[index];
    private static Row GetRow(int index) => Row.All[index];
}
