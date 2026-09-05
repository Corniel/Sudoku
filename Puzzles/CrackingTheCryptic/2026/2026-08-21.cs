namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_08_21 : CtcPuzzle
{
    public override string Title => "I See No Dots";

    public override string? Author => "Billybeth";

    public override Uri? Url => new("https://youtu.be/Wt0ZgXDQLGw");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        253│871│964
        718│649│253
        496│532│718
        ───┼───┼───
        325│187│496
        871│964│325
        649│253│871
        ───┼───┼───
        532│718│649
        187│496│532
        964│325│187
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.GermanWhisper("""
            ..B│A..│...
            .DC│..M│N..
            .E.│.L.│OP.
            ───┼───┼───
            .F.│JK.│.Q.
            .GH│I..│R..
            ..Y│X.T│S..
            ───┼───┼───
            .Z.│WVU│...
            ba.│...│...
            c..│...│...
            """)
        + Dominos.Ort.SelectMany(d => new LookupPair(d, NoDot).Couple());

    private static readonly int _ = 0;

    /// <summary>No Ratio 1:2 nor 1:3.</summary>
    private static readonly LookupDigits NoDot = LookupPair.Init(
    [
        /* 0 */ _1_to_9,
        /* 1 */ [_,_,_,4,5,6,7,8,9], // not 2, 3
        /* 2 */ [_,_,3,_,5,_,7,8,9], // not 1, 4, 6
        /* 3 */ [_,2,_,4,5,_,7,8,_], // not 1, 6, 9
        /* 4 */ [1,2,3,_,5,6,7,8,9], // not 2, 8
        /* 5 */ [1,2,3,4,_,6,7,8,9],
        /* 6 */ [1,_,_,4,5,_,7,8,9], // not 2, 3
        /* 7 */ [1,2,3,4,5,6,_,8,9],
        /* 8 */ [1,2,3,_,5,6,7,_,9], // not 4
        /* 9 */ [1,2,_,4,5,6,7,8,_], // not 3
    ]);
}
