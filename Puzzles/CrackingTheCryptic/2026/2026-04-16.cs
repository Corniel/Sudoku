namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_16 : CtcPuzzle
{
    public override string Title => "The Not So Simple Mircale";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/mRtzcjaCntQ");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        261│594│837
        594│837│261
        837│261│594
        ───┼───┼───
        483│726│159
        726│159│483
        159│483│726
        ───┼───┼───
        615│948│372
        948│372│615
        372│615│948
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKnight
        + pos(3, 5).Clue(6)
        + pos(5, 4).Clue(8)
        + Lines.Parse("""
        ...│AFL│...
        ..B│GM.│...
        .CH│N..│...
        ───┼───┼───
        DIO│...│..l
        JP.│...│.mf
        Q..│...│nga
        ───┼───┼───
        ...│..o│hb.
        ...│.pi│c..
        ...│qjd│...
        """).SelectMany(Consecutives)
        + Lines.Parse("""
        ...│...│..A
        ...│...│.B.
        ...│...│C..
        ───┼───┼───
        ...│..D│...
        ...│.E.│...
        ...│F..│...
        ───┼───┼───
        ..G│...│...
        .H.│...│...
        I..│...│...
        """).SelectMany(Consecutives);

    private static Rules Consecutives(Line line)
        => range(line.Length - 1)
        .SelectMany(i => Couples.Consecutive(line[i], line[i + 1]));
}
