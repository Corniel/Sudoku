using Sudoku.Sets;
using Sudoku.Validation;

namespace Specs.Validation.Validator_specs;

public class Invalidates
{
    [Test]
    public void peer_violations()
    {
        var solution = Cells.New("""
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

        var violation = RuleSet.Standard.Validate(solution).Single();

        violation.Should().BeEquivalentTo(new SetViolation([(3, 1)], Houses.Cols[1]));
    }
}


