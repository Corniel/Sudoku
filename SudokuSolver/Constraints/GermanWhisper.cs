namespace SudokuSolver.Constraints;

public sealed class GermanWhisper(ImmutableArray<Pos> cells) : Constraint
{
    public override bool IsSet => false;

    public override PosSet Cells { get; } = [.. cells];

    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Init(cells)];

    private static IEnumerable<Restriction> Init(ImmutableArray<Pos> cells)
    {
        for (var f = 0; f < cells.Length - 1; f++)
        {
            yield return new Neighbors(cells[f + 0], cells[f + 1]);
            yield return new Neighbors(cells[f + 1], cells[f + 0]);

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

    public sealed class Neighbors(Pos appliesTo, Pos neighbor) : Restriction
    {
        public Pos AppliesTo { get; } = appliesTo;

        public Pos Neigbor { get; } = neighbor;

        public override string ToString() => $"{AppliesTo} = {Neigbor} ± 5";

        public Candidates Restrict(Cells cells) => Allowed[cells[Neigbor]];

        private static readonly ImmutableArray<Candidates> Allowed =
        [
            /* ? */ [1, 2, 3, 4, 6, 7, 8, 9],
            /* 1 */ [6, 7, 8, 9],
            /* 2 */ [7, 8, 9],
            /* 3 */ [8, 9],
            /* 4 */ [9],
            /* 5 */ [],
            /* 6 */ [1],
            /* 7 */ [1, 2],
            /* 8 */ [1, 2, 3],
            /* 9 */ [1, 2, 3, 4],
        ];
    }

    public sealed class Toggle(Pos appliesTo, Pos neighbor) : Restriction
    {
        public Pos AppliesTo { get; } = appliesTo;

        public Pos Neigbor { get; } = neighbor;

        public override string ToString() => $"Toggle: {AppliesTo}, {Neigbor}";

        public Candidates Restrict(Cells cells) => Allowed[cells[Neigbor]];

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

    public sealed class NoToggle(Pos appliesTo, Pos neighbor) : Restriction
    {
        public Pos AppliesTo { get; } = appliesTo;

        public Pos Neigbor { get; } = neighbor;

        public override string ToString() => $"No toggle: {AppliesTo}, {Neigbor}";

        public Candidates Restrict(Cells cells) => Allowed[cells[Neigbor]];

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
