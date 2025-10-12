using SudokuSolver.Restrictions;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_10_07 : CtcPuzzle
{
    public override string Title => "Golden Arrow";
    public override string? Author => "James Kopp";
    public override Uri? Url => new("https://youtu.be/Y23x1sGzWJo");
    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.Parse("""
        582│941│736
        736│582│941
        941│736│582
        ───┼───┼───
        658│294│173
        173│658│294
        294│173│658
        ───┼───┼───
        365│829│417
        417│365│829
        829│417│365
        """);

    public override Clues Clues { get; } = Clues.Parse("""
        ...│..1│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    public override Rules Constraints { get; }
        = Rules.Standard
        + AntiKing.All
        + AntiKnight.All
        + DutchWhispers.Parse("""
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│ABC│...
            ...│H.D│...
            ...│GFE│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            """)
        + GoldenArrow.Parse("""
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│ABC│...
            ...│H.D│...
            ...│GFE│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            """)
        ;

    public sealed class GoldenArrow(ImmutableArray<Pos> cells) : Rule
    {
        public static GoldenArrow Parse(string str)
            => SudokuSolver.Parsing.Lines.Parse(str).Select(line => new GoldenArrow(line)).Single();

        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            // The only sums possible:
            // 27 = [1,3,4,5,6,8]
            // 28 = [1,3,4,5,6,9]
            // 29 = [1,3,4,6,7,8]
            new Mask(cells[0], [2]),
            new Mask(cells[1], [7, 8, 9]),
            new Center((4, 4), cells[1]),
            .. cells[2..].Select(cell => new Shaft(cell, cells[1])),
        ];

        private sealed class Shaft(Pos appliesTo, Pos other) : Pair(appliesTo, other)
        {
            public override Candidates Restrict(int value) => value switch
            {
                7 => [1, 3, 4, 5, 6, 8],
                8 => [1, 3, 4, 5, 6, 9],
                9 => [1, 3, 4, 6, 7, 8],
                _ => Candidates.None,
            };
        }

        private sealed class Center(Pos appliesTo, Pos other) : Pair(appliesTo, other)
        {
            public override Candidates Restrict(int value) => value switch
            {
                7 => [9],
                8 => [7],
                9 => [5],
                _ => Candidates.None,
            };
        }

        internal static void Generate()
        {
            foreach (var candidates in Candidates.All.Where(c => c.Count is 6))
            {
                var sum = candidates.Sum();
                var ten = sum / 10;
                var one = sum % 10;
                var sm_ = Candidates.New(ten, one);

                var okay = sm_.Count is 2 && (candidates | sm_).Count is 8 && Math.Abs(ten - one) >= 4;
                if (okay)
                {
                    Console.WriteLine($"{sum} = {candidates}");
                }
            }
        }
    }
}
