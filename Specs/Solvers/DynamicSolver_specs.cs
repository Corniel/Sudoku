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
        using var _ = Logger.Options();

        var cells = Solver.Solve(Clues.None, RuleSet.Standard);
        cells.IsSolved.Should().BeTrue();
    }

    [Test]
    public void standard_Easy() => Run(70.29, PuzzleBankPuzzle.Easy.Take(5_000));

    [Test]
    public void standard_Medium() => Run(117.56, PuzzleBankPuzzle.Medium.Take(5_000));

    [Test]
    public void Standard_Hard() => Run(155.00, PuzzleBankPuzzle.Hard.Take(5_000));
    
    [Test]
    public void Standard_Diabolical() => Run(198.34, PuzzleBankPuzzle.Diabolical.Take(5_000));

    [Test]
    public void Standard_hardest() => Run(
        448.18,
        [
            .. PuzzleBankPuzzle.Diabolical.OrderByDescending(p => p.Level).Take(1_000),
            .. CtcPuzzle.Classics
        ]);

    [Test]
    public void Killer() => Run(58_646.75, KillerPuzzle.Load());

    [Test]
    public void Fantacy() => Run(
        120_975.45,
        [
            new _2020_04_12(),
            new _2024_11_18(),
            new _2024_12_09(),
            new _2025_03_25(),
            new _2025_08_07(),
            new _2025_09_03(),
            new _2025_11_17(),
            new _2025_12_11(),
            new _2025_12_12(),
            new _2025_12_15(),
            new _2025_12_17(),
        ],
        true);

    static void Run(double avg, IEnumerable<Puzzle> puzzles, bool logPuzzles = false)
    {
        using var _ = Logger.Options();

        var count = 0;

        foreach (var puzzle in puzzles)
        {
            if (logPuzzles) Console.WriteLine(puzzle);

            TestSolver.Solve(puzzle);
            count++;
        }

        var total = decimal.Round(1m * Iterator.Options.Sum() / count, 2);

        total.Should().Be((decimal)avg);
    }
}
