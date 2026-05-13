namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_04 : CtcPuzzle
{
    public override string Title => "Packing Problem";

    public override string? Author => "clover!";

    public override Uri? Url => new("https://youtu.be/OMqUAduLZfI");

    public override O Duration => O.s10;

    public override Cells Solution { get; } = Cells.New("""
        865|793|421
        743|251|869
        291|486|753
        ---+---+---
        674|932|518
        952|618|347
        318|547|692
        ---+---+---
        186|324|975
        537|169|284
        429|875|136
        """);

    protected override RuleSet GetConstraints() => RuleSet.Standard + Cages();

    private static Rules Cages() => Grid.NamedGroups("""
        ...|.BB|BBC
        .EE|DDD|..C
        EEA|AD.|.CC
        ---+---+---
        ..A|ALM|MMM
        GFF|.LL|NN.
        GFF|.L.|.NN
        ---+---+---
        G..|III|I..
        GH.|JJJ|JKK
        HHH|...|KK.
        """)
        .SelectMany(c => c.Name is 'A' ? Repeating.All([.. c.Cells]) : Cage.All([.. c.Cells]));

    private sealed class Cage(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public static Rules All(PosArray cells) =>
        [
            new Cage(cells[0], cells.Remove(cells[0])),
            new Cage(cells[1], cells.Remove(cells[1])),
            new Cage(cells[2], cells.Remove(cells[2])),
            new Cage(cells[3], cells.Remove(cells[3])),
        ];

        public override Digits Restrict(SudokuCells cells)
        {
            var allowed = Digits.None;

            foreach (var set in Sets)
                allowed |= Match(set);

            return allowed;

            Digits Match(Digits set)
            {
                var match = set;

                foreach (var other in Others.Select(o => cells[o].Digits).OrderBy(x => x.Count))
                {
                    var overlap = other & match;

                    if (overlap.HasSingle)
                        match ^= overlap;
                    else if (other.HasSingle) return Digits.None;
                }

                return match;
            }
        }

        private static readonly ImmutableArray<Digits> Sets =
        [
           [1, 2, 3, 6],
           [1, 2, 4, 7],
           [1, 2, 5, 8],
           [1, 2, 6, 9],
           [1, 3, 4, 8],
           [1, 3, 5, 9],
           [2, 3, 4, 9],
        ];
    }

    private sealed class Repeating(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public static Rules All(PosArray cells) =>
        [
            new Repeating(cells[0], cells.Remove(cells[0])),
            new Repeating(cells[1], cells.Remove(cells[1])),
            new Repeating(cells[2], cells.Remove(cells[2])),
            new Repeating(cells[3], cells.Remove(cells[3])),
        ];

        public override Digits Restrict(SudokuCells cells)
        {
            var allowed = Digits.None;

            foreach (var set in Sets)
                allowed |= Match(set);

            return allowed;

            Digits Match(ImmutableArray<int> set)
            {
                var remaining = set.ToList();

                if (!Others
                    .Select(o => cells[o].Digits)
                    .Where(d => d.HasSingle)
                    .All(x => remaining.Remove(x.Min()))) return Digits.None;

                var match = Digits.None;

                foreach (var digit in remaining)
                    match |= digit;
                return match;
            }
        }

        private static readonly ImmutableArray<ImmutableArray<int>> Sets =
        [
            [1, 2, 3, 6],
            [1, 2, 4, 7],
            [1, 2, 5, 8],
            [1, 2, 6, 9],
            [1, 3, 4, 8],
            [1, 3, 5, 9],
            [2, 3, 4, 9],

            [1, 1, 2, 4],
            [1, 1, 3, 5],
            [1, 1, 4, 6],
            [1, 1, 5, 7],
            [1, 1, 6, 8],
            [1, 1, 7, 9],

            [1, 2, 2, 5],
            [1, 3, 3, 7],
            [1, 4, 4, 9],
            [2, 3, 3, 8],
        ];
    }
}
