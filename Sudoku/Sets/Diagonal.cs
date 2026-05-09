namespace Sudoku.Sets;

public static class Diagonal
{
    /// <summary>NW-SE [(0, 0), (0, 1) ..] diagonal.</summary>
    public static readonly CellSet NW_SE = new(PosSet.New(range().Select(i => pos(i, i))), "NW-SE");

    /// <summary>NE-SW [(0, 8), (1, 7) ..] diagonal.</summary>
    public static readonly CellSet NE_SW = new(PosSet.New(range().Select(i => pos(i, _9 - i - 1))), "NE-SW");

    public static Rules Sum(int sum, Pos first, Pos last)
    {
        PosSet cells = [first, last];

        var (dr, dc) = (last.Row - first.Row, last.Col - first.Col);

        if (Math.Abs(dr) != Math.Abs(dc)) throw new NotSupportedException($"{first} => {last} is not a diagonal");

        var (r, c) = (Math.Sign(dr), Math.Sign(dc));

        var add = first;
        while (add != last)
        {
            add = new(add.Row + r, add.Col + c);
            cells |= add;
        }

        return Common.Groups.SumCage(cells, [sum]);
    }
}
