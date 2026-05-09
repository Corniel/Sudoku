namespace Sudoku.Common;

public static partial class Lines
{
    public static Rules DoubleArrow(string grid)
    {
        return Parse(grid).SelectMany(DoubleArrow);

        static Rules DoubleArrow(Line line)
        {
            var f = line[0];
            var s = line[^1];
            var shaft = line[1..^1];

            return
            [
                new DoubleArrow.End(f, s, shaft),
                new DoubleArrow.End(s, f, shaft),
                .. Group.Select(shaft, (a, o) => new DoubleArrow.Shaft(f, s, a, o))
            ];
        }
    }
}
