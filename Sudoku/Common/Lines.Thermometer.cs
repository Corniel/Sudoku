namespace Sudoku.Common;

public static partial class Lines
{
    public static Rules Thermometer(string grid)
        => Parse(grid).SelectMany(Restrictions.Thermometer.New);
}
