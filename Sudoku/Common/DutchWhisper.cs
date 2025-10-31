using Sudoku.Generics;
using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class DutchWhisper(ImmutableArray<Pos> cells) : Rule(cells)
{
    public override ImmutableArray<Restriction> Restrictions { get; } = [..Init(cells)];

    private static IEnumerable<Neighbors> Init(ImmutableArray<Pos> cells)
    {
        for (var f = 0; f < cells.Length - 1; f++)
        {
            for (var s = f + 1; s <= f + 3 && s < cells.Length; s++)
            {
                yield return new Neighbors(cells[f], cells[s], s - f - 1);
                yield return new Neighbors(cells[s], cells[f], s - f - 1);
            }
        }
    }

    public sealed class Neighbors(Pos appliesTo, Pos neighbor, int skip) : Pair(appliesTo, neighbor)
    {
        public int Skip { get; } = skip;

        public override string ToString() => $"{AppliesTo} = {Other} ± 4 ({Skip})";

        public override Digits Restrict(Digits other) => Allowed[Skip][other];

        private static readonly ImmutableArray<DigitLookup<Digits>> Allowed =
        [
            Init([ // Skip 0
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
            Init([ // Skip 1
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
            Init([ // Skip 2
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
}
