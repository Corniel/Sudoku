namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_02_14 : CtcPuzzle
{
    private static readonly ImmutableArray<ImmutableArray<int>> Sums = [.. Init()];

    public override string Title => "Valentine's Sudoku";

    public override string? Author => "Aart van de Wetering";

    public override Uri? Url => new("https://youtu.be/Lykt7NyMC4c");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        287│531│946
        134│296│875
        569│847│321
        ───┼───┼───
        748│129│563
        321│654│789
        956│783│214
        ───┼───┼───
        695│472│138
        812│365│497
        473│918│652
        """);

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        1..│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│6.4│...
        ...│...│...
        ───┼───┼───
        .9.│...│...
        ...│...│4.7
        ..3│9.8│6..
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Groups.EvenOdd("""
        ..O│O.O│O..
        .OE│EOE│EO.
        OE.│.E.│.EO
        ───┼───┼───
        OE.│...│.EO
        OE.│...│.EO
        .OE│...│EO.
        ───┼───┼───
        ..O│E.E│O..
        ...│OEO│...
        ...│.O.│...
        """)
        + Houses.Boxes[4].Select((pos, i) => new Sum(pos, i));

    private sealed class Sum(Pos appliesTo, int index) : Group(appliesTo, [.. Houses.Boxes[4]])
    {
        public int Index { get; } = index;

        public override Digits Restrict(SudokuCells cells)
        {
            Ints options = [.. range(Sums.Length)];

            for (var i = 0; i < 9; i++)
            {
                if (i == Index) continue;

                var digits = cells[Others[i]].Digits;

                // Remove options that can not match based on the index of the cell.
                foreach (var option in options.Where(o => !digits.Contains(Sums[o][i])))
                    options ^= option;
            }

            return [.. options.Select(o => Sums[o][Index])];
        }
    }

    private static IEnumerable<ImmutableArray<int>> Init()
    {
        int[] digits = [1, 2, 3, 5, 7, 8, 9];

        return digits.Permutations().Where(row3_plus_row4_is_row5).Select(Sum);

        static ImmutableArray<int> Sum(int[] per) => [per[0], per[1], per[2], 6, per[3], 4, per[4], per[5], per[6]];

        static bool row3_plus_row4_is_row5(int[] per)
        {
            if ((per[4] - per[0]) is not 5 and not 6) return false;

            var row3 = (per[0] * 100) + (per[1] * 10) + per[2];
            var row4 = /*....*/ 600 + (per[3] * 10) + 4;
            var row5 = (per[4] * 100) + (per[5] * 10) + per[6];

            return row3 + row4 == row5;
        }
    }
}
