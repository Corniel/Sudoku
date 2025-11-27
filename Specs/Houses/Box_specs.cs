using Sudoku.Houses;

namespace Specs.Houses.Box_specs;

public class IndexOf
{
    [Test]
    public void All_cells_is_in_Box([Range(0, 8)] int index)
        => new Box(index).Should().AllSatisfy(
            pos => Box.IndexOf(pos).Should().Be(index));
}
