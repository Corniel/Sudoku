namespace Sudoku.Restrictions;

public static class NonConsecutive
{
    public static Rules New(Domino d) => New(d.A, d.B);

    public static Rules New(Pos one, Pos two) =>
    [
        .. new LookupPair(one, two, Pairs).Couple(),
        new CellSet([one, two], "Non-consecutive"),
    ];

    public static Rules New(Pos one, Pos two, Pos tre) =>
    [
        new Triple(one, [two, tre]),
        new Triple(two, [tre, one]),
        new Triple(tre, [one, two]),
    ];

    public static Rules New(PosSet set) => set.ToImmutableArray() switch
    {
        { Length: 2 } cells => New(cells[0], cells[1]),
        { Length: 3 } cells => New(cells[0], cells[1], cells[2]),
        _ => throw new NotSupportedException($"NonNon-consecutive groups with size {set.Count} are not supported."),
    };

    private static readonly LookupDigits Pairs = LookupPair.Init(d => ~Digits.Between(d - 1, d + 1));
    private static readonly LookupDigits Triples = InitTriple();

    private sealed class Triple(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
            => Triples[[cells[Others[0]].Digit, cells[Others[1]].Digit]];
    }

    private static LookupDigits InitTriple()
    {
        var lookup = new LookupDigits();
        foreach (var digits in Digits.All)
        {
            var (min, max) = (digits.Min(), digits.Max());
            var delta = max - min;

            lookup[digits] = _1_to_9 ^ (digits.Count, delta) switch
            {
                (2, 1) => [min - 1, max + 1],
                (2, 2) => [min + 1],
                _ => default,
            };
        }
        return lookup;
    }
}
