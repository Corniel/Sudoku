namespace Sudoku.Common;

public static partial class Groups
{
    [Pure]
    public static Rules EvenOdd(string grid)
    {
        var cages = Grid.NamedGroups(grid);
        return
        [
            .. cages.FirstOrNone(c => c.Name is 'E')?.Cells.Select(Mask.Even) ?? [],
            .. cages.FirstOrNone(c => c.Name is 'O')?.Cells.Select(Mask.Odd) ?? [],
        ];
    }
}
