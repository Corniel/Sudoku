namespace Specs.Cells_specs;

public class Parses
{
    [Test]
    public void Puzzles()
    {
        var cells = Cells.Parse(@"
            6..|..4|..3
            ..5|7.6|.1.
            .1.|...|7..
            ---+---+---
            .98|32.|6..
            75.|8..|.21
            ..4|1.7|.9.
            ---+---+---
            4..|5..|..7
            .6.|...|1..
            3..|69.|.52");
        
        cells.ToString().Should().HaveLength(131);
    }
}
