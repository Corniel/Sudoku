namespace SudokuSolver.Solvers;

public static partial class Solver
{
    public static Cells Solve(Clues clues) => Solve(clues, Rules.Standard, ReduceOptions.Default);

    public static Cells Solve(Clues clues, Rules rules, ReduceOptions? options = null)
    {
        options ??= ReduceOptions.Default;
        var cells = Cells.Empty;
        var context = new Context(rules);

        Reduce(clues, cells, context, options);

        if (options.Backtracker && context.Todos.HasAny)
        {
            var backtracker = Backtracker.New(context);
            backtracker.Solve(cells);
        }
        return cells;
    }
}
