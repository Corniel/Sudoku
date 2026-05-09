using Sudoku.Generics;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_17 : CtcPuzzle
{
    public override string Title => "The Fireflies' Pairing Danee";

    public override string? Author => "Patrick Junke";

    public override Uri? Url => new("https://youtu.be/o3619evP8Yc");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        123│745│968
        459│168│237
        768│239│154
        ───┼───┼───
        382│471│596
        671│592│483
        594│683│721
        ───┼───┼───
        916│827│345
        247│356│819
        835│914│672
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Couples.WhiteDots("""
        ...│.AA│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│.BB
        ...│...│...
        ...│...│...
        """)
        + Couples.BlackDots("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ..A│...│..C
        ..A│...│..C
        ...│...│...
        ───┼───┼───
        ...│...│...
        BB.│...│...
        ...│...│...
        """)
        + GoldenDots();

    public static Rules GoldenDots()
    {
        var hor = Grid.NamedGroups("""
        ..A│B..│...
        ..A│B..│.I.
        ...│...│.I.
        ───┼───┼───
        ...│.F.│...
        .C.│.F.│..G
        .C.│...│..G
        ───┼───┼───
        ...│...│...
        ..D│E..│.H.
        ..D│E..│.H.
        """);
        var ver = Grid.NamedGroups("""
        ...│...│...
        ...│...│...
        ...│.AA│...
        ───┼───┼───
        ...│.BB│...
        .CC│...│DD.
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│FF.│...
        .EE│...│...
        """);

        PosSet[] golden = [.. hor, .. ver];

        foreach (var p in Dominos.Ort)
        {
            var sums = p switch
            {
                _ when golden.Contains(p.Set) => Gold,
                _ when Houses.Boxes.NotAny(b => p.Set.IsSubsetOf(b)) => Bord,
                _ => Othr,
            };

            var cage = Cage(p.A, p.B, sums);
            yield return cage.One;
            yield return cage.Two;
        }

        static Couple<Cage> Cage(Pos a, Pos b, Ints totals) => new(
            new Cage(a, [b], totals),
            new Cage(b, [a], totals));
    }

    private static readonly Ints Gold = [4, 8, 12, 16];
    private static readonly Ints Bord = [2, 6, 10, 14];
    private static readonly Ints Othr = [3, 5, 6, 7, 9, 10, 11, 13, 14, 15, 17];
}
