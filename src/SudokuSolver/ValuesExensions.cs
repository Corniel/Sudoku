namespace SudokuSolver;

public static class ValuesExensions
{
    public static Values Or(this IEnumerable<Values> puzzle)
    {
        var cell = Values.Invalid;
        foreach(var c in puzzle)
        {
            cell |= c;
        }
        return cell;
    }

    public static IEnumerable<Values> Undecided(this IEnumerable<Values> vals) => vals.Where(v => v.IsUndecided());
}
