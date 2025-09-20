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
