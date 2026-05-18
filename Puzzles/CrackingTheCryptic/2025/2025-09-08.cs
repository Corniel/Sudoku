namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_09_08 : CtcPuzzle
{
    public override string Title => "Four at a Time";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/9LDrEYKa-aQ");

    public override O Duration => O.ms10;

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        4..│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│4..
        ───┼───┼───
        .9.│...│...
        ...│...│...
        ...│...│5..
        """);

    public override Cells Solution { get; } = Cells.New("""
        961│437│258
        485│269│173
        237│851│649
        ───┼───┼───
        842│716│395
        316│594│827
        759│328│416
        ───┼───┼───
        593│642│781
        628│175│934
        174│983│562
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + Thermos("""
        A..│...│...
        .B.│...│...
        ..C│...│..a
        ───┼───┼───
        ...│D..│.b.
        ..E│...│c..
        .F.│..d│...
        ───┼───┼───
        G..│...│e..
        ...│...│.f.
        ...│...│..g
        """)
        + Thermos("""
        ..a│...│..g
        ...│b..│.f.
        ...│.c.│e..
        ───┼───┼───
        ...│..d│...
        ...│...│...
        ...│D..│...
        ───┼───┼───
        ..C│.E.│...
        .B.│..F│...
        A..│...│G..
        """);

    private static Rules Thermos(string grid)
        => Lines.Parse(grid).SelectMany(Thermos);

    private static Rules Thermos(Line line)
    {
        for (var i = 0; i < line.Length; i++)
            yield return new SlowThermometer(line[i], line[..i], line[(i + 1)..]);
    }

    private sealed class SlowThermometer(Pos appliesTo, PosArray before, PosArray after) : Restriction
    {
        public Pos AppliesTo { get; } = appliesTo;

        public PosArray Before { get; } = [.. before.Reverse()];

        public PosArray After { get; } = after;

        public PosSet Cells { get; } = [.. before, .. after];

        public Digits Restrict(SudokuCells cells)
        {
            var bef = Step.Walk(Before, cells);
            var aft = Step.Walk(After, cells);

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

            return _1_to_9;
        }
    }

    private readonly record struct Step(int First, int Sign)
    {
        public static Step Walk(PosArray steps, SudokuCells graph)
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
