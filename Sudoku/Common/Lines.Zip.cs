namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>A zip line contains pairs of cells that sum to the center cell.</summary>
    public static Rules Zip(string grid)
    {
        return Parse(grid).SelectMany(Zip);

        static Rules Zip(Line line)
        {
            if (line.Length.IsEven())
                throw new InvalidConstraint($"ZipLine {line.Name} has an even length of {line.Length}");

            var mid = line.Length / 2;
            var circle = line[mid];

            for (var i = 0; i < mid; i++)
            {
                var (one, two) = (line[i], line[^(i + 1)]);

                yield return new Arrow.Circle(circle, [one, two]);
                yield return new Arrow.Shaft(circle, one, [two]);
                yield return new Arrow.Shaft(circle, two, [one]);
            }
        }
    }
}
