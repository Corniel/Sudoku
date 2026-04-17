namespace Puzzles.CrackingTheCryptic;

public sealed class _2022_12_19 : CtcPuzzle
{
    public override string Title => "The Fiftheenth Day Of Christmas";

    public override string? Author => "Chris Moore";

    public override Uri? Url => new("https://youtu.be/Iu9sDHZwjj8");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        318│459│267
        952│617│843
        647│238│519
        ───┼───┼───
        879│543│126
        235│961│784
        164│782│935
        ───┼───┼───
        786│325│491
        523│194│678
        491│876│352
        """);

    protected override Rules GetConstraints()
        => Rules.AntiKnight
        + PurpleLines("""
        ...│BB.│...
        A..│B..│...
        A..│C..│DE.
        ───┼───┼───
        ...│.CD│..E
        .F.│CGH│DE.
        ..F│G..│H..
        ───┼───┼───
        .F.│.GH│...
        ...│...│..J
        ...│II.│.JJ
        """)
        + Thermometers.Parse("""
        ...│...│...
        ...│...│...
        ...│C..│...
        ───┼───┼───
        ...│.C.│...
        ...│C..│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    private static IEnumerable<Restriction> PurpleLines(string str)
    {
        RenbanLine[] lines = [.. RenbanLines.Parse(str)];
        PosSet[] ln = [..lines.Select(ln => ln.Cells)];
        return lines.SelectMany(Line);

        IEnumerable<Restriction> Line(RenbanLine line) =>
        [
            .. Group.Select(line.Cells, (a, o) => new Unique(a, o)),
            .. Group.Select(line.Cells, (a, o) => new PurpleLine(a, o, ln)),
        ];
    }

    public sealed class PurpleLine(
        Pos appliesTo,
        ImmutableArray<Pos> others,
        PosSet[] lines) : Group(appliesTo, [.. others, .. lines.SelectMany(l => l)])
    {
        private readonly int Size = others.Length + 1;
        private readonly ImmutableArray<Pos> Line = others;
        private readonly ImmutableArray<Pos>[] Lines =
        [
            .. lines.Where(l => l.Count == others.Length + 1 && !l.Contains(appliesTo))
                .Select(ln => ln.ToImmutableArray())
        ];

        public override Digits Restrict(SudokuCells cells)
        {
            var total = Sum;
            var digits = Digits.None;

            foreach (var o in Line)
            {
                var d = cells[o].Digits;
                total -= d;
                digits |= d;
            }

            if (total.Digits.HasSingle)
            {
                Combos.Clear();
                Combos.Add(digits | total.Digits);

                foreach (var line in Lines)
                {
                    var combo = Digits.None;
                    foreach (var c in line)
                        combo |= cells[c].Digit;

                    if (combo.Count == Size && !Combos.Add(combo))
                    {
                        return Digits.None;
                    }
                }
            }

            return total.Digits;
        }

        private static readonly Ints Sum = [15];
        private static readonly HashSet<Digits> Combos = [];
    }
}
