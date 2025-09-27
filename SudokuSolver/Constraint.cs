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

    public double Bits
    {
        get
        {
            if (field is 0)
            {
                var count = Candidates.Count;
                field = Info.Cell(count)
                    + (Info.Peer(count) * Set.Count)
                    + Restrictions.Sum(r => r.Bits);
            }
            return field;
        }
    }

    /// <inheritdoc />
    public override string ToString() => $"{Cell}, {Candidates}, Bits = {Bits:0.000}, Res = {Restrictions.Length}, Peers = {Peers.Length}";

    public static Constraint operator +(Constraint c, PosSet peers)
    {
        var join = c.Set | peers;
        return c with { Set = join, Peers = [.. join] };
    }

    public static Constraint operator +(Constraint c, Restriction res)
        => c with { Restrictions = c.Restrictions.Add(res) };
}
