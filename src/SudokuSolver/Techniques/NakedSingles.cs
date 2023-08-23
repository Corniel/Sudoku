namespace SudokuSolver.Techniques;

/// <summary>Reduces (naked) singles.</summary>
/// <remarks>
/// Any puzzle which have only one candidate can safely be assigned that value.
/// 
/// It is very important whenever a value is assigned to a cell, that this
/// value is also excluded as a candidate from all other blank puzzle sharing
/// the same row, column and sub square.
/// </remarks>
public sealed class NakedSingles : Technique
{
    /// <inheritdoc />
    public Puzzle Reduce(Puzzle puzzle, Regions regions)
    {
        foreach (var location in regions.Locations)
        {
            var cell = puzzle[location];
            if (cell.SingleValue())
            {
                foreach (var link in Links[location])
                {
                    puzzle = puzzle.Not(link, cell);
                }
            }
        }
        return puzzle;
    }

    public static readonly IReadOnlyList<IReadOnlyCollection<Location>> Links = Enumerable.Range(0, 81)
            .Select(Location.Index)
            .Select(GetLinks)
            .ToArray();

    private static Location[] GetLinks(Location location)
        => Regions.Default.Where(r => r.Contains(location)).SelectMany(r => r).Where(r => r != location).Distinct().ToArray();
}
