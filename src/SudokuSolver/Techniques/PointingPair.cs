namespace SudokuSolver.Techniques;

/// <summary>Reduces pointing pairs.</summary>
/// <remarks>
/// When a certain candidate appears only in two cells in a region, and
/// those cells are both shared with the same other region, they are called
/// a pointing pair. All other appearances of the candidates can be
/// eliminated.
/// </remarks>
public class PointingPair : PointingMultiple
{
    protected override int Size => 2;
}