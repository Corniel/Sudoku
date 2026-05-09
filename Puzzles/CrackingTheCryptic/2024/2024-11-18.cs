namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_11_18 : CtcPuzzle
{
    public override string Title => "Equivalenee";

    public override string? Author => "Michael Lefkowitz";

    public override Uri? Url => new("https://youtu.be/vx2taaxQ2YI");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
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

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + AllCages();

    private static Rules AllCages()
    {
        var groups = Grid.NamedGroups("""
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

        ImmutableArray<PosArray> cages = [.. groups.Select(g => g.Cells.ToImmutableArray())];

        return groups.SelectMany(gr => Group.Select(gr, (a, o) => new Reducer(a, o, cages)));
    }

    private sealed class Reducer(Pos appliesTo, PosArray other, ImmutableArray<PosArray> cages)
        : Group(appliesTo, other)
    {
        private readonly ImmutableArray<PosArray> Cages = cages;

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

        private static Ints Sum(PosArray cage, SudokuCells cells)
        {
            var sum = Ints.Zero;

            foreach (var digits in cage.Select(c => cells[c].Digits))
                sum += digits;

            return sum;
        }
    }
}
