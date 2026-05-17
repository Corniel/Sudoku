using System.Diagnostics;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_11_09 : CtcPuzzle
{
    public override string Title => "Bow and Arrow";

    public override string? Author => "Jonas Gleim";

    public override Uri? Url => new("https://youtu.be/pdtWTg4LrqQ");

    public override O Duration => O.μs10;

    public override Cells Solution { get; } = Cells.New("""
        678│351│942
        539│284│761
        412│976│385
        ───┼───┼───
        195│843│627
        367│529│814
        284│617│539
        ───┼───┼───
        921│738│456
        846│195│273
        753│462│198
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.Thermometer("""
        CDE│.R.│.IH
        B..│..Q│..G
        A..│...│PL.
        ───┼───┼───
        ...│...│.OK
        ..p│on.│.N.
        ...│...│..l
        ───┼───┼───
        e..│...│.k.
        d..│...│..g
        cba│...│.ih
        """)
       + Lines.Thermometer("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│.A.
        ...│...│.B.
        ───┼───┼───
        ...│...│C..
        ...│..D│...
        ...│.E.│...
        """)
        + Lines.Arrow("""
        ...│.D.│...
        ...│C..│...
        ..B│...│...
        ───┼───┼───
        .A.│...│...
        ...│...│...
        .a.│...│...
        ───┼───┼───
        ..b│...│...
        ...│c..│...
        ...│.d.│...
        """)
        + LongArrow();

    /// <remarks>
    /// The long arrow requires the first two digits read as a number to be equal to 45 minus the sum of the digits.
    /// </remarks>
    private static Rules LongArrow()
    {
        var masks = new Digits[2];

        for (var t = 1; t <= _9; t++)
        {
            for (var o = 1; o <= _9; o++)
            {
                if (t == o) continue;

                var tot = t * 10 + o;

                if (tot == 45 - t - o)
                {
                    masks[0] |= t;
                    masks[1] |= o;
                }
            }
        }
        return masks.Select((mask, i) => new Mask((4, i), mask));
    }
}
