using Puzzles;
using Puzzles.CrackingTheCryptic;
using Puzzles.PuzzleBank;
using Puzzles.SudokuPad;
using SudokuSolver.Solvers;
using System;
using System.Collections.Immutable;

namespace Specs.Puzzles_specs;

[Explicit]
public class Cracking_the_Cryptic
{
    private static readonly ImmutableArray<Puzzle> Puzzles = CtcPuzzle.All;

    [TestCaseSource(nameof(Puzzles))]
    public void Puzzle(Puzzle puzzle)
    {
        var solved = DynamicSolver.Solve(puzzle.Clues, puzzle.Constraints);

        solved.Should().Be(puzzle.Solution);

        Console.WriteLine(solved);
    }
}

public class SudokuPad_app
{
    private static readonly ImmutableArray<Puzzle> Puzzles = SudokuPadPuzzle.All;

    [TestCaseSource(nameof(Puzzles))]
    public void Puzzle(Puzzle puzzle)
    {
        var solved = DynamicSolver.Solve(puzzle.Clues);

        solved.Should().Be(puzzle.Solution);

        Console.WriteLine(solved);
    }
}

public class Puzzle_bank
{
    private static readonly ImmutableArray<Puzzle> Easys = [.. PuzzleBankPuzzle.Easy.Take(100)];

    private static readonly ImmutableArray<Puzzle> Mediums = [.. PuzzleBankPuzzle.Medium.Take(100)];

    private static readonly ImmutableArray<Puzzle> Hards = [.. PuzzleBankPuzzle.Hard.Take(100)];

    private static readonly ImmutableArray<Puzzle> Diabolicals = [.. PuzzleBankPuzzle.Diabolical.Take(100)];


    [TestCaseSource(nameof(Easys))]
    public void Easy(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Mediums))]
    public void Medium(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Hards))]
    public void Hard(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Diabolicals))]
    public void Diabolical(Puzzle puzzle) => Solve(puzzle);

    private static void Solve(Puzzle puzzle)
    {
        var solved = DynamicSolver.Solve(puzzle.Clues, Rules.Standard);
        solved.Should().BeSolved();
    }
}
