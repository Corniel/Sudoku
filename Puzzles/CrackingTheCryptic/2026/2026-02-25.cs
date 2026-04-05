namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_02_25 : CtcPuzzle
{
    public override string Title => "XII";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/dzh_1Ndfy4w");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.Parse("""
        973641528
        496375182
        251839647
        528163974
        742586391
        185924736
        819257463
        637492815
        364718259
        """);

    public override Clues Clues { get; } = Clues.Parse("""
        ....4....
        .........
        .........
        .........
        .........
        .........
        ...2.7...
        ....9....
        .6.....5.
        """);

    public override Rules Constraints { get; }
        = Rules.Basic
        + Twins.Parse("""
        ..AB.CDEF
        ..BA.DCFE
        GHIJ...KL
        HGJI...LK
        .........
        MN...OPQR
        NM...PORQ
        STUV.XY..
        TSVU.YX..
        """)
        + NonConsecutives.Orthogonally();
}
