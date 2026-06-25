using DynamicSolver;
using MathNet.Numerics;
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
    public void standard_Easy() => Run(61.68, PuzzleBankPuzzle.Easy.Take(5_000));

    [Test]
    public void standard_Medium() => Run(95.20, PuzzleBankPuzzle.Medium.Take(5_000));

    [Test]
    public void Standard_Hard() => Run(122.97, PuzzleBankPuzzle.Hard.Take(5_000));
    
    [Test]
    public void Standard_Diabolical() => Run(149.62, PuzzleBankPuzzle.Diabolical.Take(5_000));

    [Test]
    public void Standard_Hardest() => Run(
        335.58,
        [
            .. PuzzleBankPuzzle.Diabolical.OrderByDescending(p => p.Level).Take(1_000),
            .. CtcPuzzle.Classics
        ]);

    [Test]
    public void Killer() => Run(5_017.85, KillerPuzzle.Load());

    [Test]
    public void Fantacy() => Run(
        41_645.18,
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

        var total = 0.0;

        var prev = Iterator.Options.ToArray();


        foreach (var puzzle in puzzles)
        {
            if (logPuzzles) Console.WriteLine(puzzle);

            TestSolver.Solve(puzzle);
            count++;
            total += Math.Log10(range(10).Select(i => Iterator.Options[i] - prev[i]).Sum());
            prev = Iterator.Options.ToArray();
        }

        total = double.Round(Math.Pow(10, total / count), 2);

        ((decimal)total).Should().Be((decimal)avg);
    }
}
