using Sudoku.Generics;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_04_27 : CtcPuzzle
{
    public override string Title => "The Sequal";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/ODob3WSRoyM");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.New("""
        3..│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        2..│..4│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        .9.│...│...
        """);

    public override Cells Solution { get; } = Cells.New("""
        361│452│879
        582│379│641
        749│681│352
        ───┼───┼───
        235│964│187
        918│537│264
        674│218│935
        ───┼───┼───
        857│126│493
        426│793│518
        193│845│726
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKnight
        + Delta2_3().SelectMany(p => p);

    private static IEnumerable<Couple<Pair>> Delta2_3()
    {
        foreach (var pos in Diagonal.NE_SW)
        {
            if (pos.N() is { } n)
                yield return new LookupPair(pos, n, D3).Couple();
            if (pos.W() is { } w)
                yield return new LookupPair(pos, w, D2).Couple();
        }
    }

    public static readonly LookupDigits D2 = LookupPair.Init(
    [
        Digits.None,
        [0, 3],
        [0, 4],
        [1, 5],
        [2, 6],
        [3, 7],
        [4, 8],
        [5, 9],
        [6, 0],
        [7, 0],
    ]);

    public static readonly LookupDigits D3 = LookupPair.Init(
    [
        Digits.None,
        [0, 4],
        [0, 5],
        [0, 6],
        [1, 7],
        [2, 8],
        [3, 9],
        [4, 0],
        [5, 0],
        [6, 0],
    ]);
}
