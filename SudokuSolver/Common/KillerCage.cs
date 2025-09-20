using SudokuSolver.Generics;
using SudokuSolver.Restrictions;
using static SudokuSolver.Common.Arrow;

namespace SudokuSolver.Common;

[SuppressMessage("Clarity", "S4050", Justification = "We only need the - operator")]
public sealed partial class KillerCage(int sum, PosSet cells) : Rule
{
    public int Sum { get; } = sum;

    [Obsolete("Refactor out")]
    public override bool IsSet => true;

    public override PosSet Cells { get; } = cells;

    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Reducers(sum, cells)];

    internal override string DebuggerDisplay => $", Sum = {Sum}";

    public static KillerCage operator -(KillerCage cage, KillerCage other)
        => other.Cells.IsSubsetOf(cage.Cells)
        ? new(cage.Sum - other.Sum, cage.Cells ^ other.Cells)
        : cage;

    private static IEnumerable<Restriction> Reducers(int sum, PosSet cells) => cells.Count switch
    {
        _ when sum is 0 => [],
        1 => [new Cage1(sum, cells.First())],
        9 => [],
        _ => Cages(sum, cells),
    };

    private static IEnumerable<Cage> Cages(int sum, PosSet cells)
    {
        ImmutableArray<Pos> all = [.. cells];

        foreach (Pos pos in all)
        {
            yield return new Cage(sum, pos, all.Remove(pos));
        }
    }

    private sealed class Cage1(int sum, Pos appliesTo) : Restriction
    {
        public Candidates Sum { get; } = [sum];

        public Pos AppliesTo { get; } = appliesTo;

        public Candidates Restrict(Cells cells) => Sum;
    }

    private sealed class Cage(int sum, Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public int Sum { get; } = sum;

        public CandidateLookup<Candidates> Candidates { get; } = Lookup[others.Length + 1][sum];

        public override Candidates Restrict(Cells cells)
        {
            var known = SudokuSolver.Candidates.None;

            foreach (var cell in Others)
                known |= cells[cell];

            return Candidates[known];
        }
    }
}
