namespace SudokuSolver.Techniques2;

public static class HiddenSingles
{
    public static bool Reduce(Context context)
    {
        var todo = context.Singles.Not();

        while (todo.HasAny)
        {
            todo = todo.Dequeue(out var multiple);
            var values = new Values(context.Cells[multiple]);

            var rows = Links.Rows[multiple];
            var cols = Links.Columns[multiple];
            var sqrs = Links.Squares[multiple];

            while (values.HasAny)
            {
                var value = values.First;
                values &= ~value;

                if (!Contains(value, context, rows)
                    || !Contains(value, context, cols)
                    || !Contains(value, context, sqrs))
                {
                    context.Cells[multiple] = (uint)value;
                    context.Singles |= multiple;
                    values = default;

                    // An invalid state occured.
                    if (!NakedSingles.Reduce(context, Locations.New(multiple))) return false;

                    // We can reduce the number of TODO's.
                    todo = todo.Except(context.Singles);
                }
            }
        }

        return true;

        static bool Contains(Values value, Context context, IReadOnlyCollection<Location> links)
        {
            foreach (var link in links)
            {
                var other = new Values(context.Cells[link]);
                if ((other & value) != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
