namespace SudokuSolver.Techniques;

/// <summary>Reduces pointing pairs.</summary>
/// <remarks>
/// When a certain candidate appears only in two three in a region, and
/// those puzzle are all shared with the same other region, they are called
/// a pointing triple. All other appearances of the candidates can be
/// eliminated.
/// </remarks>
public class PointingTriple : PointingMultiple
{
    protected override int Size => 3;
}