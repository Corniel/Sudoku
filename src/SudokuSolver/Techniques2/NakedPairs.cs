namespace SudokuSolver.Techniques2;

/// <summary>Reduces naked pairs.</summary>
/// <remarks>
/// Two puzzle in a row, a column or a block having only the same pair of
/// candidates are called a Naked Pair.
/// 
/// All other appearances of the two candidates in the same row, column,
/// or block can be eliminated.
/// </remarks>
public static class NakedPairs
{
    public static bool Reduce(Context context)
    {
        var todo = context.Singles.Not();

        while (todo.HasAny)
        {
            todo = todo.Dequeue(out var pair);
            var values = new Values(context.Cells[pair]);

            // Next.
            if (values.Count != 2) continue;

            if (Other(values, context, Links.Rows[pair]) is { IsKnown: true } row)
            {
                foreach(var link in Links.Rows[pair])
                {
                    context.Cells[link] &= ~((uint)values);
                }
                context.Cells[pair] = (uint)values;
            }
            else if(Other(values, context, Links.Columns[pair]) is { IsKnown: true } col)
            {

            }
            else if (Other(values, context, Links.Squares[pair]) is { IsKnown: true } sqr)
            {

            }

        }
        return true;

        static Location Other(Values pair, Context context, IReadOnlyCollection<Location> other)
        {
            foreach(var location in other) 
            {
                if (context.Cells[location] == (uint)pair)
                {
                    return location;
                }
            }
            return Location.None;
        }
    }
}
