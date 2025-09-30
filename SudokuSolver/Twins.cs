using SudokuSolver.Restrictions;

namespace SudokuSolver;

public sealed class Twins(Pos one, Pos two) : Rule(one, two)
{
    public override ImmutableArray<Restriction> Restrictions { get; } = [new Twin(one, two), new Twin(two, one)];
}
