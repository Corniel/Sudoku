using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class EntropicLine(ImmutableArray<Pos> cells) : Rule
{
    public override bool IsSet => false;

    public override PosSet Cells { get; } = [.. cells];

    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Init(cells)];

    private static IEnumerable<Restriction> Init(ImmutableArray<Pos> cells)
    {
        for (var f = 0; f < cells.Length - 1; f++)
        {
            for (var s = f + 1; s < cells.Length; s++)
            {
                if ((s - f) % 3 == 0)
                {
                    yield return new Same(cells[f], cells[s]);
                    yield return new Same(cells[s], cells[f]);
                }
                else
                {
                    yield return new Different(cells[f], cells[s]);
                    yield return new Different(cells[s], cells[f]);
                }
            }
        }
    }

    public sealed class Same(Pos appliesTo, Pos other) : Pair(appliesTo, other)
    {
        public override double Bits => Info.Avg(3);

        public override Candidates Restrict(int value) => Lookup[value];

        private static readonly ImmutableArray<Candidates> Lookup =
        [
            Candidates._1_to_9,

            Candidates._123,
            Candidates._123,
            Candidates._123,

            Candidates._456,
            Candidates._456,
            Candidates._456,

            Candidates._789,
            Candidates._789,
            Candidates._789,
        ];
    }

    public sealed class Different(Pos appliesTo, Pos other) : Pair(appliesTo, other)
    {
        public override double Bits => Info.Avg(6);

        public override Candidates Restrict(int value) => Lookup[value];

        private static readonly ImmutableArray<Candidates> Lookup =
        [
            Candidates._1_to_9,

            ~Candidates._123,
            ~Candidates._123,
            ~Candidates._123,

            ~Candidates._456,
            ~Candidates._456,
            ~Candidates._456,

            ~Candidates._789,
            ~Candidates._789,
            ~Candidates._789,
        ];
    }
}
