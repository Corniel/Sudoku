using SudokuSolver.Parsing;
using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class Parity(ImmutableArray<Pos> evens, ImmutableArray<Pos> odds) : Rule
{
    public override bool IsSet => false;

    public override PosSet Cells { get; } = [.. evens, .. odds];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        .. evens.Select(c => new Mask(c, Candidates.Even)),
        .. odds.Select(c => new Mask(c, Candidates.Odd)),
    ];

    public static Parity Parse(string str)
    {
        var cages = NamedCage.Parse(str);
        var even = cages.FirstOrDefault(c => c.Name is 'E')?.Cells ?? [];
        var odd = cages.FirstOrDefault(c => c.Name is 'O')?.Cells ?? [];
        return new(even, odd);
    }
}
