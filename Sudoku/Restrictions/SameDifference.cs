namespace Sudoku.Restrictions;

public sealed class SameDifference : Restriction
{
    public SameDifference(Pos appliesTo, Line cells)
    {
        AppliesTo = appliesTo;
        Array = [.. cells];
        Cells = [.. cells];
        var index = cells.IndexOf(appliesTo);
        Neigbors = [.. cells.Index().Where(x => int.Abs(x.Index - index) is 1).Select(x => x.Item)];
    }

    public Pos AppliesTo { get; }

    public PosArray Array { get; }

    public PosArray Neigbors { get; }

    public PosSet Cells { get; }

    public Digits Restrict(SudokuCells cells)
    {
        var deltas = Deltas(cells[Array[0]].Digits, cells[Array[1]].Digits);

        for (var i = 2; i < Array.Length; i++)
        {
            var add = Deltas(cells[Array[i]].Digits, cells[Array[i - 1]].Digits);

            // No shared delta.
            if ((deltas &= add) is 0) return Digits.None;
        }

        var dt = new Ints(deltas);

        Ints allowed = cells[Neigbors[0]].Digits;
        allowed = (allowed + dt) | (allowed - dt);

        if (Neigbors.Length >= 2)
        {
            Ints second = cells[Neigbors[1]].Digits;
            second = (second + dt) | (second - dt);
            allowed &= second;
        }

        return allowed.Digits;
    }

    private static int Deltas(Digits ls, Digits rs)
    {
        var deltas = 0;

        foreach (var l in ls)
            foreach (var r in rs)
                deltas |= 1 << int.Abs(l - r);

        return deltas;
    }
}
