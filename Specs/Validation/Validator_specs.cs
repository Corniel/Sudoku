using Puzzles;
using Puzzles.PuzzleBank;
using SudokuSolver.Common;
using SudokuSolver.Houses;

namespace Specs.Validation.Validator_specs;

public class Invalidates
{
    [Test]
    public void peer_violations()
    {
        var solution = Cells.Parse("""
            .54|738|261
            261|495|837
            837|162|594
            ---+---+---
            159|384|726
            726|951|483
            483|627|159
            ---+---+---
            948|273|615
            615|849|372
            372|516|948
            """);

        var violation = Rules.Standard.Validate(solution).Single();

        violation.Should().BeEquivalentTo(new
        {
            Cell = new Pos(3, 1),
            Value = 5,
            Constraint = Col.All[1],
        });
    }

    [Test]
    public void restriction_violations()
    {
        var solution = Cells.Parse("""
            594|738|261
            261|495|837
            837|162|594
            ---+---+---
            159|384|726
            726|951|483
            483|627|159
            ---+---+---
            948|273|615
            615|849|372
            372|516|948
            """);

        ImmutableArray<Rule> rules = [new KillerCage(3, [(0, 0), (0, 1)])];

        var violation = rules.Validate(solution).First();

        violation.Should().BeEquivalentTo(new
        {
            Cell = new Pos(0, 0),
            Value = 5,
            Allowed = Candidates.None,
            Constraint = new Pos[] { (0, 0), (0, 1) },
            Restriction = new { Sum = 3 },
        });
    }
}

[Explicit]
public class Solves
{
    [TestCase("backtracking"/*.*/, _all_, _all_, _all_, _all_)]
    [TestCase("nakedsingles"/*.*/, _none, _none, 1_480, 5_724)]
    [TestCase("hidden"/*.......*/, _none, 1_466, 9_265, _all_)]
    [TestCase("nakedpairs"/*...*/, _none, 2_580, 9_511, _all_)]
    [TestCase("nakedtriples"/*.*/, _none, 2_593, 9_512, _all_)]
    [TestCase("nakedquads"/*...*/, _none, 2_599, 9_512, _all_)]
    [TestCase("intersection"/*.*/, _none, 4_149, _all_, _all_)]
    [TestCase("x-wing"/*.......*/, _none, 4_833, _all_, _all_)]
    [TestCase("swordfish"/*....*/, _none, 4_837, _all_, _all_)]
    [TestCase("jellyfish"/*....*/, _none, 4_837, _all_, _all_)]
    public void Using(string options, int diabolical, int hard, int medium, int easy)
    {
        Solve(Options[options])
            .Should().BeEquivalentTo(new Dictionary<string, int>
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
        ["backtracking"/*.*/] = new() { Backtracker = true },
        ["nakedsingles"/*.*/] = new() { NakedSingles = true },
        ["hidden"/*.......*/] = new() { NakedSingles = true, Hidden = true },
        ["nakedpairs"/*...*/] = new() { NakedSingles = true, Hidden = true, NakedPairs = true },
        ["nakedtriples"/*.*/] = new() { NakedSingles = true, Hidden = true, NakedPairs = true, NakedTriples = true },
        ["nakedquads"/*...*/] = new() { NakedSingles = true, Hidden = true, NakedPairs = true, NakedTriples = true, NakedQuads = true },
        ["intersection"/*.*/] = new() { NakedSingles = true, Hidden = true, NakedPairs = true, NakedTriples = true, NakedQuads = true, Intersection = true },
        ["x-wing"/*.......*/] = new() { NakedSingles = true, Hidden = true, NakedPairs = true, NakedTriples = true, NakedQuads = true, Intersection = true, XWing = true },
        ["swordfish"/*....*/] = new() { NakedSingles = true, Hidden = true, NakedPairs = true, NakedTriples = true, NakedQuads = true, Intersection = true, XWing = true, Swordfish = true },
        ["jellyfish"/*....*/] = new() { NakedSingles = true, Hidden = true, NakedPairs = true, NakedTriples = true, NakedQuads = true, Intersection = true, XWing = true, Swordfish = true, Jellyfish = true },
    };

    private const int _none = 0;
    private const int _all_ = Take;
    private const int Take = 10_000;

    private static readonly ImmutableArray<Puzzle> Easys = [.. PuzzleBankPuzzle.Easy.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Mediums = [.. PuzzleBankPuzzle.Medium.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Hards = [.. PuzzleBankPuzzle.Hard.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Diabolicals = [.. PuzzleBankPuzzle.Diabolical.Take(Take)];

}
