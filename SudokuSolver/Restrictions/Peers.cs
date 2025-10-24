namespace SudokuSolver.Restrictions;

/// <summary>Represents a cell and its peers (per set).</summary>
public sealed class Peers(Pos appliesTo, PosSet set) : Restriction
{
    public Pos AppliesTo { get; } = appliesTo;

    public PosSet Set { get; } = set;

    /// <inheritdoc />
    public PosSet Links { get; } = set ^ appliesTo;

    /// <inheritdoc />
    public Candidates Restrict(Graph graph) => Candidates._1_to_9;
}
