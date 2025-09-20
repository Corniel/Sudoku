using SudokuSolver.Common;

namespace Specs.Restrictions.Dutch_wisper_specs;

public class Solves
{
    [Test]
    public void Parsed()
    {
        var clues = Clues.Parse("""
            ..5|.6.|7..
            ...|...|...
            ...|.3.|4.5
            ---+---+---
            ...|...|...
            ...|...|...
            ...|...|...
            ---+---+---
            2.3|.4.|...
            ...|...|...
            ..6|.7.|8..
            """);

        Rules rules =
            Rules.Standard
            + DutchWhispers.Parse("""
            ABC|DEF|GHI
            RQP|ONM|LKJ
            STU|VWX|YZa
            ---+---+---
            ...|...|...
            ...|...|...
            ...|...|...
            ---+---+---
            ...|...|...
            ...|...|...
            ...|...|...
            """)
            + DutchWhispers.Parse("""
            ...|...|...
            ...|...|...
            ...|...|...
            ---+---+---
            IHG|FED|CBA
            JKL|MNO|PQR
            aZY|XWV|UTS
            ---+---+---
            ...|...|...
            ...|...|...
            ...|...|...
            """)
            + DutchWhispers.Parse("""
            ...|...|...
            ...|...|...
            ...|...|...
            ---+---+---
            ...|...|...
            ...|...|...
            ...|...|...
            ---+---+---
            IHG|FED|CBA
            JKL|MNO|PQR
            aZY|XWV|UTS
            """);

        var solved = Solver.Solve(clues, rules);

        solved.Should().Be("""
            495|162|738
            738|495|162
            162|738|495
            ---+---+---
            951|627|384
            384|951|627
            627|384|951
            ---+---+---
            273|849|516
            849|516|273
            516|273|849
            """, rules);
    }
}

public class Neighbors
{
    [Test]
    public void Generate()
    {
        var lookup = new Candidates[10][];
        lookup[0] = [];

        for (var val = 1; val <= 9; val++)
        {

            var candidates = new Candidates[4];
            candidates[0] |= val;

            for (var i = 1; i < 4; i++)
            {
                foreach (var v in candidates[i - 1])
                {
                    candidates[i] |= Allowed[v];
                }
            }
            lookup[val] = candidates;
        }

        Console.WriteLine("private static readonly ImmutableArray<ImmutableArray<Candidates>> Allowed =");
        Console.WriteLine("[");
        for (var skip = 1; skip < 4; skip++)
        {
            Console.WriteLine($"    [ // Skip {skip - 1}");
            Console.WriteLine("        /* ? */ [1,2,3,4,5,6,7,8,9],");
            for (var v = 1; v <= 9; v++)
            {
                Console.WriteLine($"        /* {v} */ {lookup[v][skip]},");
            }
            Console.WriteLine("    ],");
        }
        Console.WriteLine("]");
    }

    private static readonly ImmutableArray<Candidates> Allowed =
    [
        /* ? */ [1, 2, 3, 4, 6, 7, 8, 9],
        /* 1 */ [5, 6, 7, 8, 9],
        /* 2 */ [6, 7, 8, 9],
        /* 3 */ [7, 8, 9],
        /* 4 */ [8, 9],
        /* 5 */ [1, 9],
        /* 6 */ [1, 2],
        /* 7 */ [1, 2, 3],
        /* 8 */ [1, 2, 3, 4],
        /* 9 */ [1, 2, 3, 4, 5],
    ];
}
