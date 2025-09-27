using SudokuSolver.Solvers;
using System.Collections.Concurrent;

namespace SudokuSolver.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public abstract class Pair(Pos appliesTo, Pos other) : Restriction
{
    /// <summary>The cell that is bound to the restriction.</summary>
    public Pos AppliesTo { get; } = appliesTo;

    /// <summary>The other cell that defines the restriction.</summary>
    public Pos Other { get; } = other;

    /// <inheritdoc />
    public abstract double Bits { get; }

    /// <summary>Restricts based on the current allowed candidates.</summary>
    public Candidates Restrict(Context context)
    {
        var candidates = Candidates.None;

        foreach (var value in context[Other].Candidates)
            candidates |= Restrict(value);

        return candidates;
    }

    /// <inheritdoc />
    public Candidates Restrict(Cells cells) => Restrict(cells[Other]);

    /// <inheritdoc cref="Restriction.Restrict(Cells)" />
    protected abstract Candidates Restrict(int value);

    /// <inheritdoc />
    public override string ToString() => $"{AppliesTo} => {Other}";
}
