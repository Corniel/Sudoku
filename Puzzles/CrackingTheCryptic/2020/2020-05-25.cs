namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_05_25 : CtcPuzzle
{
    public override string Title => "Killer XXL";

    public override string? Author => "Phistomefel";

    public override Uri? Url => new("https://youtu.be/gzZl1EK4bww");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        467│821│539
        239│567│481
        518│493│762
        ───┼───┼───
        682│134│957
        794│258│613
        351│679│824
        ───┼───┼───
        843│716│295
        926│345│178
        175│982│346
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        .AA│ABB│B..
        .CC│A..│BBD
        CCC│AAA│BBD
        ───┼───┼───
        C.E│...│DDD
        C.E│...│D.I
        EEE│...│D.I
        ───┼───┼───
        EFF│GGG│III
        EFF│..G│II.
        ..F│FFG│GG.
        A=42 B=33 C=39 D=38 E=32 F=37 G=28 I=31
        """)
        + Diagonal.NE_SW
        + Diagonal.NW_SE
        + KillerCages.Extend;
}
