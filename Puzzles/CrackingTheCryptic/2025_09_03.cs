namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_03 : CtcPuzzle
{
    public override string Title => "Most Squares";

    public override string? Author => "henter";

    public override Uri? Url => new("https://youtu.be/_Rf4tFPFPqU");

    public override O Duration => O.oo;

    public override Cells Solution { get; } = Cells.Parse("""
        913│587│642
        456│219│378
        287│463│591
        ───┼───┼───
        538│674│219
        194│825│763
        762│391│854
        ───┼───┼───
        649│732│185
        375│148│926
        821│956│437
        """);

    public override Rules Constraints { get; }
        = Rules.Standard
         + NamedCage.Parse("""
            .AA│A..│...
            AAx│...│.CC
            Axx│xx.│CC.
            ───┼───┼───
            A.x│xxC│...
            ..x│xCC│..D
            ...│CCz│..D
            ───┼───┼───
            ..C│..z│z.D
            .CC│...│..D
            C..│.DD│DD.
            """)
            .SelectMany(named => Group.Select(named.Cells, (a, o) => new Line(a, o)));

    public sealed class Line(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var app = cells[AppliesTo].Digits;

            var min = Digits.New(app.First());
            var max = app;
            var sum = Ints.Zero;

            foreach (var cell in Others.Select(o => cells[o]))
            {
                min |= cell.Digit;
                max |= cell.Digits;
                sum += cell.Digits;
            }

            max &= Digits.AtLeast(min.Last());

            var allowed = Digits.None;

            foreach (var digit in max)
                allowed |= (Sqrs[digit] - sum).Digits & Digits.AtMost(digit);

            return allowed;
        }

        private static readonly ImmutableArray<Ints> Sqrs = [..range(0, _9 + 1).Select(d => Ints.New(d.Sqr()))];
    }
}
