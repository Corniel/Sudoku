namespace SudokuSolver;

public static class Validator
{
    public static bool IsValid(this IEnumerable<Constraint> constraints, Cells cells)
        => constraints.All(c => c.IsValid(cells));
}
