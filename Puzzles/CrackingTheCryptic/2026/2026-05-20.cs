namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_20 : CtcPuzzle
{
    public override string Title => "Balance Lines";

    public override string? Author => "Jonesy";

    public override Uri? Url => new("https://youtu.be/_jkTguG0XnU");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.New("""
        531│784│692
        862│159│473
        947│632│185
        ───┼───┼───
        623│591│748
        789│346│251
        154│278│936
        ───┼───┼───
        376│425│819
        418│963│527
        295│817│364
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Grid.NamedGroups("""
        AAA│AAB│BBB
        A..│CC.│DDB
        ...│C..│.DD
        ───┼───┼───
        EEE│EFF│.II
        GG.│HFF│IIJ
        GNN│HHH│I.J
        ───┼───┼───
        ..N│N..│KJJ
        M.M│N..│KJJ
        MMM│LLL│KJJ
        """).SelectMany(g => Group.Select(g, (a, o) => new BalancedLine(a, o)))
        + Couples.WhiteDots("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        .AA│...│...
        ...│B..│...
        ...│B..│...
        """)
        + pos(1, 0).IsEven;

    public sealed class BalancedLine(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var balance = Offset;

            foreach (var other in Others)
            {
                var digits = cells[other].Digits;
                var e = balance + (digits & Digits.Even);
                var o = balance - (digits & Digits.Odd);
                balance = e | o;
            }

            var odd = (balance - Offset).Digits & Digits.Odd;
            var evn = (Offset - balance).Digits & Digits.Even;
            return odd | evn;
        }

        private static readonly Ints Offset = [64];
    }
}
