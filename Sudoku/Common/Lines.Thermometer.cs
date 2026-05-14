namespace Sudoku.Common;

public static partial class Lines
{
    public static Rules Thermometer(string grid)
        => Parse(grid).SelectMany(Restrictions.Thermometer.New);

    public static RulesExtender SlowThermometer(string grid) =>
        rules =>
        {
            PosSet[] sets = [.. rules.Sets];
            return rules + Parse(grid).SelectMany(line => Restrictions.Thermometer.New(line, sets));
        };
}
