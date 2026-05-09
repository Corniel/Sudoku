namespace Sudoku.Sets;

public sealed class Window(int index, PosSet set) : House(index, set)
{
    internal static IEnumerable<Window> All() => Grid.NamedGroups("""
        ...│...│...
        .AA│A.B│BB.
        .AA│A.B│BB.
        ───┼───┼───
        .AA│A.B│BB.
        ...│...│...
        .CC│C.D│DD.
        ───┼───┼───
        .CC│C.D│DD.
        .CC│C.D│DD.
        ...│...│...
        """)
        .Select(g => new Window(g.Name - 'A', g));
}
