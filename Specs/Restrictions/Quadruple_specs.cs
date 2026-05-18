using Sudoku.Common;
using Sudoku.Restrictions;
using Sudoku.Validation;

namespace Specs.Restrictions.Quadruple_specs;

public class Restricts
{
    [Test]
    public void To_none_when_distinct_can_not_be_reached()
    {
        var res = new Quadruple((1, 1), [(0, 0), (0, 1), (1, 0)], [1, 2, 3]);

        var cells = CellsWrapper.Parse("""
            14.│...│...
            5..│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            """);

        res.Restrict(cells).Should().Be(Digits.None);
    }

    [Test]
    public void To_last_remaining_when()
    {
        var res = new Quadruple((1, 1), [(0, 0), (0, 1), (1, 0)], [1, 2, 3]);

        var cells = CellsWrapper.Parse("""
            23.│...│...
            4..│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            """);

        res.Restrict(cells).Should().Be([1]);
    }

    [Test]
    public void Not_when_multiple_remaining()
    {
        var res = new Quadruple((1, 1), [(0, 0), (0, 1), (1, 0)], [1, 2, 3]);

        var cells = CellsWrapper.Parse("""
            .3.│...│...
            4..│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│...
            ...│...│...
            """);

        res.Restrict(cells).Should().Be(_1_to_9);
    }

    [Test]
    public void Multiple()
    {
        var cells = Cells.New("""
            658│392│147
            734│861│259
            219│547│638
            ───┼───┼───
            945│618│372
            863│274│915
            127│935│864
            ───┼───┼───
            496│723│581
            572│186│493
            381│459│726
            """);

        var res = RuleSet.Standard
            + Groups.Cages("""
            AA.│...│.CC
            AA.│...│.CC
            ...│.BB│...
            ───┼───┼───
            ...│.BB│...
            ...│...│...
            ...│bb.│...
            ───┼───┼───
            ...│bb.│...
            aa.│...│.cc
            aa.│...│.cc

            A:a:357
            B:14
            C:c:579
            """);

        res.Validate(cells).Should().HaveCount(4);
    }
}
