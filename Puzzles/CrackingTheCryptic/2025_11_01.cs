
namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_11_01 : CtcPuzzle
{
    public override string Title => "Parity Patrol 101";

    public override string? Author => "Jonesy";

    public override Uri? Url => new("https://youtu.be/3ZltSGljbh8");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.Parse("""
        893│625│147
        714│398│562
        526│714│893
        ───┼───┼───
        275│463│981
        631│879│254
        948│152│736
        ───┼───┼───
        187│946│325
        459│231│678
        362│587│419
        """);

    public override Rules Constraints { get; }
        = Rules.Killer("""
            ...│AAA│.BB
            CC.│DDE│EFF
            ..G│..E│...
            ───┼───┼───
            .GG│...│HHH
            .II│JJJ│HK.
            ..I│..L│.KK
            ───┼───┼───
            ...│.LL│...
            MMN│...│OOO
            ..N│NN.│...
            A = 13  B = 11  C = 8   D = 12  E = 17  F = 8   G = 18  H = 20
            I = 12  J = 24  K = 14  L = 12  M = 9   N = 24  O = 21
            """)
        + Checks();

    private static IEnumerable<ParityCheck> Checks()
    {
        foreach (var pos in Pos.All)
        {
            if (pos.N() is { } n1 && n1.N() is { } n2)
                yield return new(pos, [n1, n2]);

            if (pos.S() is { } s1 && s1.S() is { } s2)
                yield return new(pos, [s1, s2]);

            if (pos.N() is { } n && pos.S() is { } s)
                yield return new(pos, [n, s]);

            if (pos.W() is { } w1 && w1.W() is { } w2)
                yield return new(pos, [w1, w2]);

            if (pos.E() is { } e1 && e1.E() is { } e2)
                yield return new(pos, [e1, e2]);

            if (pos.W() is { } w && pos.E() is { } e)
                yield return new(pos, [w, e]);
        }
        yield break;
    }

    public sealed class ParityCheck(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
            => (Par(cells[Others[0]].Digits), Par(cells[Others[1]].Digits)) switch
            {
                (Parity.O, Parity.O) => Digits.Even,
                (Parity.E, Parity.E) => Digits.Odd,
                _ => Digits._1_to_9,
            };

        private static Parity Par(Digits digits) => digits switch
        {
            _ when (digits & Digits.Even).HasNone => Parity.O,
            _ when (digits & Digits.Odd).HasNone => Parity.E,
            _ => Parity.None,
        };

        private enum Parity
        {
            None = 0,
            O = 1,
            E = 2,
        }
    }
}
