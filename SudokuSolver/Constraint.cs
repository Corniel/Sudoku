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

    /// <summary>Validates that the digits in the cell are compliant with the constraint.</summary>
    public bool IsValid(Cells cells)
    {
        if (IsSet)
        {
            var values = Candidates.None;
            foreach (var cell in Cells)
            {
                values |= cells[cell];
            }
            if (values.Count != Count) return false;
        }

        foreach (var res in Restrictions)
        {
            Candidates value = [cells[res.AppliesTo]];

            if ((value & res.Restrict(cells)).HasNone) return false;
        }
        return true;
    }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator()" />
    public PosSet.Iterator GetEnumerator() => Cells.GetEnumerator();

    /// <inheritdoc />
    IEnumerator<Pos> IEnumerable<Pos>.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal virtual string DebuggerDisplay => IsSet ? " (set)" : string.Empty;
}
