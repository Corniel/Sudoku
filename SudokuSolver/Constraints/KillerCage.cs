using SudokuSolver.Generics;

namespace SudokuSolver.Constraints;

[SuppressMessage("Clarity", "S4050", Justification = "We only need the - operator")]
public sealed partial class KillerCage(int sum, PosSet cells) : Constraint
{
    public int Sum { get; } = sum;

    public override bool IsSet => true;

    public override PosSet Cells => cells;

    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Reducers(sum, cells)];

    public static KillerCage operator -(KillerCage cage, KillerCage other)
        => cage.Cells.IsSubsetOf(other.Cells)
        ? new(cage.Sum - other.Sum, cage.Cells ^ other.Cells)
        : cage;

    private static IEnumerable<Restriction> Reducers(int sum, PosSet cells) => cells.Count switch
    {
        1 => [new Cage1(sum, cells.First())],
        2 => [new Cage2(sum, cells.First(), cells.Skip(1).First())],
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

    private sealed class Cage2(int sum, Pos appliesTo, Pos other) : Restriction
    {
        public int Sum { get; } = sum;

        public Pos AppliesTo { get; } = appliesTo;

        public Pos Other { get; } = other;

        public Candidates Restrict(Cells cells)
        {
            var value = cells[Other];

            return value is 0
                ? Candidates.AtMost(Sum - 1)
                : Candidates.New(Sum - value);
        }
    }

    private sealed class Cage(int sum, Pos appliesTo, ImmutableArray<Pos> other) : Restriction
    {
        public int Sum { get; } = sum;

        public Pos AppliesTo { get; } = appliesTo;

        public ImmutableArray<Pos> Other { get; } = other;

        public CandidateLookup<Candidates> Candidates { get; } = Lookup[other.Length + 1][sum];

        public Candidates Restrict(Cells cells)
        {
            var known = SudokuSolver.Candidates.None;

            foreach (var cell in Other)
                known |= cells[cell];

            return Candidates[known];
        }
    }
}
