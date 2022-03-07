namespace SudokuSolver;

public static class CellExtensions
{
    public static IEnumerable<Values> Values(this IEnumerable<Cell> cells) => cells.Select(c => c.Values);

    public static IEnumerable<Cell> Undecided(this IEnumerable<Cell> cells) => cells.Where(c => c.Values.IsUndecided());
}
