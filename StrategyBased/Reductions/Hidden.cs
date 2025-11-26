namespace StrategyBased.Reductions;

public static class Hidden
{
    public static void Single(Nodes cells)
    {
        foreach (var house in cells.Houses)
        {
            var count = cells.Assignments(house.Cells);

            for (var val = 1; val <= _9; val++)
                if (count[val] is { HasSingle: true } single)
                    cells[single.First()].Digits = [val];
        }
    }

    public static void Pairs(Nodes cells)
    {
        foreach (var house in cells.Houses)
            Pair(house, cells);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Pair(Rule house, Nodes cells)
    {
        var assignments = cells.Assignments(house.Cells);

        foreach (var digits in Combinations.Take2(assignments.WithMax(2))
            .Select(pair => Digits.New(pair.One, pair.Two)))
        {
            var pair = PosSet.Empty;

            foreach (var value in digits)
                pair |= assignments[value];

            if (pair.Count is 2)
            {
                var others = (house.Cells & cells.Todo) ^ pair;

                foreach (var update in pair)
                    cells[update].Digits &= digits;

                foreach (var update in others)
                    cells[update].Digits ^= digits;
            }
        }
    }

    public static void Triples(Nodes cells)
    {
        foreach (var house in cells.Houses)
            Triple(house, cells);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Triple(Rule house, Nodes cells)
    {
        var assignments = cells.Assignments(house.Cells);

        foreach (var digits in Combinations.Take3(assignments.WithMax(3))
            .Select(triple => Digits.New(triple.One, triple.Two, triple.Thr)))
        {
            var triple = PosSet.Empty;

            foreach (var value in digits)
                triple |= assignments[value];

            if (triple.Count is 3)
            {
                var others = (house.Cells & cells.Todo) ^ triple;

                foreach (var update in triple)
                    cells[update].Digits &= digits;

                foreach (var update in others)
                    cells[update].Digits ^= digits;
            }
        }
    }

    public static void Quads(Nodes cells)
    {
        foreach (var house in cells.Houses)
            Quad(house, cells);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Quad(Rule house, Nodes cells)
    {
        var assignments = cells.Assignments(house.Cells);

        foreach (var digits in Combinations.Take4(assignments.WithMax(4))
            .Select(quad => Digits.New(quad.One, quad.Two, quad.Thr, quad.For)))
        {
            var quad = PosSet.Empty;

            foreach (var value in digits)
                quad |= assignments[value];

            if (quad.Count is 4)
            {
                var others = (house.Cells & cells.Todo) ^ quad;

                foreach (var update in quad)
                    cells[update].Digits &= digits;

                foreach (var update in others)
                    cells[update].Digits ^= digits;
            }
        }
    }
}
