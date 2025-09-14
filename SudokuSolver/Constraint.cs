using SudokuSolver.Validation;

namespace SudokuSolver;

/// <summary>A constraint.</summary>
[DebuggerDisplay("{GetType().Name}{DebuggerDisplay}, Count = {Count}, Restrictions = {Restrictions.Length}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public abstract class Constraint : IReadOnlyCollection<Pos>
{
    /// <summary>
    /// Indicates that all cells are part of the same set, and therefor must
    /// have different values.
    /// </summary>
    public abstract bool IsSet { get; }

    /// <summary>The cells bound to the constraint.</summary>
    public abstract PosSet Cells { get; }

    /// <inheritdoc />
    public int Count => Cells.Count;

    /// <summary>The restrictions (other than being peers).</summary>
    public abstract ImmutableArray<Restriction> Restrictions { get; }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator()" />
    public PosSet.Iterator GetEnumerator() => Cells.GetEnumerator();

    /// <inheritdoc />
    IEnumerator<Pos> IEnumerable<Pos>.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal virtual string DebuggerDisplay => IsSet ? " (set)" : string.Empty;
}
