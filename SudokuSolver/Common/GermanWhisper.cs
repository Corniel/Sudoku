using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class GermanWhisper(ImmutableArray<Pos> cells) : Rule
{
    public override bool IsSet => false;

    public override PosSet Cells { get; } = [.. cells];

    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Init(cells)];

    private static IEnumerable<Restriction> Init(ImmutableArray<Pos> cells)
    {
        for (var f = 0; f < cells.Length - 1; f++)
        {
            yield return new DeltaMin(cells[f + 0], cells[f + 1], 5);
            yield return new DeltaMin(cells[f + 1], cells[f + 0], 5);

            for (var s = f + 2; s < cells.Length; s++)
            {
                if (((s - f) & 1) == 0)
                {
                    yield return new NoToggle(cells[f], cells[s]);
                    yield return new NoToggle(cells[s], cells[f]);
                }
                else
                {
                    yield return new Toggle(cells[f], cells[s]);
                    yield return new Toggle(cells[s], cells[f]);
                }
            }
        }
    }

    public override string ToString() => $"German whispers = {string.Join(", ", Cells)}";

    private sealed class Toggle(Pos appliesTo, Pos neighbor) : Pair(appliesTo, neighbor)
    {
        public override double Bits { get; } = Info.Avg(4);

        public override string ToString() => $"Toggle: {AppliesTo}, {Other}";

        public override Candidates Restrict(int value) => Allowed[value];

        private static readonly ImmutableArray<Candidates> Allowed =
        [
            /* ? */ [1, 2, 3, 4, 6, 7, 8, 9],
            /* 1 */ [6, 7, 8, 9],
            /* 2 */ [6, 7, 8, 9],
            /* 3 */ [6, 7, 8, 9],
            /* 4 */ [6, 7, 8, 9],
            /* 5 */ [],
            /* 6 */ [1, 2, 3, 4],
            /* 7 */ [1, 2, 3, 4],
            /* 8 */ [1, 2, 3, 4],
            /* 9 */ [1, 2, 3, 4],
        ];
    }

    private sealed class NoToggle(Pos appliesTo, Pos neighbor) : Pair(appliesTo, neighbor)
    {
        public override double Bits { get; } = Info.Avg(4);

        public override string ToString() => $"No toggle: {AppliesTo}, {Other}";

        public override Candidates Restrict(int value) => Allowed[value];

        private static readonly ImmutableArray<Candidates> Allowed =
        [
            /* ? */ [1, 2, 3, 4, 6, 7, 8, 9],
            /* 1 */ [1, 2, 3, 4],
            /* 2 */ [1, 2, 3, 4],
            /* 3 */ [1, 2, 3, 4],
            /* 4 */ [1, 2, 3, 4],
            /* 5 */ [],
            /* 6 */ [6, 7, 8, 9],
            /* 7 */ [6, 7, 8, 9],
            /* 8 */ [6, 7, 8, 9],
            /* 9 */ [6, 7, 8, 9],
        ];
    }
}
