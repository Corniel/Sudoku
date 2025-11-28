using Sudoku.Houses;

namespace StrategyBased.Reductions;

public static partial class Hidden
{
    public static IEnumerable<HiddenCells> Row(Nodes nodes, Row row, int min, int? max = null)
    {
        Array.Clear(Indexes);
        Array.Clear(PosSets);

        max ??= min;

        foreach (var cell in row.Cells & nodes.Todo)
        {
            foreach (var digit in nodes[cell].Digits)
            {
                Indexes[digit] |= cell.Col;
                PosSets[digit] |= cell;
            }
        }

        return Indexes.Select((indexes, digit) => new HiddenCells
        {
            Digit = digit,
            Index = row.Index,
            Indexes = indexes,
            Cells = row.Cells,
            Peers = PosSets[digit],
        })
        .Where(hc => hc.Indexes.Count >= min && hc.Indexes.Count <= max);
    }

    public static IEnumerable<HiddenCells> Col(Nodes nodes, Col col, int min, int? max = null)
    {
        Array.Clear(Indexes);
        Array.Clear(PosSets);

        max ??= min;

        foreach (var cell in col.Cells & nodes.Todo)
        {
            foreach (var digit in nodes[cell].Digits)
            {
                Indexes[digit] |= cell.Row;
                PosSets[digit] |= cell;
            }
        }

        return Indexes.Select((indexes, digit) => new HiddenCells
        {
            Digit = digit,
            Index = col.Index,
            Indexes = indexes,
            Cells = col.Cells,
            Peers = PosSets[digit],
        })
        .Where(hc => hc.Indexes.Count >= min && hc.Indexes.Count <= max);
    }

    public static IEnumerable<HiddenCells> House(Nodes nodes, Rule house, int min, int? max = null)
    {
        if (house is Row row) return Row(nodes, row, min, max);
        if (house is Col col) return Col(nodes, col, min, max);

        Array.Clear(Indexes);
        Array.Clear(PosSets);

        max ??= min;
        var index = 0;

        foreach (var cell in house.Cells)
        {
            if (nodes.Todo.Contains(cell))
            {
                foreach (var digit in nodes[cell].Digits)
                {
                    Indexes[digit] |= cell.Row;
                    PosSets[digit] |= cell;
                }
            }
            index++;
        }

        return Indexes.Select((indexes, digit) => new HiddenCells
        {
            Digit = digit,
            Index = -1,
            Indexes = indexes,
            Cells = house.Cells,
            Peers = PosSets[digit],
        })
        .Where(hc => hc.Peers.Count >= min && hc.Peers.Count <= max);
    }

    private static readonly Indexes[] Indexes = new Indexes[_9 + 1];
    private static readonly PosSet[] PosSets = new PosSet[_9 + 1];
}
