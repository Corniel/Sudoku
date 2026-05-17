namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_05_17 : CtcPuzzle
{
    public override string Title => "The New Mirical";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/Tv-48b-KuxI");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        948│372│615
        372│615│948
        615│948│372
        ───┼───┼───
        483│726│159
        726│159│483
        159│483│726
        ───┼───┼───
        837│261│594
        261│594│837
        594│837│261
        """);

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│...│...
        ...│.4.│...
        ───┼───┼───
        ..3│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKnight
        + Anti.King
        + NonConsecutives.Orthogonally();
}
