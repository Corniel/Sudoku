namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_08_15 : CtcPuzzle
{
    public override string Title => "Thermo Bunches";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/vCg9gbNk7b4");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        823│765│194
        916│843│257
        754│921│683
        ───┼───┼───
        238│617│945
        175│394│862
        649│582│731
        ───┼───┼───
        387│159│426
        562│478│319
        491│236│578
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + pos(1, 6).Clue(2)
        + pos(7, 3).Clue(4)
        + Lines.Thermometer("""
            ...│...│..A
            ...│...│.BG
            ...│...│CHK
            ───┼───┼───
            ...│..D│ILa
            ...│.E.│Mbf
            ...│...│cgk
            ───┼───┼───
            ...│..d│hl.
            ...│..i│m..
            ...│..n│...
            """)
        + Lines.Thermometer("""
            ...│n..│...
            ..m│i..│...
            .lh│d..│...
            ───┼───┼───
            kgc│L..│...
            fbK│...│...
            aJG│...│...
            ───┼───┼───
            IFC│...│...
            EB.│...│...
            A..│...│...
            """);
}
