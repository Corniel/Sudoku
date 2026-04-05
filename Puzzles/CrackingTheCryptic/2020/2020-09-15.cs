namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_09_15 : CtcPuzzle
{
    public override string Title => "Sudoku XV";

    public override string? Author => "Arvid Baars";

    public override Uri? Url => new("https://youtu.be/9ATC_uBF8ow");

    public override O Duration => O.μs10;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        .5.│3.8│.2.
        ───┼───┼───
        2.5│.3.│6.9
        .9.│4.6│.1.
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        176│524│938
        524│983│167
        983│167│245
        ───┼───┼───
        367│249│851
        812│675│493
        459│318│726
        ───┼───┼───
        245│831│679
        798│456│312
        631│792│584
        """);

    public override Rules Constraints { get; }
        = Rules.Killer("""
        ..B│.E.│H..
        .AB│DEG│HJ.
        .AC│DFG│IJ.
        ───┼───┼───
        ..C│.F.│I..
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        A = 10  B = 10  C = 10  D = 10  E = 10  F = 10  G = 10  H = 10  I = 10  J = 10
        """)
        + Nots()
        + KillerCages.Extend;

    private static IEnumerable<Restriction> Nots()
    {
        var cages = NamedCage.Parse("""
        ..B│.E.│H..
        .AB│DEG│HJ.
        .AC│DFG│IJ.
        ───┼───┼───
        ..C│.F.│I..
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """).Select(c => PosSet.New(c.Cells)).ToArray();

        return Dominos.Ort
            .Where(d => !cages.Contains(d.Set))
            .Select(d => new LookupPair(d.A, d.B, Not5_10))
            .Couples();
    }

    private static readonly int _ = 0;
    private static readonly LookupDigits Not5_10 = LookupPair.Init(
    [
        Digits._1_to_9,
        [_, 2, 3, _, 5, 6, 7, 8, _],
        [1, _, _, 4, 5, 6, 7, _, 9],
        [1, _, _, 4, 5, 6, _, 8, 9],
        [_, 2, 3, _, 5, _, 7, 8, 9],
        [1, 2, 3, 4, _, 6, 7, 8, 9],
        [1, 2, 3, _, 5, _, 7, 8, 9],
        [1, 2, _, 4, 5, 6, _, 8, 9],
        [1, _, 3, 4, 5, 6, 7, _, 9],
        [_, 2, 3, 4, 5, 6, 7, 8, _],
    ]);
}
