using Sudoku.Restrictions;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_05_21 : CtcPuzzle
{
    public override string Title => "Stepped Themos";
    public override string? Author => "Aad van de Wetering";
    public override Uri? Url => new("https://youtu.be/AdSOJQ3huN0");
    public override O Duration => O.ms;

    public override Clues Clues { get; } = Clues.Parse("""
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

    public override Cells Solution { get; } = Cells.Parse("""
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

    public override Rules Constraints { get; } =
        Rules.Standard
        + NonConsecutive.Create()
        + Thermometer.Parse("""
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|1..|...
        ---+---+---
        ..3|2..|...
        .54|...|...
        .6.|...|...
        """)
        + Thermometer.Parse("""
        ...|...|...
        65.|...|...
        .43|...|...
        ---+---+---
        ..2|1..|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        """)
        + Thermometer.Parse("""
        ...|...|.6.
        ...|...|45.
        ...|..2|3..
        ---+---+---
        ...|..1|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        """)
        + Thermometer.Parse("""
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|..1|2..
        ---+---+---
        ...|...|34.
        ...|...|.56
        ...|...|...
        """);

    public sealed class NonConsecutive(PosSet cells) : Set([..cells])
    {
        public override ImmutableArray<Restriction> Restrictions { get; } = Reducer.Reducers([.. cells]);

        public sealed class Reducer(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
        {
            public override Digits Restrict(SudokuCells graph)
            {
                var index = Digits.New(graph[Others[0]].Digit, graph[Others[1]].Digit);
                return Loookup[index.GetHashCode()];
            }

            public static ImmutableArray<Restriction> Reducers(ImmutableArray<Pos> cells) =>
            [
                new Reducer(cells[0], cells.Remove(cells[0])),
                new Reducer(cells[1], cells.Remove(cells[1])),
                new Reducer(cells[2], cells.Remove(cells[2])),
            ];

            private static readonly ImmutableArray<Digits> Loookup = Init();

            private static ImmutableArray<Digits> Init()
            {
                var lookup = new Digits[1 << 9 + 1];

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

        public static IEnumerable<NonConsecutive> Create()
        {
            for (var f = 0; f < _9; f++)
            {
                for (var s = 0; s < 9; s += 3)
                {
                    yield return new NonConsecutive([(f, s), (f, s + 1), (f, s + 2)]);
                    yield return new NonConsecutive([(s, f), (s + 1, f), (s + 2, f)]);
                }
            }
        }
    }
}
