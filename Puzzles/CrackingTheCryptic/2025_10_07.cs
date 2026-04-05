namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_10_07 : CtcPuzzle
{
    public override string Title => "Golden Arrow";

    public override string? Author => "James Kopp";

    public override Uri? Url => new("https://youtu.be/Y23x1sGzWJo");

    public override O Duration => O.μs10;

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

    public override Rules Constraints { get; }
        = Rules.AntiKnight
        + AntiKing.All
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
            => Lines.Parse(str).Select(line => new GoldenArrow(line)).Single();

        /// <inheritdoc />
        /// <remarks>
        /// The only sums possible:
        /// 27 = [1,3,4,5,6,8]
        /// 28 = [1,3,4,5,6,9]
        /// 29 = [1,3,4,6,7,8].
        /// </remarks>
        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            new Mask(cells[0], [2]),
            new Mask(cells[1], [7, 8, 9]),
            new LookupPair((4, 4), cells[1], Center),
            .. cells[2..].Select(cell => new LookupPair(cell, cells[1], Shaft)),
        ];
    }

    private static readonly LookupDigits Center = LookupPair.Init(d => d switch
    {
        7 => [9],
        8 => [7],
        9 => [5],
        _ => Digits.None,
    });

    private static readonly LookupDigits Shaft = LookupPair.Init(d => d switch
    {
        7 => [1, 3, 4, 5, 6, 8],
        8 => [1, 3, 4, 5, 6, 9],
        9 => [1, 3, 4, 6, 7, 8],
        _ => Digits.None,
    });
}
