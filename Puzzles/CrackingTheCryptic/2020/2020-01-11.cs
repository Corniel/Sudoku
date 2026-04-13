namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_01_11 : CtcPuzzle
{
    public override string Title => "<= 5";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/ZU5fSDHJq8k");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│..1
        .8.│...│...
        ...│...│...
        ───┼───┼───
        8..│.1.│...
        ..3│.62│...
        ...│...│...
        ───┼───┼───
        ...│...│.9.
        4.7│9..│...
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        249│675│831
        385│149│672
        761│238│954
        ───┼───┼───
        896│314│527
        573│862│149
        124│597│386
        ───┼───┼───
        612│483│795
        437│951│268
        958│726│413
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + Dominos.Ort.SelectMany(d => DeltaMax.New(d, 5));
}
