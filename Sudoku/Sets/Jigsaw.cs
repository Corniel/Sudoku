namespace Sudoku.Sets;

/// <summary>
/// Represents an irregular (jigsaw) house, as an alternative to <see cref="Box"/>es.
/// </summary>
public static class Jigsaw
{
    public static Rules New(string grid)
        => Grid.NamedGroups(grid).SelectMany(Jigsaws);

    private static Rules Jigsaws(NamedGroup group)
        => char.IsAsciiDigit(group.Name)
        ? group.Select(c => new Mask(c, [group.Name - '0']))
        : [new CellSet(group, $"Jigsaw[{group.Name}]")];
}
