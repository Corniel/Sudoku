namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_11_18 : CtcPuzzle
{
    public override string Title => "Equivalenee";

    public override string? Author => "Michael Lefkowitz";

    public override Uri? Url => new("https://youtu.be/vx2taaxQ2YI");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.Parse("""
        192│845│376
        648│379│152
        735│621│498
        ───┼───┼───
        983│564│721
        526│917│843
        471│283│965
        ───┼───┼───
        367│458│219
        854│192│637
        219│736│584
        """);

    public override Rules Constraints { get; } = Rules.Standard + Cages();

    private static List<Cage> Cages()
    {
        var named = NamedCage.Parse("""
            XCC│ab.│...
            XX.│abY│Y.c
            .dE│E.Y│.ec
            ───┼───┼───
            .d.│AAf│.ec
            ...│g.f│...
            DD.│gBB│hMM
            ───┼───┼───
            ij.│...│h..
            ij.│ZFF│.kl
            ...│ZZG│Gkl
            """);

        var cages = new List<Cage>();

        foreach (var n in named)
            cages.AddRange(new Cage([..n.Cells], cages));

        return cages;
    }

    public sealed class Cage(ImmutableArray<Pos> cells, List<Cage> cages) : Rule(cells)
    {
        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            .. Group.Select(cells, (a, o) => new Reducer(a, o, cages)),
        ];

        public sealed class Reducer(Pos appliesTo, ImmutableArray<Pos> other, List<Cage> cages) : Group(appliesTo, other)
        {
            private readonly IReadOnlyCollection<Cage> Cages = cages;

            public override Digits Restrict(SudokuCells cells)
            {
                var sum = Ints.All;
                var iterator = Cages.GetEnumerator();
                while (sum.HasAny && iterator.MoveNext())
                    sum &= Sum(iterator.Current, cells);

                foreach (var digits in Others.Select(o => cells[o].Digits))
                    sum -= digits;

                return sum.Digits;
            }

            private static Ints Sum(Cage cage, SudokuCells cells)
            {
                var sum = Ints.Zero;

                foreach (var digits in cage.Cells.Select(c => cells[c].Digits))
                    sum += digits;

                return sum;
            }
        }
    }
}
