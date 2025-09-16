using SudokuSolver.Parsing;

namespace SudokuSolver.Constraints;

public sealed class Parity(ImmutableArray<Pos> evens, ImmutableArray<Pos> odds) : Constraint
{
    public override bool IsSet => false;

    public override PosSet Cells { get; } = [.. evens, .. odds];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        .. evens.Select(c => new Even(c)),
        .. odds.Select(c => new Odd(c)),
    ];

    public override string ToString()
    {
        var sb = new StringBuilder();
        Pos[] e = [.. Restrictions.OfType<Even>().Select(r => r.AppliesTo)];
        Pos[] o = [.. Restrictions.OfType<Odd>().Select(r => r.AppliesTo)];
        if (e.Length > 0)
        {
            sb.Append($"Even: {string.Join(", ", e)}");
        }
        if (o.Length > 0)
        {
            if (sb.Length > 0) sb.Append(", ");
            sb.Append($"Odd: {string.Join(", ", o)}");
        }
        return sb.ToString();
    }

    public sealed class Even(Pos cell) : Restriction
    {
        public Pos AppliesTo { get; } = cell;

        public Candidates Restrict(Cells cells) => Candidates.Even;

        public override string ToString() => $"{AppliesTo} is even";
    }

    public sealed class Odd(Pos cell) : Restriction
    {
        public Pos AppliesTo { get; } = cell;

        public Candidates Restrict(Cells cells) => Candidates.Odd;

        public override string ToString() => $"{AppliesTo} is odd";
    }

    public static Parity Parse(string str)
    {
        var cages = NamedCage.Parse(str);
        var even = cages.FirstOrDefault(c => c.Name is 'E')?.Cells ?? [];
        var odd = cages.FirstOrDefault(c => c.Name is 'O')?.Cells ?? [];
        return new(even, odd);
    }
}
