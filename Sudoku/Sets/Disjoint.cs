namespace Sudoku.Sets;

public sealed class Disjoint(int index, PosSet set) : House(index, set)
{
    internal static IEnumerable<Disjoint> All() => Grid.NamedGroups("""
        ABC│ABC│ABC
        DEF│DEF│DEF
        GHI│GHI│GHI
        ───┼───┼───
        ABC│ABC│ABC
        DEF│DEF│DEF
        GHI│GHI│GHI
        ───┼───┼───
        ABC│ABC│ABC
        DEF│DEF│DEF
        GHI│GHI│GHI
        """)
        .Select(g => new Disjoint(g.Name - 'A', g));
}
