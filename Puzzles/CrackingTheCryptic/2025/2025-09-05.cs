using Sudoku.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_05 : CtcPuzzle
{
    public override string Title => "Besties 2";

    public override string? Author => "Jeet Sampat";

    public override Uri? Url => new("https://youtu.be/ZxElsPvjkqw");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        241│837│956
        736│529│481
        895│614│372
        ───┼───┼───
        652│148│739
        378│295│614
        914│376│528
        ───┼───┼───
        563│481│297
        487│952│163
        129│763│845
        """);

    public override Rules Constraints { get; }
        = Rules.AntiKnight
        + KillerCages.Parse("""
        ...│...│...
        ...│...│...
        ..A│B.C│C..
        ───┼───┼───
        ..A│Bca│b.d
        ...│.ca│b.d
        ..D│D.F│G..
        ───┼───┼───
        ..E│E.F│G..
        ...│...│...
        ...│ee.│...

        A = 7   B = 7   C = 7   D = 7   E = 7   F = 7   G = 7
        a = 13  b = 13  c = 13  d = 13  e = 13
        """)
        + Group.Select(Diagonal.NW_SE.Cells, (a, o) => new Cage(a, o, [39]));
}
