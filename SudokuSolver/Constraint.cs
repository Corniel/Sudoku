namespace SudokuSolver;

public sealed record Constraint
{
    public static Constraint None(Pos cell) => new() { Cell = cell };

    /// <summary>The constraint cell.</summary>
    public required Pos Cell { get; init; }

    /// <summary>The candidates that can be considered.</summary>
    public Candidates Candidates { get; init; } = Candidates._1_to_9;

    /// <summary>The set representation of the peers.</summary>
    public PosSet Set { get; init; }

    /// <summary>The array representation of the peers.</summary>
    public ImmutableArray<Pos> Peers { get; init; } = [];

    /// <summary>The (dynamic) restrictions that apply to this cell.</summary>
    public ImmutableArray<Restriction> Restrictions { get; init; } = [];

    public Constraint Solve(int value) => this with { Candidates = Candidates.New(value) };

    public Constraint Reduce(int value) => this with { Candidates = Candidates ^ value };

    public override string ToString() => $"{Cell}, Res = {Restrictions.Length}, Set = {Set.Count} [ {string.Join(", ", Set)} ]";

    public static Constraint operator +(Constraint c, PosSet peers)
    {
        var join = c.Set | peers;
        return c with { Set = join, Peers = [.. join] };
    }

    public static Constraint operator +(Constraint c, Restriction res)
        => c with { Restrictions = c.Restrictions.Add(res) };
}
