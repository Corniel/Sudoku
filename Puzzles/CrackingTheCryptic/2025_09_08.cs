namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_08 : CtcPuzzle
{
    public override string Title => "Four at a Time";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/9LDrEYKa-aQ");

    public override O Duration => O.s10;

    public override Clues Clues { get; } = Clues.Parse("""
        ...|...|...
        4..|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|4..
        ---+---+---
        .9.|...|...
        ...|...|...
        ...|...|5..
        """);

    public override Rules Constraints { get; } =
        Rules.Standard
        + SlowThermometer.Parse("""
            1..|...|...
            .2.|...|...
            ..3|...|...
            ---+---+---
            ...|4..|...
            ..5|...|...
            .6.|...|...
            ---+---+---
            7..|...|...
            ...|...|...
            ...|...|...
            """)
        + SlowThermometer.Parse("""
            ..1|...|..7
            ...|2..|.6.
            ...|.3.|5..
            ---+---+---
            ...|..4|...
            ...|...|...
            ...|...|...
            ---+---+---
            ...|...|...
            ...|...|...
            ...|...|...
            """)
        + SlowThermometer.Parse("""
            ...|...|...
            ...|...|...
            ...|...|...
            ---+---+---
            ...|...|...
            ...|...|...
            ...|4..|...
            ---+---+---
            ..3|.5.|...
            .2.|..6|...
            1..|...|7..
            """)
        + SlowThermometer.Parse("""
            ...|...|...
            ...|...|...
            ...|...|..1
            ---+---+---
            ...|...|.2.
            ...|...|3..
            ...|..4|...
            ---+---+---
            ...|...|5..
            ...|...|.6.
            ...|...|..7
            """);

    public override Cells Solution { get; } = Cells.Parse("""
        961|437|258
        485|269|173
        237|851|649
        ---+---+---
        842|716|395
        316|594|827
        759|328|416
        ---+---+---
        593|642|781
        628|175|934
        174|983|562
        """);

    public sealed class SlowThermometer(ImmutableArray<Pos> cells) : Rule(cells)
    {
        public override ImmutableArray<Restriction> Restrictions { get; }
            = [.. cells.Select((c, i) => new Reduce(c, cells[..i], cells[(i + 1)..]))];

        public static SlowThermometer Parse(string str) => new([.. Clues.Parse(str).OrderBy(c => c.Digit).Select(c => c.Pos)]);

        public sealed class Reduce(Pos appliesTo, ImmutableArray<Pos> before, ImmutableArray<Pos> after) : Restriction
        {
            public Pos AppliesTo { get; } = appliesTo;

            public ImmutableArray<Pos> Before { get; } = [.. before.Reverse()];

            public ImmutableArray<Pos> After { get; } = after;

            public PosSet Links { get; } = [.. before, .. after];

            public Digits Restrict(SudokuCells graph)
            {
                var bef = Step.Walk(Before, graph);
                var aft = Step.Walk(After, graph);

                // ASC based on after
                if (aft.Sign > 0)
                {
                    return Digits.Between(bef.First, aft.First);
                }

                // DESC based on before
                if (bef.Sign > 0)
                {
                    return Digits.Between(aft.First, bef.First);
                }

                // DESC based on after.
                if (aft.Sign < 0)
                {
                    return bef.First is 0
                        ? Digits.AtLeast(aft.First)
                        : Digits.Between(aft.First, bef.First);
                }

                // ASC based on before
                if (bef.Sign < 0)
                {
                    return aft.First is 0
                        ? Digits.AtLeast(bef.First)
                        : Digits.Between(bef.First, aft.First);
                }

                if (aft.First is not 0 && bef.First is not 0)
                {
                    var min = aft.First;
                    var max = bef.First;
                    if (max < min) (min, max) = (max, min);
                    return Digits.Between(min, max);
                }

                return Digits._1_to_9;
            }
        }
    }

    private readonly record struct Step(int First, int Sign)
    {
        public static Step Walk(ImmutableArray<Pos> steps, SudokuCells graph)
        {
            var first = 0;

            foreach (var step in steps)
            {
                var next = graph[step].Digit;

                if (next is not 0 && next != first)
                {
                    if (first is not 0) return new(first, next - first);
                    first = next;
                }
            }
            return new(first, 0);
        }
    }
}
