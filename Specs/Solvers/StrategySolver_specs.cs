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

    //                                   solv    h1     pd     h2     n2     xwing   h3    n3    sky     kite  sfish   h4      n4   jfish
    [TestCase(nameof(Easys), /*.......*/ _all_, 42_76, _none, _none, _none, _none, _none, _none, _none, _none, _none, _none, _none, _none)]
    [TestCase(nameof(Mediums), /*.....*/ _all_, 85_18, 27_93, 04_64, _none, _none, _none, _none, _none, _none, _none, _none, _none, _none)]
    [TestCase(nameof(Hards), /*.......*/ 67_31, 99_90, 89_11, 48_26, 14_12, 16_74, 05_55, 00_80, 16_80, 16_92, 01_75, 00_09, _none, 00_06)]
    [TestCase(nameof(Diabolicals), /*.*/ 00_26, 99_52, 90_58, 45_70, 13_91, 14_23, 07_26, 01_29, 15_49, 28_95, 02_43, 00_64, 00_03, 00_36)]
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
        int kite,
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
            [TwoStringKite] = kite,
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
