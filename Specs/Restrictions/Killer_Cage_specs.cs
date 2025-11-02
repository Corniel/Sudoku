using System.IO;
using System.Text;

namespace Specs.Restrictions.Killer_Cage_specs;

public class Parses
{
    [Test]
    public void cell_based_cages()
    {
        var rules = Rules.Killer("""
            AAB|BBC|DEF
            GGH|HCC|DEF
            GGI|ICJ|KKF
            ---+---+---
            LMM|INJ|KOF
            LPP|QNJ|OOR
            SPT|QNU|VVR
            ---+---+---
            STT|QWU|UXX
            SYZ|WWa|aXX
            SYZ|Wbb|bcc

            A = 3   B = 15  C = 22
            D = 4   E = 16  F = 15
            G = 25  H = 17  I = 9
            J = 8   K = 20  L = 6
            M = 14  N = 17  O = 17
            P = 13  Q = 20  R = 12
            S = 27  T = 6   U = 20
            V = 6   W = 10  X = 14
            Y = 8   Z = 16  a = 15
            b = 13  c = 17
            """);

        var solution = TestSolver.Solve(Clues.None, rules);

        solution.Should().Be("""
            215|647|398
            368|952|174
            794|381|652
            ---+---+---
            586|274|931
            142|593|867
            973|816|425
            ---+---+---
            821|739|546
            659|428|713
            437|165|289
            """);
    }

    [Test, Ignore("Check outcome, it seems that multiple solutions are possible.")]
    public void cages_per_line()
    {
        var rules = Rules.Killer("""
            27=(0,0)+(0,1)+(1,0)+(2,0)
            13=(0,2)+(1,1)+(1,2)+(2,1)
            15=(0,3)+(1,3)+(2,3)+(3,3)+(4,3)
            28=(2,2)+(3,0)+(3,1)+(3,2)
            17=(4,0)+(5,0)+(4,1)+(4,2)
            17=(5,1)+(5,2)+(5,3)+(5,4)
            20=(6,0)+(6,1)+(6,2)+(7,2)
            25=(7,0)+(7,1)+(8,0)+(8,1)+(8,2)
            33=(5,5)+(6,5)+(6,4)+(6,3)+(7,3)
            16=(0,4)+(1,4)+(1,5)+(1,6)
            16=(0,5)+(0,6)+(0,7)+(0,8)
            27=(1,7)+(1,8)+(2,7)+(2,8)
            """);

        var solution = TestSolver.Solve(Clues.None, rules);

        solution.Should().Be("""
            892|463|571
            364|571|289
            715|298|364
            ---+---+---
            689|324|157
            573|186|492
            241|759|638
            ---+---+---
            428|637|915
            956|812|743
            137|945|826
            """);
    }
}

public class Generates
{
    [TestCase(1 + 2, /*.................................................*/ 8 + 9, 2)]
    [TestCase(1 + 2 + 3, /*.........................................*/ 7 + 8 + 9, 3)]
    [TestCase(1 + 2 + 3 + 4, /*.................................*/ 6 + 7 + 8 + 9, 4)]
    [TestCase(1 + 2 + 3 + 4 + 5, /*.........................*/ 5 + 6 + 7 + 8 + 9, 5)]
    [TestCase(1 + 2 + 3 + 4 + 5 + 6, /*.................*/ 4 + 5 + 6 + 7 + 8 + 9, 6)]
    [TestCase(1 + 2 + 3 + 4 + 5 + 6 + 7, /*.........*/ 3 + 4 + 5 + 6 + 7 + 8 + 9, 7)]
    [TestCase(1 + 2 + 3 + 4 + 5 + 6 + 7 + 8, /*.*/ 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9, 8)]
    public void lookup_for_sum(int min, int max, int bits)
    {
        var lookups = Sudoku.Restrictions.Cage.Lookup[bits];

        lookups[..(min - 1)].Should().AllSatisfy(l => l.Should().BeNull());
        lookups[min..].Should().AllSatisfy(l => l.Should().NotBeNull());
        lookups.Should().HaveCount(max + 1);
    }

    [Explicit]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    public void lookup(int cells)
    {
        var file = new FileInfo($"./../../../../Sudoku/Restrictions/Cage_{cells}.md");

        using var writer = new StreamWriter(file.FullName, false, new UTF8Encoding(false));

        Console.WriteLine(file.FullName);

        for (var sum = Min(cells); sum <= Max(cells); sum++)
        {
            writer.Write($"## {sum}\n");

            foreach (var known in Digits.All.Where(c => c.Count < cells).OrderByDescending(c => c.Count))
            {
                var missing = sum - known.Sum();
                var unknown = cells - known.Count;
                var digits = Digits.None;

                // check for bitcount, sum, and not overlapping already used digits
                foreach (var option in Digits.All
                    .Where(c
                        => c.Count == unknown
                        && c.Sum() == missing
                        && ((known & c) == Digits.None)))
                {
                    digits |= option;
                }

                digits ^= known;

                if (digits.HasAny)
                {
                    writer.Write($"{known}={digits}\n".Replace(",", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty));
                }
            }
        }

        writer.Flush();
        file.Refresh();
        file.Exists.Should().BeTrue();
    }

    static int Min(int unknown) => Enumerable.Range(1, unknown).Sum();

    static int Max(int unknown) => Enumerable.Range(0, unknown).Sum(i => 9 - i);
}
