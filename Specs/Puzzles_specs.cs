using DynamicSolver;
using Puzzles;
using Puzzles.CrackingTheCryptic;
using Puzzles.Killer;
using Puzzles.NewYorkTimes;
using Puzzles.PuzzleBank;
using Puzzles.SudokuPad;
using Specs.Tools;
using System.IO;
using System.Text;

namespace Specs.Puzzles_specs;

public class Work_in_progress
{
    [Test]
    public void Cracking_the_Cryptic()
    {
        using var _ = Logger.Options();

        var puzzle = new _2025_12_24();

        if (puzzle.Solution.IsSolved)
            puzzle.Constraints.Should().BeValidFor(puzzle.Solution);

        var solver = Solver.Iterate(puzzle.Clues, puzzle.Constraints);
        solver.MoveNext();
        var solved = Cells.New(solver.Current);
        Console.WriteLine(solved);

        if (puzzle.Solution.IsSolved)
            solved.Should().Be(puzzle.Solution, puzzle.Constraints);
        else
            puzzle.Constraints.Should().BeValidFor(solved);

        solver.MoveNext().Should().BeFalse("Solution should be unique");
    }
}

public class Cracking_the_Cryptic
{
    private static readonly ImmutableArray<Puzzle> Unknowns = [.. CtcPuzzle.All.Where(p => p.Duration is O.Unknown)];
    private static readonly ImmutableArray<Puzzle> Fasts = [.. CtcPuzzle.All.Where(p => p.Duration is <= O.ms100 and not O.Unknown)];
    private static readonly ImmutableArray<Puzzle> Slows = [.. CtcPuzzle.All.Where(p => p.Duration is > O.ms100 and not O.oo)];
    private static readonly ImmutableArray<Puzzle> Unsolvables = [.. CtcPuzzle.All.Where(p => p.Duration is O.oo)];

    [TestCaseSource(nameof(Unknowns))]
    public void Unknown(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Fasts))]
    public void Fast(Puzzle puzzle) => Solve(puzzle);

    [Explicit]
    [TestCaseSource(nameof(Slows))]
    public void Slow(Puzzle puzzle) => Solve(puzzle);

    [Explicit]
    [TestCaseSource(nameof(Unsolvables))]
    public void Unsolvable(Puzzle puzzle) => Solve(puzzle);

    private static void Solve(Puzzle puzzle)
    {
        using var _ = Logger.Options();

        puzzle.Constraints.Should().BeValidFor(puzzle.Solution);
        var solved = TestSolver.Solve(puzzle);
        Console.WriteLine(solved);
        solved.Should().Be(puzzle.Solution, puzzle.Constraints);
    }
}

public class Anti_Knight
{
    [Test]
    public void Solves() => TestSolver.Solve(
      Clues.Parse("""
            ...|5..|...
            ...|..4|...
            .58|...|.2.
            ---+---+---
            ...|..9|...
            .6.|...|..5
            ...|1..|3..
            ---+---+---
            ..3|..2|4..
            6..|.78|...
            .9.|...|..1
            """),
          Rules.AntiKnight)
      .Should().Be("""
            926|583|714
            137|624|598
            458|791|623
            ---+---+---
            245|839|167
            361|247|985
            879|165|342
            ---+---+---
            583|912|476
            614|378|259
            792|456|831
            """);
}

public class Hyper_Sudoku
{
    [Test]
    public void Solves() => TestSolver.Solve(
        Clues.Parse("""
            .4.|...|..9
            9..|...|8..
            .1.|3..|...
            ---+---+---
            ...|4.2|..8
            ...|.3.|...
            ...|...|7.5
            ---+---+---
            ...|.9.|...
            .67|..4|...
            ...|..5|4..
            """),
            Rules.Hyper)
        .Should().Be("""
            543|168|279
            926|547|813
            718|329|546
            ---+---+---
            179|452|368
            285|736|194
            634|981|725
            ---+---+---
            451|293|687
            367|814|952
            892|675|431
            """);
}

public class Jigsaw_Sudokud
{
    [Test]
    public void Solves()
    {
        var rules = Rules.Jigsaw("""
            AAA|BBB|BCC
            AAA|BBB|BCC
            AAD|DEB|CCC 
            ---+---+---        
            ADD|DEE|FCC
            DDD|EEE|FFF
            GGD|EEF|FFH
            ---+---+---
            GGG|JEF|FHH
            GGJ|JJJ|HHH
            GGJ|JJJ|HHH
            """);

        TestSolver.Solve(
          Clues.Parse("""
            4..|7.9|.2.
            ...|.2.|...
            .9.|..8|...
            ---+---+---
            1.4|...|3..
            7..|4.1|..2
            ..2|...|1.3
            ---+---+---
            ...|6..|.1.
            ...|.4.|...
            .1.|2..|.45
            """),
            rules)
       .Should().Be("""
            463|719|528
            857|326|491
            296|158|734
            ---+---+---
            184|972|356
            735|491|862
            942|865|173
            ---+---+---
            528|634|917
            671|543|289
            319|287|645
            """,
            rules);
    }
}

public class Killer_Sudoku
{
    private static readonly ImmutableArray<Puzzle> Puzzles = [.. KillerPuzzle.Load()];

    [TestCaseSource(nameof(Puzzles))]
    public void Puzzle(Puzzle puzzle)
    {
        using var _ = Logger.Options();


        var solved = TestSolver.Solve(puzzle);

        solved.ToString().Should().NotContain(".");

        puzzle.Constraints.Should().BeValidFor(solved);

        Console.WriteLine(solved);
    }
}

public class New_York_Times
{
    private static readonly ImmutableArray<Puzzle> Puzzles = [.. NewYorkTimesPuzzle.Hard];

    [Test]
    public void Can_be_loaded()
    {
        Func<ImmutableArray<NewYorkTimesPuzzle>> load = () => NewYorkTimesPuzzle.Hard;
        load.Should().NotThrow();
    }

    [TestCaseSource(nameof(Puzzles))]
    public void Puzzle(Puzzle puzzle)
    {
        puzzle.Constraints.Should().BeValidFor(puzzle.Solution);
        var solved = TestSolver.Solve(puzzle);
        solved.Should().Be(puzzle.Solution, puzzle.Constraints);
    }
}


public class X_Sudoku
{
    [Test]
    public void Solves() => TestSolver.Solve(
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

public class SudokuPad_app
{
    private static readonly ImmutableArray<Puzzle> Puzzles = SudokuPadPuzzle.All;

    [TestCaseSource(nameof(Puzzles))]
    public void Puzzle(Puzzle puzzle)
    {
        var solved = TestSolver.Solve(puzzle);

        solved.Should().Be(puzzle.Solution);

        Console.WriteLine(solved);
    }
}

public class Puzzle_bank
{
    private const int Take = 100;

    private static readonly ImmutableArray<Puzzle> Easys = [.. PuzzleBankPuzzle.Easy.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Mediums = [.. PuzzleBankPuzzle.Medium.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Hards = [.. PuzzleBankPuzzle.Hard.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Diabolicals = [.. PuzzleBankPuzzle.Diabolical.Take(Take)];

    private static readonly ImmutableArray<Puzzle> Hypers =
    [
        .. PuzzleBankPuzzle.Diabolical.Where(p => p.IsHyper),
        .. PuzzleBankPuzzle.Hard.Where(p => p.IsHyper),
        .. PuzzleBankPuzzle.Medium.Where(p => p.IsHyper),
        .. PuzzleBankPuzzle.Easy.Where(p => p.IsHyper),
    ];

    private static readonly ImmutableArray<Puzzle> Xs =
    [
        .. PuzzleBankPuzzle.Diabolical.Where(p => p.IsX),
        .. PuzzleBankPuzzle.Hard.Where(p => p.IsX),
        .. PuzzleBankPuzzle.Medium.Where(p => p.IsX),
        .. PuzzleBankPuzzle.Easy.Where(p => p.IsX),
    ];

    [TestCaseSource(nameof(Easys))]
    public void Easy(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Mediums))]
    public void Medium(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Hards))]
    public void Hard(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Diabolicals))]
    public void Diabolical(Puzzle puzzle) => Solve(puzzle);

    [TestCaseSource(nameof(Hypers))]
    public void Hyper(Puzzle puzzle) => Solve(puzzle, Rules.Hyper);

    [TestCaseSource(nameof(Xs))]
    public void XSudoku(Puzzle puzzle) => Solve(puzzle, Rules.XSudoku);

    [Explicit("Only usefull to extend the file definitions")]
    [TestCase(nameof(Diabolical))]
    [TestCase(nameof(Hard))]
    [TestCase(nameof(Medium))]
    [TestCase(nameof(Easy))]
    public void Process(string name)
    {
        var file = new FileInfo($"../../../../Puzzles/PuzzleBank/{name}.txt");

        file.Exists.Should().BeTrue();

        using var writer = new StreamWriter(file.FullName, false, new UTF8Encoding(false));

        foreach (var puzzle in PuzzleBankPuzzle.Load(name))
        {
            // Update
            // ...
            // Save
            puzzle.WriteTo(writer);
        }
    }

    private static void Solve(Puzzle puzzle, Rules? rules = null)
    {
        using var _ = Logger.Options();

        var solved = TestSolver.Solve(puzzle.Clues, rules ?? Rules.Standard);
        solved.Should().Be(puzzle.Solution);
    }
}

public class Other
{
    [Test]
    public void Wikipedia_example()
    {
        using var _ = Logger.Options();

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

        TestSolver.Solve(clues).Should().Be("""
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
    public void worlds_hardest_Studoku_according_The_Telegraph()
    {
        using var _ = Logger.Options();

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

        TestSolver.Solve(clues).Should().Be("""
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
