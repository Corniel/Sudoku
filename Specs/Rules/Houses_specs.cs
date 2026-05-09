using Sudoku.Sets;

namespace Specs.Houses_specs;

public class Rows
{
    [Test]
    public void unique_for([Range(0, 8)] int index)
        => Houses.Rows[index].Should().HaveCount(9);

    [Test]
    public void all_unique()
        => Houses.Rows.SelectMany(x => x).Should().BeEquivalentTo(Pos.All);
}

public class Cols
{
    [Test]
    public void unique_for([Range(0, 8)] int index)
        => Houses.Cols[index].Should().HaveCount(9);

    [Test]
    public void all_unique()
        => Houses.Cols.SelectMany(x => x).Should().BeEquivalentTo(Pos.All);
}

public class Boxes
{
    [Test]
    public void unique_for([Range(0, 8)] int index)
        => Houses.Boxes[index].Should().HaveCount(9);

    [Test]
    public void all_unique()
        => Houses.Boxes.SelectMany(x => x).Should().BeEquivalentTo(Pos.All);

    public class IndexOf
    {
        [Test]
        public void All_cells_is_in_Box([Range(0, 8)] int index)
            => Houses.Boxes[index].Should().AllSatisfy(
                pos => Box.IndexOf(pos).Should().Be(index));
    }

}

public class Diagonals
{
    [Test]
    public void NW_SE()
        => Diagonal.NW_SE.Should().BeEquivalentTo(
        [
            new Pos(0, 0),
            (1, 1),
            (2, 2),
            (3, 3),
            (4, 4),
            (5, 5),
            (6, 6),
            (7, 7),
            (8, 8),
        ]);

    [Test]
    public void NE_SW()
        => Diagonal.NE_SW.Should().BeEquivalentTo(
        [
            new Pos(0, 8),
            (1, 7),
            (2, 6),
            (3, 5),
            (4, 4),
            (5, 3),
            (6, 2),
            (7, 1),
            (8, 0),
        ]);
}
