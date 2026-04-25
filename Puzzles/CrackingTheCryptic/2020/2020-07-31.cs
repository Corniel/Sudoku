using System.Data;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_07_31 : CtcPuzzle
{
    public override string Title => "Arrow/Group Sum";

    public override string? Author => "ahaupt";

    public override Uri? Url => new("https://youtu.be/73iEwlTO_p0");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.Parse("""
        529│781│463
        876│423│159
        431│965│827
        ───┼───┼───
        315│842│976
        294│376│581
        687│159│342
        ───┼───┼───
        168│537│294
        952│614│738
        743│298│615
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + Arrows.Parse("""
        ...│...│C..
        ...│...│B..
        ...│..A│.bc
        ───┼───┼───
        ...│...│a..
        ...│...│...
        ..E│...│...
        ───┼───┼───
        GF.│e..│...
        ..f│...│...
        ..g│...│...
        """)
        + KillerCages.Parse("""
        ...│...│...
        ...│BB.│...
        AA.│BB.│...
        ───┼───┼───
        AA.│...│...
        ...│...│...
        ...│...│.CC
        ───┼───┼───
        ...│.DD│.CC
        ...│.DD│...
        ...│...│...
        A = 11  B = 21  C = 19  D = 15
        """).Select(c => AtMost((KillerCage)c))
         + KillerCages.Parse("""
        ..A│A..│...
        ..A│A..│...
        ...│...│...
        ───┼───┼───
        .BB│...│...
        .BB│...│CC.
        ...│...│CC.
        ───┼───┼───
        ...│...│...
        ...│..D│D..
        ...│..D│D..
        A = 26  B = 19  C = 20  D = 25
        """).Select(c => AtLeast((KillerCage)c))
        + pos(0, 3).LT(0, 4)
        + pos(3, 0).GT(4, 0)
        + pos(4, 8).LT(5, 8)
        + pos(8, 4).GT(8, 5)
        ;

    private static SumCage AtMost(KillerCage cage)
        => new([.. range(1, cage.Sum)], cage.Cells);

    private static SumCage AtLeast(KillerCage cage)
        => new(Ints.New([.. range(8 + 8 + 9 + 9)]) & [.. range(cage.Sum, 4 * 9)], cage.Cells);
}
