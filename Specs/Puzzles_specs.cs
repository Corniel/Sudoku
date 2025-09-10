using Puzzles;
using Puzzles.CrackingTheCryptic;
using Puzzles.Killer;
using Puzzles.PuzzleBank;
using Puzzles.SudokuPad;
using SudokuSolver.Solvers;
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

public class Killers
{
    private static readonly ImmutableArray<Puzzle> Puzzles = [..KillerPuzzle.Load()];

    [TestCaseSource(nameof(Puzzles))]
    public void Puzzle(Puzzle puzzle)
    {
        var solved = DynamicSolver.Solve(puzzle.Clues, puzzle.Constraints);

        solved.ToString().Should().NotContain(".");

        Console.WriteLine(solved);
    }
}

public class SudokuPad_app
{
    private static readonly ImmutableArray<Puzzle> Puzzles = SudokuPadPuzzle.All;

    [TestCaseSource(nameof(Puzzles))]
    public void Puzzle(Puzzle puzzle)
    {
        var solved = DynamicSolver.Solve(puzzle.Clues, puzzle.Constraints);

        solved.Should().Be(puzzle.Solution);

        Console.WriteLine(solved);
    }
}

public class X_Sudoku
{
    [Test]
    public void Solves() => DynamicSolver.Solve(
            Clues.Parse("""
            .1.|2.3|.4.
            8..|...|6.5
            .7.|...|...
            ---+---+---
            4..|...|..6
            ...|...|...
            2..|...|..7
            ---+---+---
            ...|...|.9.
            7.9|...|..8
            .2.|3.4|.5.
            """),
            Rules.XSudoku)
           .Should().Be("""
            516|273|849
            832|941|675
            974|658|123
            ---+---+---
            485|712|936
            397|486|512
            261|539|487
            ---+---+---
            153|867|294
            749|125|368
            628|394|751
        """);
}

public class Puzzle_bank
{
    private static readonly ImmutableArray<Puzzle> Easys = [.. PuzzleBankPuzzle.Easy.Take(100)];

    private static readonly ImmutableArray<Puzzle> Mediums = [.. PuzzleBankPuzzle.Medium.Take(100)];

    private static readonly ImmutableArray<Puzzle> Hards = [.. PuzzleBankPuzzle.Hard.Take(100)];

    private static readonly ImmutableArray<Puzzle> Diabolicals = [.. PuzzleBankPuzzle.Diabolical.Take(100)];

    private static readonly ImmutableArray<Puzzle> Xs =
    [
        .. PuzzleBankPuzzle.Diabolical.Where(p => p.IsX).Take(25),
        .. PuzzleBankPuzzle.Hard.Where(p => p.IsX).Take(25),
        .. PuzzleBankPuzzle.Medium.Where(p => p.IsX).Take(25),
        .. PuzzleBankPuzzle.Easy.Where(p => p.IsX).Take(25),
    ];

    [TestCaseSource(nameof(Easys))]
    public void Easy(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Mediums))]
    public void Medium(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Hards))]
    public void Hard(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Diabolicals))]
    public void Diabolical(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Xs))]
    public void XSudoku(Puzzle puzzle) => Solve(puzzle, Rules.XSudoku);

    private static void Solve(Puzzle puzzle, ImmutableArray<Constraint>? rules = null)
    {
        var cs = rules ?? Rules.Standard;
        var solved = DynamicSolver.Solve(puzzle.Clues, cs);
        solved.Should().BeSolved();
    }
}

public class Other
{
    [Test]
    public void Wikipedia_example()
    {
        var clues = Clues.Parse("""
            53.|.7.|...
            6..|195|...
            .98|...|.6.
            ---+---+---
            8..|.6.|..3
            4..|8.3|..1
            7..|.2.|..6
            ---+---|---
            .6.|...|28.
            ...|419|..5
            ...|.8.|.79
            """);

        DynamicSolver.Solve(clues).Should().Be("""
            534|678|912
            672|195|348
            198|342|567
            ---+---+---
            859|761|423
            426|853|791
            713|924|856
            ---+---+---
            961|537|284
            287|419|635
            345|286|179
            """);
    }

    [Test]
    public void worlds_ardest_Studoku_according_The_Telegraph()
    {
        var clues = Clues.Parse("""
            8..|...|...
            ..3|6..|...
            .7.|.9.|2..
            ---+---+---
            .5.|..7|...
            ...|.45|7..
            ...|1..|.3.
            ---+---+---
            ..1|...|.68
            ..8|5..|.1.
            .9.|...|4..
            """);

        DynamicSolver.Solve(clues).Should().Be("""
            812|753|649
            943|682|175
            675|491|283
            ---+---+---
            154|237|896
            369|845|721
            287|169|534
            ---+---+---
            521|974|368
            438|526|917
            796|318|452
            """);
    }
}
