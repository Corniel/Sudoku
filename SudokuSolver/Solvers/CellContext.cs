namespace SudokuSolver.Solvers;

[Diagnostics.Mutable]
public sealed record CellContext
{
    public CellContext(Pos cell) => Pos = cell;

    public Pos Pos { get; }

    public Candidates Candidates { get; set; } = Candidates.All;

    public ImmutableArray<Pos> Peers { get; set; }

    public ImmutableArray<Restriction> Restrictions { get; init; } = [];

    public override string ToString() => $"{Pos}, {Candidates}, Peers = {Peers.Length}, Restrictions = {Restrictions.Length}";
}
