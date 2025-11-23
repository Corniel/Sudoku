using Sudoku.Common;

namespace Specs.Restrictions.German_whisper_specs;

public class Parses
{
    [Test]
    public void multiple_at_once()
    {
        var wispers = Rules.Standard + GermanWhispers.Parse("""
            ...|.5.|.E.
            .13|4..|DFG
            I2.|..C|...
            ---+---+---
            .J.|.B.|h78
            .K.|.e.|g.9
            LON|..f|jk.
            ---+---+---
            .P.|...|X.l
            ..Q|.TV|Y.b
            ...|RU.|.ac
            """);

        var clues = Clues.Parse("""
            ..2|.8.|.9.
            ...|...|..8
            ...|...|...
            ---+---+---
            ...|.1.|...
            ..5|..7|...
            ...|...|...
            ---+---+---
            54.|...|...
            ...|...|..2
            ...|...|...
            """);

        var solved = TestSolver.Solve(clues, wispers);

        solved.Should().Be("""
            352│184│796
            479│356│218
            816│279│453
            ───┼───┼───
            634│915│827
            985│427│361
            127│638│945
            ───┼───┼───
            543│862│179
            791│543│682
            268│791│534
            """,
            wispers);
    }
}
