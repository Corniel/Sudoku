using Sudoku.Parsing;

namespace Sudoku.Common;

public static class Thermometers
{
    public static IEnumerable<Thermometer> Parse(string str)
        => Lines.Parse(str).Select(line => new Thermometer(line));
}
