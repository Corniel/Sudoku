namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_09 : CtcPuzzle
{
    public override string Title => "36x Even!";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/Yv_RMyYTeRU");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        752│183│496
        349│256│187
        618│749│352
        ───┼───┼───
        983│612│745
        476│395│218
        521│874│963
        ───┼───┼───
        235│961│874
        167│438│529
        894│527│631
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Groups.EvenOdd("""
        OOE│OEO│EOE
        OEO│EOE│OEO
        EOE│O4O│OOE
        ───┼───┼───
        OEO│EOE│OEO
        EOE│OOO│EOE
        OEO│8O4│OEO
        ───┼───┼───
        EO5│O6O│8OE
        OEO│E3E│OEO
        EOE│OEO│EOO
        """)
        + Triples().SelectMany(NonConsecutive.New);

    private static IEnumerable<PosSet> Triples()
    {
        foreach (var r in range())
        {
            foreach (var c in range())
            {
                // Horizontal
                if (c is > 0 and < 8)
                    yield return [pos(r, c - 1), pos(r, c + 0), pos(r, c + 1)];

                // Vertical
                if (r is > 0 and < 8)
                    yield return [pos(r - 1, c), pos(r + 0, c), pos(r + 1, c)];

                // Diagonal
                if (c is > 0 and < 8 && r is > 0 and < 8)
                {
                    yield return [pos(r - 1, c - 1), pos(r, c), pos(r + 1, c + 1)];
                    yield return [pos(r - 1, c + 1), pos(r, c), pos(r + 1, c - 1)];
                }
            }
        }
    }
}
