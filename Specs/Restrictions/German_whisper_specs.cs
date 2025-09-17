using SudokuSolver.Constraints;

namespace Specs.Restrictions.German_whisper_specs;

public class Parses
{
    [Test]
    public void multiple_at_once()
    {
        var wispers = GermanWhispers.Parse("""
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
            """).ToArray();

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

        var solved = DynamicSolver.Solve(clues, [.. Rules.Standard, .. wispers]);

        solved.Should().Be("""
            352|186|794
            967|254|318
            814|379|526
            ---+---+---
            726|513|849
            485|927|163
            193|648|275
            ---+---+---
            548|762|931
            679|831|452
            231|495|687
            """,
            [.. Rules.Standard, .. wispers]);
    }
}
