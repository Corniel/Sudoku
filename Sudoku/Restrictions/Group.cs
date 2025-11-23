namespace Sudoku.Restrictions;

/// <summary>Describes a restriction between two cells.</summary>
public abstract class Group(Pos appliesTo, ImmutableArray<Pos> others) : Restriction
{
    /// <inheritdoc />
    public Pos AppliesTo { get; } = appliesTo;

    /// <summary>The other cell that defines the restriction.</summary>
    public ImmutableArray<Pos> Others { get; } = others;

    /// <inheritdoc />
    public virtual PosSet Links { get; } = [.. others];

    /// <inheritdoc />
    public abstract Digits Restrict(SudokuCells cells);

    /// <inheritdoc />
    public override string ToString() => $"({GetType().Name}) {AppliesTo} => [{string.Join(',', Others)}]";

    public static IEnumerable<T> Select<T>(PosSet positions, Func<Pos, ImmutableArray<Pos>, T> selector) => Select(positions.ToImmutableArray(), selector);

    public static IEnumerable<T> Select<T>(ImmutableArray<Pos> positions, Func<Pos, ImmutableArray<Pos>, T> selector)
        => positions
        .Select((pos, index) => selector(pos, positions.RemoveAt(index)));
}
