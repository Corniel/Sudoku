using Puzzles;
using Puzzles.PuzzleBank;

namespace Specs.Reduce_specs;

public class Reduce_specs
{
    [Explicit]
    public class Solves
    {
        [TestCase("clues"/*...............*/, _none, _none, 14_80, 57_24)]
        [TestCase("hidden singles"/*......*/, _none, _none, 71_23, _all_)]
        [TestCase("naked pairs"/*.........*/, _none, 16_53, 90_72, _all_)]
        [TestCase("hidden pairs"/*........*/, _none, 14_66, 92_65, _all_)]
        [TestCase("naked triples"/*.......*/, _none, 29_53, 95_65, _all_)]
        [TestCase("hidden triples"/*......*/, _none, 29_43, 95_53, _all_)]
        [TestCase("naked quads"/*.........*/, 00_05, 30_14, 95_71, _all_)]
        [TestCase("hidden quads"/*........*/, 00_05, 30_13, 95_71, _all_)]
        [TestCase("pointing candidates"/*.*/, _none, 41_39, _all_, _all_)]
        [TestCase("X-Wing"/*..............*/, 00_13, 50_88, _all_, _all_)]
        [TestCase("Swordfish"/*...........*/, 00_13, 50_90, _all_, _all_)]
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
            [nameof(Diabolicals)] = Diabolicals.Count(p => Solver.Solve(p.Clues, p.Constraints, options).IsSolved),
            [nameof(Hards)] = Hards.Count(p => Solver.Solve(p.Clues, p.Constraints, options).IsSolved),
            [nameof(Mediums)] = Mediums.Count(p => Solver.Solve(p.Clues, p.Constraints, options).IsSolved),
            [nameof(Easys)] = Easys.Count(p => Solver.Solve(p.Clues, p.Constraints, options).IsSolved),
        };

        private static readonly Dictionary<string, ReduceOptions> Options = new()
        {
            ["clues"/*...............*/] = new(),
            ["hidden singles"/*......*/] = new() { HiddenSingles = true },
            ["naked pairs"/*.........*/] = new() { HiddenSingles = true, NakedPairs = true },
            ["hidden pairs"/*........*/] = new() { HiddenSingles = true, HiddenPairs = true },
            ["naked triples"/*.......*/] = new() { HiddenSingles = true, NakedPairs = true, HiddenPairs = true, NakedTriples = true },
            ["hidden triples"/*......*/] = new() { HiddenSingles = true, NakedPairs = true, HiddenPairs = true, HiddenTriples = true },
            ["naked quads"/*.........*/] = new() { HiddenSingles = true, NakedPairs = true, HiddenPairs = true, NakedTriples = true, HiddenTriples = true, NakedQuads = true },
            ["hidden quads"/*........*/] = new() { HiddenSingles = true, NakedPairs = true, HiddenPairs = true, NakedTriples = true, HiddenTriples = true, HiddenQuads = true },
            ["pointing candidates"/*.*/] = new() { HiddenSingles = true, NakedPairs = true, HiddenPairs = true, PointingCandidates = true },
            ["X-Wing"/*..............*/] = new() { HiddenSingles = true, NakedPairs = true, HiddenPairs = true, PointingCandidates = true, NakedTriples = true, HiddenTriples = true, NakedQuads = true, HiddenQuads = true, XWing = true },
            ["Swordfish"/*...........*/] = new() { HiddenSingles = true, NakedPairs = true, HiddenPairs = true, PointingCandidates = true, NakedTriples = true, HiddenTriples = true, NakedQuads = true, HiddenQuads = true, XWing = true, Swordfish = true },
        };

        private const int _none = 0;
        private const int _all_ = Take;
        private const int Take = 10_000;

        private static readonly ImmutableArray<Puzzle> Easys = [.. PuzzleBankPuzzle.Easy.Take(Take)];

        private static readonly ImmutableArray<Puzzle> Mediums = [.. PuzzleBankPuzzle.Medium.Take(Take)];

        private static readonly ImmutableArray<Puzzle> Hards = [.. PuzzleBankPuzzle.Hard.Take(Take)];

        private static readonly ImmutableArray<Puzzle> Diabolicals = [.. PuzzleBankPuzzle.Diabolical.Take(Take)];
    }
}
