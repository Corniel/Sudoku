namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_27 : CtcPuzzle
{
    public override string Title => "Tinsel & Baubles";

    public override string? Author => "Marty Sears";

    public override Uri? Url => new("https://youtu.be/6Xc78VZxcYI");

    public override O Duration => O.ms100;

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│..6│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.New("""
        536│497│182
        849│152│376
        172│683│594
        ───┼───┼───
        715│348│629
        628│915│743
        493│726│815
        ───┼───┼───
        964│571│238
        387│264│951
        251│839│467
        """);

    private const string Tinsels = """
        .D.│FGk│lmn
        C.E│H..│.o.
        BA.│.I.│.QR
        ───┼───┼───
        qr.│...│.TS
        ..s│...│U..
        vut│...│ON.
        ───┼───┼───
        w..│.ih│..M
        .a.│...│g.L
        ..b│cde│f.K
        """;

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.GermanWhisper(Tinsels)
        + GetBaubles();

    private static Rules GetBaubles()
    {
        PosArray baubles = [.. Grid.NamedGroups("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│AAA│...
        ...│...│...
        ...│AA.│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """).Single()];

        PosArray tinsel = [.. Lines.Parse(Tinsels).SelectMany(l => l)];

        return
        [
            .. Group.Select(baubles, (a, o) => new Bauble(a, o, tinsel)),
            .. Group.Select(tinsel, (a, o) => new Tinsel(a, o, baubles)),
        ];
    }

    public sealed class Bauble(Pos appliesTo, PosArray others, PosArray tinsel) : Restriction
    {
        public Pos AppliesTo { get; } = appliesTo;

        public PosArray Tinsel { get; } = tinsel;

        public PosSet Cells { get; } = [appliesTo, .. others, .. tinsel];

        public Digits Restrict(SudokuCells cells)
        {
            Array.Clear(Min);
            Array.Clear(Max);

            foreach (var t in Tinsel)
            {
                var digits = cells[t].Digits;

                if (digits.HasSingle)
                    Min[digits.First()]++;
                else
                    foreach (var d in digits)
                        Max[d]++;
            }

            var allowed = Digits.None;

            for (var d = 1; d <= _9; d++)
            {
                if (d is 5) continue;

                var (min, max) = (Min[d], Max[d]);
                if (min <= d && min + max >= d)
                    allowed |= d;
            }
            return allowed;
        }
    }

    public sealed class Tinsel(Pos appliesTo, PosArray others, PosArray baubles) : Restriction
    {
        public Pos AppliesTo { get; } = appliesTo;

        public PosArray Others { get; } = others;

        public PosArray Baubles { get; } = baubles;

        public PosSet Cells { get; } = [appliesTo, .. others, .. baubles];

        public Digits Restrict(SudokuCells cells)
        {
            Array.Clear(Count);

            foreach (var b in Baubles)
                foreach (var d in cells[b].Digits)
                    Count[d]++;

            var singles = Count.Count(c => c is 1);

            // If no count is unique, there are no restrictions.
            if (singles is 0)
                return Digits._1_to_9;

            Array.Clear(Min);
            Array.Clear(Max);

            foreach (var o in Others)
            {
                var digits = cells[o].Digits;

                if (digits.HasSingle)
                    Min[digits.First()]++;
                else
                    foreach (var d in digits)
                        Max[d]++;
            }

            var allowed = Digits.None;

            for (var d = 1; d <= _9; d++)
            {
                if (d is 5) continue;

                var count = Count[d];

                if (count is not 1)
                    allowed |= d;
                else
                {
                    var (min, max) = (Min[d], Max[d]);

                    if (min + max + 1 >= d)
                        allowed |= d;
                    else if (min != d)
                        return Digits.None;
                }
            }

            return allowed;
        }
    }

    private static readonly int[] Count = new int[_9 + 1];
    private static readonly int[] Min = new int[_9 + 1];
    private static readonly int[] Max = new int[_9 + 1];
}
