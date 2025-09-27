using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

/// <summary>
/// A themometer descibes a path where every next cell has to have a higher
/// value then all previous ones.
/// </summary>
public sealed class Thermometer(ImmutableArray<Pos> path) : Rule
{
    public override bool IsSet => true;

    public override PosSet Cells { get; } = [.. path];

    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Reducers(path)];

    public static Thermometer Parse(string str)
    {
        var path = Clues.Parse(str);
        return new([.. path.OrderBy(c => c.Value).Select(c => c.Pos)]);
    }

    private static IEnumerable<Restriction> Reducers(ImmutableArray<Pos> path)
    {
        for (var f = 0; f < path.Length - 1; f++)
        {
            for (var s = f + 1; s < path.Length; s++)
            {
                yield return new Less(path[f], path[s], s - f);
                yield return new More(path[s], path[f], s - f);
            }
        }
    }

    [DebuggerDisplay("{AppliesTo} <= {Other} - {Delta}")]
    public sealed class Less(Pos appliesTo, Pos other, int delta) : Pair(appliesTo, other)
    {
        public int Delta { get; } = delta;

        public override double Bits => Info.Avg(9 - Delta);

        public override Candidates Restrict(Cells cells)
        {
            var value = cells[Other];
            return Candidates.AtMost((value is 0 ? _9 : value) - Delta);
        }
    }

    [DebuggerDisplay("{AppliesTo} >= {Other} + {Delta}")]
    public sealed class More(Pos appliesTo, Pos other, int delta) : Pair(appliesTo, other)
    {
        public override double Bits => Info.Avg(9 - Delta);

        public int Delta { get; } = delta;

        public override Candidates Restrict(Cells cells)
        {
            var value = cells[Other];
            return Candidates.AtLeast((value is 0 ? 1 : value) + Delta);
        }
    }
}
