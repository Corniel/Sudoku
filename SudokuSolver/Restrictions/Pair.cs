using Sudoku.Generics;

namespace SudokuSolver.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public abstract class Pair(Pos appliesTo, Pos other) : Restriction
{
    /// <inheritdoc />
    public Pos AppliesTo { get; } = appliesTo;

    /// <summary>The other cell that defines the restriction.</summary>
    public Pos Other { get; } = other;

    /// <inheritdoc />
    public PosSet Links { get; } = [other];

    /// <inheritdoc />
    public Candidates Restrict(Graph graph) => Restrict(graph.Test(Other));

    /// <inheritdoc cref="Restriction.Restrict(Graph)" />
    public virtual Candidates Restrict(Candidates other)
    {
        var candidates = Candidates.None;

        foreach (var val in other)
            candidates |= Restrict(val);

        return candidates;
    }

    /// <inheritdoc cref="Restriction.Restrict(Graph)" />
    public virtual Candidates Restrict(int value) => Restrict([value]);

    /// <inheritdoc />
    public override string ToString() => $"{AppliesTo} => {Other}";

    public static CandidateLookup<Candidates> Init(Candidates[] byValue)
    {
        var lookup = new CandidateLookup<Candidates>();

        foreach (var candidates in Candidates.All)
            foreach (var value in candidates)
                lookup[candidates] |= byValue[value];

        return lookup;
    }
}
