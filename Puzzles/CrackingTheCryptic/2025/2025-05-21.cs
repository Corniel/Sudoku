namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_05_21 : CtcPuzzle
{
    public override string Title => "Stepped Themos";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/AdSOJQ3huN0");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.New("""
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        7..|...|...
        ..9|...|...
        """);

    public override Cells Solution { get; } = Cells.New("""
        541|627|893
        982|531|674
        376|984|521
        ---+---+---
        625|493|718
        137|865|942
        498|172|356
        ---+---+---
        813|259|467
        754|316|289
        269|748|135
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + Triples().SelectMany(NonConsecutives)
        + Lines.Thermometer("""
         ...|...|.P.
         FE.|...|NO.
         .DC|..L|M..
         ---+---+---
         ..B|A.K|...
         ...|...|...
         ...|a.k|l..
         ---+---+---
         ..c|b..|mn.
         .ed|...|.op
         .f.|...|...
         """);

    private static Rules NonConsecutives(PosSet cells)
        => Group.Select(cells, (a, o) => new NonConsecutive(a, o));

    public sealed class NonConsecutive(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var index = Digits.New(cells[Others[0]].Digit, cells[Others[1]].Digit);
            return Loookup[index.GetHashCode()];
        }

        private static readonly ImmutableArray<Digits> Loookup = Init();

        private static ImmutableArray<Digits> Init()
        {
            var lookup = new Digits[1 << (_9 + 1)];

            lookup[0] = Digits._1_to_9;

            for (var i = 0; i < 9; i++)
            {
                lookup[1 << i] = Digits._1_to_9;
            }

            for (var i = 1; i <= 9; i++)
            {
                for (var j = i; j <= 9; j++)
                {
                    var index = Digits.New(i, j).GetHashCode();

                    lookup[index] = (j - i) switch
                    {
                        0 => ~Digits.New(i),
                        1 => ~Digits.Between(i - 1, j + 1),
                        2 => ~Digits.Between(i - 0, j + 0),
                        _ => Digits._1_to_9,
                    };
                }
            }
            return [.. lookup];
        }
    }

    private static IEnumerable<PosSet> Triples()
    {
        for (var f = 0; f < _9; f++)
        {
            for (var s = 0; s < 9; s += 3)
            {
                yield return PosSet.New((f, s), (f, s + 1), (f, s + 2));
                yield return PosSet.New((s, f), (s + 1, f), (s + 2, f));
            }
        }
    }
}
