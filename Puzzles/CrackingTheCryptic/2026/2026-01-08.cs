namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_01_08 : CtcPuzzle
{
    public override string Title => "Paper Snowflake";

    public override string? Author => "Kainapple";

    public override Uri? Url => new("https://youtu.be/Ux6LYrFtjFc");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        936│452│817
        258│917│463
        471│638│295
        ───┼───┼───
        347│286│951
        612│795│348
        589│341│672
        ───┼───┼───
        793│524│186
        125│869│734
        864│173│529
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.Arrow("""
        .lk│...│...
        ...│j..│...
        ..D│...│...
        ───┼───┼───
        .C.│E..│f..
        .BH│I.g│...
        .AG│.hd│...
        ───┼───┼───
        ...│..b│c..
        ...│.a.│...
        ...│...│...
        """)
        + Shadings();

    public static Rules Shadings()
    {
        foreach (var pos in Pos.All)
        {
            var (r, c) = pos;
            var (dr, dc) = (Math.Abs(4 - r), Math.Abs(4 - c));

            var reflections = new HashSet<Pos>()
            {
                new(4 - dc, 4 - dr), new(4 - dr, 4 - dc),
                new(4 - dc, 4 + dr), new(4 - dr, 4 + dc),
                new(4 + dc, 4 - dr), new(4 + dr, 4 - dc),
                new(4 + dc, 4 + dr), new(4 + dr, 4 + dc),
            };
            reflections.Remove(pos);

            foreach (var reflect in reflections)
                yield return new LookupPair(pos, reflect, Shading);
        }
    }

    public static readonly LookupDigits Shading = LookupPair.Init(
    [
        /* ? */ _1_to_9,
        /* 1 */ Digits.AtMost(6),
        /* 2 */ Digits.AtMost(6),
        /* 3 */ Digits.AtMost(6),
        /* 4 */ _1_to_9,
        /* 5 */ _1_to_9,
        /* 6 */ _1_to_9,
        /* 7 */ Digits.AtLeast(4),
        /* 8 */ Digits.AtLeast(4),
        /* 9 */ Digits.AtLeast(4),
    ]);
}
