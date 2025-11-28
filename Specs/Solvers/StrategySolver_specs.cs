using Puzzles;
using Puzzles.NewYorkTimes;
using Puzzles.PuzzleBank;
using StrategyBased;
using Sudoku.Contracts;
using System.Collections.Frozen;
using static StrategyBased.StrategyType;

namespace Specs.Solvers.StrategySolver_specs;

[Explicit]
public class Solves
{
    [Test]
    public void Communicating_used_stagies()
    {
        var puzzle = new NewYorkTimesPuzzle(
            new(2019, 01, 31),
            Clues.Parse(".2......5..4.7...1....3.....7..2.9..4.....3.....6....8.56....1....3..7.29..8....."),
            Cells.Parse("729481635364579281185236479678123954412958367593647128256794813841365792937812546"));

        var solver = new StrategyBasedSolver(puzzle.Clues, puzzle.Constraints, ReduceOptions.All);
        var steps = solver.Select(r => new Step(r.Type, r.Cells)).ToList();
#if DEBUG
        foreach (var step in steps)
        {
            Console.WriteLine($$"""new { Cells = new { Solved = {{step.Cells.Solved}} }, Type = {{step.Type}} },""");
        }
#endif
        steps.Should().BeEquivalentTo(
        [
            new { Cells = new { Solved = 23 }, Type = HiddenSingles },
            new { Cells = new { Solved = 23 }, Type = PointingDigits },
            new { Cells = new { Solved = 23 }, Type = HiddenPairs },
            new { Cells = new { Solved = 47 }, Type = HiddenSingles },
            new { Cells = new { Solved = 47 }, Type = PointingDigits },
            new { Cells = new { Solved = 47 }, Type = HiddenPairs },
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

    //                                   solv    h1     pd     h2     n2     xwing   h3    n3    sky    sfish   h4      n4   jfish
    [TestCase(nameof(Easys), /*.......*/ _all_, 42_76, _none, _none, _none, _none, _none, _none, _none, _none, _none, _none, _none)]
    [TestCase(nameof(Mediums), /*.....*/ _all_, 85_18, 27_93, 04_64, _none, _none, _none, _none, _none, _none, _none, _none, _none)]
    [TestCase(nameof(Hards), /*.......*/ 59_57, 99_90, 88_67, 47_54, 13_42, 15_82, 05_41, 00_79, 15_68, 02_06, 00_08, _none, 00_05)]
    [TestCase(nameof(Diabolicals), /*.*/ 00_21, 99_51, 90_16, 45_25, 13_49, 13_62, 06_95, 01_22, 15_19, 02_39, 00_63, 00_03, 00_35)]
    public void Using(
        string collection,
        int solved,
        int hiddenSingles,
        int pointingDigits,
        int hiddenPairs,
        int nakedPairs,
        int xwing,
        int hiddenTriples,
        int nakedTriples,
        int skyscraper,
        int swordfish,
        int hiddenQuads,
        int nakedQuads,
        int jellyfish)
    {
        var options = new ReduceOptions
        (
            HiddenSingles,
            PointingDigits,
            HiddenPairs,
            NakedPairs,
            XWing,
            HiddenTriples,
            NakedTriples,
            Skyscraper,
            TwoStringKite,
            Swordfish,
            HiddenQuads,
            NakedQuads,
            Jellyfish
        );

        var puzzles = Sets[collection];
        var results = options.Strategies.ToDictionary(s => s.Type, _ => 0);

        var solutions = 0;
        var wrong = 0;
        foreach (var puzzle in puzzles)
        {
            var solver = new StrategyBasedSolver(Nodes.Empty & puzzle.Constraints & puzzle.Clues, options);

            foreach (var type in solver.Select(r => r.Type).Distinct())
                results[type]++;

            if (solver.Nodes.IsSolved)
            {
                wrong += puzzle.Solution == Cells.New(solver.Nodes) ? 0 : 1;
                solutions++;
            }
        }
       
        Console.WriteLine($"Solved: {solutions:00_00}");
        foreach(var kvp in  results)
        {
            Console.WriteLine($"{kvp.Key,-20}: {kvp.Value:00_00}");
        }

        wrong.Should().Be(0);
        solutions.Should().Be(solved);
        results.Should().BeEquivalentTo(new Dictionary<StrategyType, int>()
        {
            [HiddenSingles] = hiddenSingles,
            [PointingDigits] = pointingDigits,
            [HiddenPairs] = hiddenPairs,
            [NakedPairs] = nakedPairs,
            [XWing] = xwing,
            [HiddenTriples] = hiddenTriples,
            [NakedTriples] = nakedTriples,
            [Skyscraper] = skyscraper,
            [TwoStringKite] = 0,
            [Swordfish] = swordfish,
            [HiddenQuads] = hiddenQuads,
            [NakedQuads] = nakedQuads,
            [Jellyfish] = jellyfish,
        });
    }

    private const int _none = 0;
    private const int _all_ = Take;
    private const int Take = 10_000;

    private static readonly ImmutableArray<Puzzle> Easys = [.. PuzzleBankPuzzle.Easy.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Mediums = [.. PuzzleBankPuzzle.Medium.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Hards = [.. PuzzleBankPuzzle.Hard.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Diabolicals = [.. PuzzleBankPuzzle.Diabolical.Take(Take)];

    public static readonly FrozenDictionary<string, ImmutableArray<Puzzle>> Sets = new Dictionary<string, ImmutableArray<Puzzle>>()
    {
        [nameof(Easys)] = Easys,
        [nameof(Mediums)] = Mediums,
        [nameof(Hards)] = Hards,
        [nameof(Diabolicals)] = Diabolicals,
    }
    .ToFrozenDictionary();
}
