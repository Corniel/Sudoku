using Puzzles;
using Puzzles.NewYorkTimes;
using Puzzles.PuzzleBank;
using StrategyBased;
using Sudoku.Contracts;
using static StrategyBased.StrategyType;

namespace Specs.Solvers.StrategySolver_specs;

[Explicit]
public class Solves
{
    [Test]
    public void Communicating_used_stagies()
    {
        var puzzle = NewYorkTimesPuzzle.Hard[0];
        var solver = new StrategyBasedSolver(puzzle.Clues, puzzle.Constraints, ReduceOptions.All);
        var steps = solver.Select(r => new Step(r.Type, r.Cells)).ToList();
#if DEBUG
        foreach (var step in steps)
        {
            Console.WriteLine($"{step.Type}\n{step.Cells}\n");
        }
#endif
        steps.Should().BeEquivalentTo(
        [
            new { Cells = new { Solved = 30 }, Type = HiddenSingles },
            new { Cells = new { Solved = 36 }, Type = PointingDigits },
            new { Cells = new { Solved = 51 }, Type = HiddenSingles },
            new { Cells = new { Solved = 81 }, Type = HiddenSingles },
        ]);
    }

    private sealed record Step(StrategyType Type, Cells Cells);

    [Test]
    public void Without_inconsitancies() => PuzzleBankPuzzle.Diabolical.Take(Take).Should().AllSatisfy(puzzle =>
    {
        var solver = new StrategyBasedSolver(puzzle.Clues, puzzle.Constraints, ReduceOptions.All);
        _ = solver.LastOrDefault();
        solver.Nodes.HasIncosistency.Should().BeFalse(because: puzzle.ToString());

        if (solver.Nodes.IsSolved)
        {
            Cells.New(solver.Nodes).Should().Be(puzzle.Solution);
        }
    });

    [TestCase("clues"/*...........*/, _none, _none, 14_80, 57_24)]
    [TestCase("hidden singles"/*..*/, _none, _none, 71_23, _all_)]
    [TestCase("naked pairs"/*.....*/, _none, 16_53, 90_72, _all_)]
    [TestCase("hidden pairs"/*....*/, _none, 14_66, 92_65, _all_)]
    [TestCase("naked triples"/*...*/, _none, 29_53, 95_65, _all_)]
    [TestCase("hidden triples"/*..*/, _none, 29_43, 95_53, _all_)]
    [TestCase("naked quads"/*.....*/, 00_05, 30_14, 95_71, _all_)]
    [TestCase("hidden quads"/*....*/, 00_05, 30_13, 95_71, _all_)]
    [TestCase("pointing digits"/*.*/, _none, 41_39, _all_, _all_)]
    [TestCase("X-Wing"/*..........*/, 00_13, 50_88, _all_, _all_)]
    [TestCase("Swordfish"/*.......*/, 00_13, 50_90, _all_, _all_)]
    public void Using(string options, int diabolical, int hard, int medium, int easy)
    {
        var solved = Solve(Options[options]);
        solved.Should().BeEquivalentTo(new Dictionary<string, int>
        {
            [nameof(Diabolicals)] = diabolical,
            [nameof(Hards)] = hard,
            [nameof(Mediums)] = medium,
            [nameof(Easys)] = easy,
        });
    }

    private static Dictionary<string, int> Solve(ReduceOptions options) => new()
    {
        [nameof(Diabolicals)] = Diabolicals.Count(p => StrategyBasedSolver.Solve(p.Clues, p.Constraints, options).IsSolved),
        [nameof(Hards)] = Hards.Count(p => StrategyBasedSolver.Solve(p.Clues, p.Constraints, options).IsSolved),
        [nameof(Mediums)] = Mediums.Count(p => StrategyBasedSolver.Solve(p.Clues, p.Constraints, options).IsSolved),
        [nameof(Easys)] = Easys.Count(p => StrategyBasedSolver.Solve(p.Clues, p.Constraints, options).IsSolved),
    };

    private static readonly Dictionary<string, ReduceOptions> Options = new()
    {
        ["clues"/*...........*/] = new(),
        ["hidden singles"/*..*/] = new(HiddenSingles),
        ["naked pairs"/*.....*/] = new(HiddenSingles, NakedPairs),
        ["hidden pairs"/*....*/] = new(HiddenSingles, HiddenPairs),
        ["naked triples"/*...*/] = new(HiddenSingles, NakedPairs, HiddenPairs, NakedTriples),
        ["hidden triples"/*..*/] = new(HiddenSingles, NakedPairs, HiddenPairs, HiddenTriples),
        ["naked quads"/*.....*/] = new(HiddenSingles, NakedPairs, HiddenPairs, NakedTriples, HiddenTriples, NakedQuads),
        ["hidden quads"/*....*/] = new(HiddenSingles, NakedPairs, HiddenPairs, NakedTriples, HiddenTriples, HiddenQuads),
        ["pointing digits"/*.*/] = new(HiddenSingles, NakedPairs, HiddenPairs, PointingDigits),
        ["X-Wing"/*..........*/] = new(HiddenSingles, NakedPairs, HiddenPairs, PointingDigits, NakedTriples, HiddenTriples, NakedQuads, HiddenQuads, XWing),
        ["Swordfish"/*.......*/] = new(HiddenSingles, NakedPairs, HiddenPairs, PointingDigits, NakedTriples, HiddenTriples, NakedQuads, HiddenQuads, XWing, Swordfish),
    };

    private const int _none = 0;
    private const int _all_ = Take;
    private const int Take = 10_000;

    private static readonly ImmutableArray<Puzzle> Easys = [.. PuzzleBankPuzzle.Easy.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Mediums = [.. PuzzleBankPuzzle.Medium.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Hards = [.. PuzzleBankPuzzle.Hard.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Diabolicals = [.. PuzzleBankPuzzle.Diabolical.Take(Take)];
}
