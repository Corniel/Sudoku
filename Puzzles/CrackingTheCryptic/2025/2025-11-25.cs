namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_11_25 : CtcPuzzle
{
    public override string Title => "Simple Miracle";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/4fwsRuKC6EY");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        837│261│594
        261│594│837
        594│837│261
        ───┼───┼───
        159│483│726
        483│726│159
        726│159│483
        ───┼───┼───
        372│615│948
        615│948│372
        948│372│615
        """);

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        .1.│...│...
        ...│..2│...
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.AntiKnight
        + Digonals();

    public static Rules Digonals()
    {
        foreach (var diagonal in Diagonals.NESWs)
        {
            var cells = diagonal.ToArray();
            for (var i = 1; i < cells.Length; i++)
            {
                var couple = new LookupPair(cells[i], cells[i - 1], Lookup).Couple();
                yield return couple.One;
                yield return couple.Two;
            }
        }
    }

    private static readonly LookupDigits Lookup = LookupPair.Init(
    [
        Digits.None,
        /* 1 */ [2, 9],
        /* 2 */ [1, 3],
        /* 3 */ [2, 4],
        /* 4 */ [3, 5],
        /* 5 */ [4, 6],
        /* 6 */ [5, 7],
        /* 7 */ [6, 8],
        /* 8 */ [7, 9],
        /* 9 */ [1, 8],
    ]);
}
