#pragma warning disable S107 // Methods should not have too many parameters
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


    [TestCaseSource(nameof(Usings))]
    public void Using(Techniques t)
    { 
        var options = ReduceOptions.All;
        var puzzles = Sets[t.Set];
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
        var max = results.Keys.Select(k => k.ToString().Length).Max();
        foreach (var kvp in results)
        {
            var dots = new string('.', max - kvp.Key.ToString().Length);
            Console.WriteLine($"{kvp.Key} /*{dots}.*/ = {kvp.Value:00_00},");
        }

        wrong.Should().Be(0);
        solutions.Should().Be(t.Solved);
        results.Should().BeEquivalentTo(new Dictionary<StrategyType, int>()
        {
            [HiddenSingles] = t.HiddenSingles,
            [PointingDigits] = t.PointingDigits,
            [HiddenPairs] = t.HiddenPairs,
            [NakedPairs] = t.NakedPairs,
            [XWing] = t.XWing,
            [HiddenTriples] = t.HiddenTriples,
            [NakedTriples] = t.NakedTriples,
            [Skyscraper] = t.Skyscraper,
            [TwoStringKite] = t.TwoStringKite,
            [Crane] = t.Crane,
            [XYWing] = t.XYWing,
            [Swordfish] = t.Swordfish,
            [WWing] = t.WWing,
            [HiddenQuads] = t.HiddenQuads,
            [NakedQuads] = t.NakedQuads,
            [Jellyfish] = t.Jellyfish,
        });
    }

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


    private const int _all_ = Take;

    private static readonly Techniques[] Usings =
    [
        new() { Set = nameof(Easys), Solved = _all_, HiddenSingles = 42_76 },
        new() { Set = nameof(Mediums), Solved = _all_, HiddenSingles = 85_18, PointingDigits = 27_93, HiddenPairs = 04_64 },
        new() 
        {
            Set = nameof(Hards),
            Solved /*.........*/ = 85_53,
            HiddenSingles /*..*/ = 99_90,
            PointingDigits /*.*/ = 89_40,
            HiddenPairs /*....*/ = 48_84,
            NakedPairs /*.....*/ = 14_65,
            XWing /*..........*/ = 17_47,
            HiddenTriples /*..*/ = 05_67,
            NakedTriples /*...*/ = 00_86,
            Skyscraper /*.....*/ = 17_74,
            TwoStringKite /*..*/ = 17_51,
            Crane /*..........*/ = 02_45,
            XYWing /*.........*/ = 15_66,
            Swordfish /*......*/ = 01_43,
            WWing /*..........*/ = 09_94,
            HiddenQuads /*....*/ = 00_05,
            NakedQuads /*.....*/ = 00_00,
            Jellyfish /*......*/ = 00_01,
        },
        new()
        {
            Set = nameof(Diabolicals),
            Solved /*.........*/ = 13_44,
            HiddenSingles /*..*/ = 99_54,
            PointingDigits /*.*/ = 91_27,
            HiddenPairs /*....*/ = 47_18,
            NakedPairs /*.....*/ = 14_93,
            XWing /*..........*/ = 15_44,
            HiddenTriples /*..*/ = 07_65,
            NakedTriples /*...*/ = 01_38,
            Skyscraper /*.....*/ = 16_82,
            TwoStringKite /*..*/ = 30_74,
            Crane /*..........*/ = 06_56,
            XYWing /*.........*/ = 16_30,
            Swordfish /*......*/ = 02_61,
            WWing /*..........*/ = 28_75,
            HiddenQuads /*....*/ = 00_58,
            NakedQuads /*.....*/ = 00_03,
            Jellyfish /*......*/ = 00_25,
        },
    ];

    public sealed record Techniques
    {
        public required string Set { get; init; }
        public required int Solved { get; init; }
        public int HiddenSingles { get; init; }
        public int PointingDigits { get; init; }
        public int HiddenPairs { get; init; }
        public int NakedPairs { get; init; }
        public int XWing { get; init; }
        public int HiddenTriples { get; init; }
        public int NakedTriples { get; init; }
        public int Skyscraper { get; init; }
        public int TwoStringKite { get; init; }
        public int Crane { get; init; }
        public int XYWing { get; init; }
        public int Swordfish { get; init; }
        public int WWing { get; init; }
        public int HiddenQuads { get; init; }
        public int NakedQuads { get; init; }
        public int Jellyfish { get; init; }
    }
}
