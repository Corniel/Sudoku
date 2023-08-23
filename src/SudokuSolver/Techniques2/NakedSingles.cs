namespace SudokuSolver.Techniques2;


public static class NakedSingles
{
    public static bool Reduce(Context context) => Reduce(context, context.Singles);
    public static bool Reduce(Context context, Locations todo)
    {
        while (todo.HasAny)
        {
            todo = todo.Dequeue(out var single);
            var values = new Values(context.Cells[single]);

            var except = ~values;

            var links = Links.All[single];

            foreach (var link in links)
            {
                var cell = new Values(context.Cells[link]);

                // already single: skip.
                if (cell.IsSingle) { continue; }

                var reduced = cell & except;

                // inconsistency.
                if (reduced.IsInvalid)
                {
                    return false;
                }

                context.Cells[link] = (uint)reduced;

                // single
                if (reduced.IsSingle)
                {
                    context.Singles |= link;
                    todo |= link;
                }
            }
        }
        return true;
    }
}
