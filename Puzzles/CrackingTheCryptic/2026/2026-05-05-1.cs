using System.Runtime.InteropServices;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_05_1 : CtcPuzzle
{
    public override string Title => "This Is Sparta!";

    public override string? Author => "Nicolas Duhail";

    public override Uri? Url => new("https://youtu.be/d7BhfgTXQrc");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        143│267│598
        657│983│214
        298│415│736
        ───┼───┼───
        576│129│843
        481│536│927
        932│748│165
        ───┼───┼───
        814│652│379
        765│391│482
        329│874│651
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Modulars("""
        ...│..A│AAB
        ..N│NNA│CCB
        .MM│...│DCB
        ───┼───┼───
        .M.│..D│DEE
        LL.│..O│EEF
        L..│..O│OFF
        ───┼───┼───
        L..│..P│HFG
        KK.│.PP│HHG
        .KJ│JJI│IIG
        """)
        + Hollows("""
        ..A│A..│...
        .A.│...│...
        A..│.BB│...
        ───┼───┼───
        A..│BB.│...
        ..B│B..│...
        .BB│.C.│...
        ───┼───┼───
        .B.│.C.│...
        ..C│C..│...
        ...│...│...
        """);

    private static Rules Modulars(string grid)
    {
        var groups = Grid.NamedGroups(grid);

        PosSet combined = [.. groups.SelectMany(g => g)];
        combined = ~combined;

        return
        [
            // 9 * 45 - 300 = 105
            .. Group.Select(combined, (a, o) => new Cage(a, o, [105])),
            .. groups.SelectMany(line => Group.Select(line, (a, o) => new Modular(a, o))),
        ];
    }

    private static Rules Hollows(string grid) => Grid.NamedGroups(grid)
        .SelectMany(line => Group.Select(line, (a, o) => new Mpl300(a, o)));

    private sealed class Modular(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var all = Digits.None;
            foreach (var other in Others)
                all |= cells[other].Digits;

            return Lookup[all];
        }
    }

    public sealed class Mpl300(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var (a, b) = (A, B);
            a.Clear();
            b.Clear();

            a.Add(1);

            foreach (var other in Others)
            {
                foreach (var n in a)
                    foreach (var digit in cells[other].Digits)
                        b.Add(n * digit);

                (a, b) = (b, a);
                b.Clear();
            }

            Digits allowed = Digits.None;
            if (a.Contains(300)) allowed |= 1;
            if (a.Contains(150)) allowed |= 2;
            if (a.Contains(100)) allowed |= 3;
            if (a.Contains(075)) allowed |= 4;
            if (a.Contains(060)) allowed |= 5;
            if (a.Contains(050)) allowed |= 6;

            return allowed;
        }
    }

    private static readonly LookupDigits Lookup = Init();

    private static LookupDigits Init()
    {
        Digits a = [1, 4, 7];
        Digits b = [2, 5, 8];
        Digits c = [3, 6, 9];

        var lookup = new LookupDigits();

        foreach (var digits in Digits.All)
        {
            lookup[digits] = ((digits & a).HasAny, (digits & b).HasAny, (digits & c).HasAny) switch
            {
                (true, true, true) => _1_to_9,
                (false, true, true) => a,
                (true, false, true) => b,
                (true, true, false) => c,
                _ => Digits.None,
            };
        }
        return lookup;
    }

    private static readonly HashSet<int> A = [];
    private static readonly HashSet<int> B = [];
}
