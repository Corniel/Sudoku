using Sudoku.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_04_23 : CtcPuzzle
{
    public override string Title => "Indifferent Neighbours";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/Xv8D0737qfc");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.Parse("""
        9..│...│..1
        ...│.6.│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        .7.│...│.3.
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│.8.│...
        1..│...│..5
        """);

    protected override Rules GetConstraints()
        => Rules.AntiKnight
        + FixedNeigbors()
        + GroupOf3s();

    public override Cells Solution { get; } = Cells.Parse("""
        963│742│851
        527│168│349
        841│953│762
        ───┼───┼───
        419│537│628
        275│816│934
        638│429│517
        ───┼───┼───
        384│295│176
        752│681│493
        196│374│285
        """);

    private static IEnumerable<Twin> FixedNeigbors() => Pos.All
        .Where(a => a.N() is { } && a.E() is { } && Box.IndexOf(a) != Box.IndexOf(a - 8))
        .SelectMany(a => Twins.New(a, a - 8));

    private static IEnumerable<Restriction> GroupOf3s()
        => range(_9x9 / 3)
        .Select(p => new Pos(p * 3))
        .SelectMany(p => EntropicLine.New([p, p + 1, p + 2]));
}
