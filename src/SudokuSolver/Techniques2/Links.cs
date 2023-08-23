namespace SudokuSolver.Techniques2;

public static class Links
{
    public static readonly IReadOnlyList<IReadOnlyCollection<Location>> All = Enumerable.Range(0, 81)
           .Select(Location.Index)
           .Select(GetAll)
           .ToArray();

    public static readonly IReadOnlyList<IReadOnlyCollection<Location>> Rows = Enumerable.Range(0, 81)
           .Select(Location.Index)
           .Select(GetRows)
           .ToArray();

    public static readonly IReadOnlyList<IReadOnlyCollection<Location>> Columns = Enumerable.Range(0, 81)
           .Select(Location.Index)
           .Select(GetColumns)
           .ToArray();

    public static readonly IReadOnlyList<IReadOnlyCollection<Location>> Squares = Enumerable.Range(0, 81)
           .Select(Location.Index)
           .Select(GetSquares)
           .ToArray();

    private static Location[] GetAll(Location location) => Get(location, _ => true);
    private static Location[] GetRows(Location location) => Get(location, r => r.Type == RegionType.Row);
    private static Location[] GetColumns(Location location) => Get(location, r => r.Type == RegionType.Column);
    private static Location[] GetSquares(Location location) => Get(location, r => r.Type == RegionType.Square);

    private static Location[] Get(Location location, Predicate<Region> predicate)
       => Regions.Default
           .Where(r => r.Contains(location) && predicate(r))
           .SelectMany(r => r)
           .Where(r => r != location)
           .Distinct()
           .ToArray();
}
