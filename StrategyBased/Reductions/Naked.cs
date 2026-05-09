namespace StrategyBased.Reductions;

public static class Naked
{
    public static void Pairs(Nodes cells)
    {
        foreach (var set in cells.Rules.Sets)
        {
            var updates = set & cells.Todo;

            foreach (var pair in cells.NakedCells([.. updates], 2))
            {
                foreach (var update in updates ^ pair.Cells)
                    cells[update].Digits ^= pair.Digits;
            }
        }
    }

    public static void Triples(Nodes cells)
    {
        foreach (var set in cells.Rules.Sets)
        {
            var updates = set & cells.Todo;

            foreach (var triple in cells.NakedCells([.. updates], 3))
            {
                foreach (var update in updates ^ triple.Cells)
                    cells[update].Digits ^= triple.Digits;
            }
        }
    }

    public static void Quads(Nodes cells)
    {
        foreach (var set in cells.Rules.Sets)
        {
            var updates = set & cells.Todo;

            foreach (var triple in cells.NakedCells([.. updates], 4))
            {
                foreach (var update in updates ^ triple.Cells)
                    cells[update].Digits ^= triple.Digits;
            }
        }
    }
}
