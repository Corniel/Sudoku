using Sudoku.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_11 : CtcPuzzle
{
    public override string Title => "Fallen Mast In A Storm";

    public override string? Author => "Marty Sears";

    public override Uri? Url => new("https://youtu.be/um2u7MC1X3o");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        564│321│879
        456│132│987
        645│213│798
        ───┼───┼───
        798│546│213
        879│654│321
        987│465│132
        ───┼───┼───
        213│879│546
        321│987│654
        132│798│465
        """);

    public override Rules Constraints { get; }
        = Rules.Basic
        + ThreeDisticts()
        + SameSums.Parse("""
        ..A│.BB│...
        a..│B..│...
        aa.│...│...
        ───┼───┼───
        b..│...│...
        .b.│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        A=B a=b
        """)
        + SameDifferences.Parse("""
        A..│...│...
        .B.│...│...
        ..C│...│...
        ───┼───┼───
        ...│D..│...
        ...│.E.│...
        ...│..F│...
        ───┼───┼───
        ...│...│G..
        ...│...│.H.
        ...│...│..I
        """)
        + SameDifferences.Parse("""
        ...│...│..A
        ...│...│.B.
        ...│.l.│C..
        ───┼───┼───
        ...│m.D│...
        ..n│.E.│..f
        ...│F..│.ga
        ───┼───┼───
        ..G│...│hb.
        .H.│..i│c..
        I..│.jd│...
        """)
        ;

    private static IEnumerable<Restriction> ThreeDisticts()
        => Box.All
        .SelectMany(box => Group.Select(box.Cells, (a, o) => new ThreeDistict(a, o)));

    private sealed class ThreeDistict(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var used = Digits.None;
            foreach (var other in Others)
                used |= cells[other].Digit;

            return used.Count switch
            {
                < 3 => Digits._1_to_9,
                3 => used,
                _ => Digits.None,
            };
        }
    }
}
