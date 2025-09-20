using SudokuSolver.Houses;

namespace Specs.Houses_specs;

public class Rows
{
    [Test]
    public void unique_for([Range(0, 8)] int index)
        => Row.All[index].Should().HaveCount(9);

    [Test]
    public void all_unique()
        => Row.All.SelectMany(x => x).Should().BeEquivalentTo(Pos.All);
}

public class Cols
{
    [Test]
    public void unique_for([Range(0, 8)] int index)
        => Col.All[index].Should().HaveCount(9);

    [Test]
    public void all_unique()
        => Col.All.SelectMany(x => x).Should().BeEquivalentTo(Pos.All);
}

public class Boxes
{
    [Test]
    public void unique_for([Range(0, 8)] int index)
        => Box.All[index].Should().HaveCount(9);

    [Test]
    public void all_unique()
        => Box.All.SelectMany(x => x).Should().BeEquivalentTo(Pos.All);
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
