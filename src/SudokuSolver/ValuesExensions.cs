namespace SudokuSolver;

public static class ValuesExensions
{
    public static Values Or(this IEnumerable<Values> cells)
    {
        var cell = Values.Invalid;
        foreach(var c in cells)
        {
            cell |= c;
        }
        return cell;
    }
}
