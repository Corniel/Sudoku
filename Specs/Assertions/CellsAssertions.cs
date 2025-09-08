using AwesomeAssertions.Execution;
using SudokuSolver.Solvers;
using System.Diagnostics.CodeAnalysis;

namespace AwesomeAssertions;

public sealed class CellsAssertions(Cells subject)
{
    private readonly AssertionChain Chain = AssertionChain.GetOrCreate();

    public Cells Subject { get; } = subject;

    public void BeSolved([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        var reference = Backtracker.Solve(Clues.Parse(Subject.ToString()));

        Be(reference, because, becauseArgs);
    }

    public void Be(string expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        => Be(Cells.Parse(expected), because, becauseArgs);

    public void Be(Cells expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        Chain
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject.Equals(expected))
            .WithDefaultIdentifier("Puzzle")
            .FailWith($"Expected:\n{expected}\n\nAcutal:\n{Subject}");
    }
}
