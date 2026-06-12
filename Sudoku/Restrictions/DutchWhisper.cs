namespace Sudoku.Restrictions;

public static class DutchWhisper
{
    public static Rules New(Domino domino)
        => new LookupPair(domino.A, domino.B, Lookups[0]).Couple();

    public static Rules New(Line line)
    {
        for (var f = 0; f < line.Length - 1; f++)
        {
            for (var s = f + 1; s <= f + 3 && s < line.Length; s++)
            {
                var pairs = new LookupPair(line[f], line[s], Lookups[s - f - 1]).Couple();
                yield return pairs.One;
                yield return pairs.Two;
            }
        }
    }

    private static readonly ImmutableArray<LookupDigits> Lookups =
    [
        LookupPair.Init([ // Skip 0
            /* ? */ [1,2,3,4,5,6,7,8,9],
            /* 1 */ [5,6,7,8,9],
            /* 2 */ [6,7,8,9],
            /* 3 */ [7,8,9],
            /* 4 */ [8,9],
            /* 5 */ [1,9],
            /* 6 */ [1,2],
            /* 7 */ [1,2,3],
            /* 8 */ [1,2,3,4],
            /* 9 */ [1,2,3,4,5],
        ]),
        LookupPair.Init([ // Skip 1
            /* ? */ [1,2,3,4,5,6,7,8,9],
            /* 1 */ [1,2,3,4,5,9],
            /* 2 */ [1,2,3,4,5],
            /* 3 */ [1,2,3,4,5],
            /* 4 */ [1,2,3,4,5],
            /* 5 */ [1,2,3,4,5,6,7,8,9],
            /* 6 */ [5,6,7,8,9],
            /* 7 */ [5,6,7,8,9],
            /* 8 */ [5,6,7,8,9],
            /* 9 */ [1,5,6,7,8,9],
        ]),
        LookupPair.Init([ // Skip 2
            /* ? */ [1,2,3,4,5,6,7,8,9],
            /* 1 */ [1,2,3,4,5,6,7,8,9],
            /* 2 */ [1,5,6,7,8,9],
            /* 3 */ [1,5,6,7,8,9],
            /* 4 */ [1,5,6,7,8,9],
            /* 5 */ [1,2,3,4,5,6,7,8,9],
            /* 6 */ [1,2,3,4,5,9],
            /* 7 */ [1,2,3,4,5,9],
            /* 8 */ [1,2,3,4,5,9],
            /* 9 */ [1,2,3,4,5,6,7,8,9],
        ]),
    ];
}
