namespace Sudoku.Sets;

/// <summary>Contains the common houses.</summary>
public static class Houses
{
    /// <summary>The nine (3x3) boxes.</summary>
    public static readonly ImmutableArray<Box> Boxes = [.. Box.All()];

    /// <summary>The nine columns.</summary>
    public static readonly ImmutableArray<Col> Cols = [.. Col.All()];

    /// <summary>The nine rows.</summary>
    public static readonly ImmutableArray<Row> Rows = [.. Row.All()];

    /// <summary>The four windows available at hyper Sudoku's.</summary>
    public static readonly ImmutableArray<Window> Windows = [.. Window.All()];

    /// <summary>The nine disjoint houses.</summary>
    public static readonly ImmutableArray<Disjoint> Disjoints = [.. Disjoint.All()];
}
