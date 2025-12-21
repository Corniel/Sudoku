using DynamicSolver;
using Puzzles;
using Puzzles.CrackingTheCryptic;
using Puzzles.Killer;
using Puzzles.PuzzleBank;
using Specs.Tools;

namespace Specs.Solvers.DynamicSolver_specs;

[Explicit]
public class Solves
{
    [Test]
    public void Without_Clues()
    {
        var cells = Solver.Solve(Clues.None, Rules.Standard);
        cells.Should().Be("""
            123│456│789
            456│789│123
            789│123│456
            ───┼───┼───
            261│894│375
            894│537│261
            375│261│948
            ───┼───┼───
            612│378│594
            947│615│832
            538│942│617
            """);
    }

    [Test]
    public void standard_Diabolical()
    {
        var puzzles = PuzzleBankPuzzle.Diabolical.Take(10_000).ToArray();

        using var _ = Logger.Options();

        foreach (var puzzle in puzzles)
            TestSolver.Solve(puzzle);

        var total = decimal.Round(1m * Iterator.Options.Sum() / puzzles.Length, 2);

        total.Should().Be(214.47m);
    }

    [Test]
    public void Killer_sudoku()
    {
        var puzzles = KillerPuzzle.Load().ToArray();

        using var _ = Logger.Options();

        foreach (var puzzle in puzzles)
            TestSolver.Solve(puzzle);

        var total = decimal.Round(1m * Iterator.Options.Sum() / puzzles.Length, 2);

        total.Should().Be(14_135.75m);
    }

    [Test]
    public void Fantacy_()
    {
        Puzzle[] puzzles =
        [
            new _2020_04_12(),
            new _2024_11_18(),
            new _2025_03_25(),
            new _2025_08_07(),
            new _2025_11_17(),
            new _2025_12_11(),
            new _2025_12_15(),
            new _2025_12_17(),
        ];

        using var _ = Logger.Options();


        foreach (var puzzle in puzzles)
            TestSolver.Solve(puzzle);

        var total = decimal.Round(1m * Iterator.Options.Sum() / puzzles.Length, 2);

        total.Should().Be(126_623.12M);
    }
}
