namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_14 : CtcPuzzle
{
    public override string Title => "Killers In Hiding";

    public override string? Author => "Aron Lidé (Aspartagcus)";

    public override Uri? Url => new("https://youtu.be/Qm8DC7x9lEM");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        184│527│369
        329│164│587
        576│398│142
        ───┼───┼───
        837│619│425
        612│453│798
        945│872│613
        ───┼───┼───
        453│986│271
        791│245│836
        268│731│954
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Grid.NamedGroups("""
        AAA│ABB│BBB
        CCC│DDD│D..
        CCC│XX.│.ee
        ───┼───┼───
        .EE│.X.│..e
        EEE│.X.│eee
        E..│.X.│ee.
        ───┼───┼───
        EE.│.XX│ccc
        ..d│ddd│ccc
        bbb│bba│aaa
        """).SelectMany(Cages);

    /// <remarks>
    /// a * 10 + b = a + b + (c ..)
    /// a * 9 = c ..
    /// </remarks>
    private static Rules Cages(NamedGroup group)
    {
        PosArray cage = [.. group];
        var (ten, regular) = (cage[0], cage[2..]);

        return
        [
             new Mask(ten, Mask(group.Size - 2)),
            new CellSet(group, $"cage {group.Name}"),
            new Ten(ten, regular),
            .. Group.Select(regular, (a, o) => new Cage(ten, a, o)),
        ];

        // The masks of the ten digits are restricited.
        static Digits Mask(int size)
        {
            var min = (int)Math.Ceiling(triangle(size) / 9d);
            var max = (int)Math.Floor((triangle(_9) - triangle(_9 - size)) / 9d);
            return min..max;
        }
    }

    private sealed class Ten(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var sum = Ints.Zero;

            foreach (var other in Others)
                sum += cells[other].Digits;

            return (sum / Nine).Digits;
        }
    }

    private sealed class Cage(Pos ten, Pos appliesTo, PosArray others) : Group(appliesTo, [ten, .. others])
    {
        public Pos Ten { get; } = ten;

        public PosArray Regulars { get; } = others;

        public override Digits Restrict(SudokuCells cells)
        {
            var sum = Ints.Zero;
            sum += cells[Ten].Digits;
            sum *= 9;

            foreach (var other in Regulars)
                sum -= cells[other].Digits;

            return sum.Digits;
        }
    }

    private static readonly Ints Nine = [9];
}
