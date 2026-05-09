namespace Sudoku.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public abstract class Group(Pos appliesTo, PosArray others) : Restriction
{
    /// <inheritdoc />
    public Pos AppliesTo { get; } = appliesTo;

    /// <summary>The other cell that defines the restriction.</summary>
    public PosArray Others { get; } = others;

    /// <inheritdoc />
    public virtual PosSet Cells { get; } = [appliesTo, .. others];

    /// <inheritdoc />
    public abstract Digits Restrict(SudokuCells cells);

    /// <inheritdoc />
    public override string ToString() => $"({GetType().Name}) {AppliesTo} => [{string.Join(',', Others)}]";

    [Pure]
    public static IEnumerable<T> Select<T>(Line line, Func<Pos, PosArray, T> selector) => Select(line.Cells, selector);

    [Pure]
    public static IEnumerable<T> Select<T>(Rule rule, Func<Pos, PosArray, T> selector) => Select(rule.Cells, selector);

    [Pure]
    [OverloadResolutionPriority(10)]
    public static IEnumerable<T> Select<T>(PosSet positions, Func<Pos, PosArray, T> selector) => Select(positions.ToImmutableArray(), selector);

    [Pure]
    public static IEnumerable<T> Select<T>(PosArray positions, Func<Pos, PosArray, T> selector)
        => positions
        .Select((pos, index) => selector(pos, positions.RemoveAt(index)));
}
