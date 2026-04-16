namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_05_15 : CtcPuzzle
{
    public override string Title => "Equal Sudoku";

    public override string? Author => "Christoph Seeliger";

    public override Uri? Url => new("https://youtu.be/ygCqTawsmsM");

    public override O Duration => O.ms100;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│...
        ...│...│...
        ...│.7.│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        613│582│794
        597│431│628
        824│976│315
        ───┼───┼───
        158│267│943
        436│895│172
        279│314│856
        ───┼───┼───
        385│149│267
        741│623│589
        962│758│431
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + NamedCage.Parse("""
        BBG│GGG│GHH
        BCF│AAG│GHH
        CCF│.AA│IIH
        ───┼───┼───
        CFF│JJJ│.IO
        DDD│K.J│NNO
        ED.│KLJ│NOO
        ───┼───┼───
        EEK│KL.│PPP
        EEE│KLM│MPP
        EEK│KLM│XXX
        """).SelectMany(c => Group.Select(c.Cells, (a, o) => new Cage(a, o)));

    public sealed class Cage(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others), Peers
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var sum = Zero;

            foreach (var other in Others)
            {
                var digits = cells[other].Digits;
                Ints es = default;
                Ints os = default;
                if ((digits & Digits.Even) is { HasAny: true } e)
                    es = sum + e;

                if ((digits & Digits.Odd) is { HasAny: true } o)
                    os = sum - o;

                sum = es | os;
            }

            var odds = (sum - Zero).Digits & Digits.Odd;
            var even = (Zero - sum).Digits & Digits.Even;
            return odds | even;
        }

        // We use an offset to deal with negative (sub) results.
        private static readonly Ints Zero = [60];
    }
}
