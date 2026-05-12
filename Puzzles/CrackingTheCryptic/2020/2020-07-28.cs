namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_07_28 : CtcPuzzle
{
    public override string Title => "Mystery Killer";

    public override string? Author => "Phistomefel";

    public override Uri? Url => new("https://youtu.be/qLD1s_OHRkE");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.New("""
        296│147│853
        574│382│169
        813│965│742
        ───┼───┼───
        947│253│681
        652│871│934
        381│694│527
        ───┼───┼───
        139│526│478
        468│739│215
        725│418│396
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Killers("""
        AAC│DDD│FFF
        BAC│CDD│.F.
        BAA│CEE│G.N
        ───┼───┼───
        BHH│KKK│GNN
        HHJ│JLK│G.O
        IIJ│LLM│MMO
        ───┼───┼───
        ..R│R..│...
        P.R│SS.│...
        PQQ│QST│T..
        """);

    private static Rules Killers(string grid)
    {
        ImmutableArray<PosArray> cages = [.. Grid.NamedGroups(grid).Select(g => g.Cells.ToImmutableArray())];

        return
        [
            .. cages.Select(cage => new CellSet([..cage], "Cage")),
           .. cages.SelectMany((cage, i) => Group.Select(cage, (a, o) => new Cage(a, o, cages.RemoveAt(i))))
        ];
    }

    private sealed class Cage(Pos appliesTo, PosArray others, ImmutableArray<PosArray> cages)
        : Group(appliesTo, [.. others, .. cages.SelectMany(c => c)])
    {
        public PosArray Peers { get; } = others;

        public ImmutableArray<PosArray> Cages { get; } = cages;

        public override Digits Restrict(SudokuCells cells)
        {
            var factors = Factor(cells);

            Ints sum = [.. factors.SelectMany(f => Mps[f])];

            foreach (var peer in Peers)
                sum -= cells[peer].Digits;

            return sum.Digits;
        }

        public Ints Factor(SudokuCells cells)
        {
            var factors = Factors;

            foreach (var cage in Cages)
            {
                var sum = Ints.Zero;
                foreach (var cell in cage)
                    sum += cells[cell].Digits;

                foreach (var factor in factors)
                {
                    if ((sum & Mps[factor]).HasNone)
                        factors ^= factor;
                }
            }

            return factors;
        }
    }

    private static readonly Ints Factors = Ints.New(10..17);
    private static readonly Ints[] Mps =
    [
        [], [], [], [], [], [], [], [], [], [],
            [10, 20, 30],
            [11, 22, 33],
            [12, 24],
            [13, 26],
            [14, 28],
            [15, 30],
            [16, 32],
            [17, 34],
    ];
}
