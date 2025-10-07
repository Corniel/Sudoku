using SudokuSolver.Generics;
using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class EntropicLine(ImmutableArray<Pos> cells) : Rule(cells)
{
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
        public override Candidates Restrict(Candidates other) => Lookup[other];

        private static readonly CandidateLookup<Candidates> Lookup = Init(
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
        ]);
    }

    public sealed class Different(Pos appliesTo, Pos other) : Pair(appliesTo, other)
    {
        public override Candidates Restrict(Candidates other) => Lookup[other];

        private static readonly CandidateLookup<Candidates> Lookup = Init(
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
        ]);
    }
}
