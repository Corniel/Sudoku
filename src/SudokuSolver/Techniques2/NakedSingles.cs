namespace SudokuSolver.Techniques2;


public static class NakedSingles
{
    public static bool Reduce(Context context) => Reduce(context, context.Singles);
    public static bool Reduce(Context context, Locations todo)
    {
        while (todo.HasAny)
        {
            todo = todo.Dequeue(out var single);

            var mask = ~context.Cells[single];

            var links = Links.All[single];

            foreach(var link in links)
            {
                var cell = context.Cells[link];

                // already single: skip.
                if ((cell & (cell - 1)) == 0) { continue; }

                var reduced = cell & mask;
                
                // inconsistency.
                if (reduced == 0) { return false; }

                context.Cells[link] = reduced;

                // single
                if ((reduced & reduced -1) == 0)
                {
                    context.Singles |= link;
                    todo = link;
                }
            }
        }
        return true;
    }
}
