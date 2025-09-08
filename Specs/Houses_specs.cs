using System;

namespace Specs.Houses_specs;

public class Rows
{
    [Test]
    public void unique_for([Range(0, 8)] int index)
        => Houses.Row[index].Should().HaveCount(9);

    [Test]
    public void all_unique()
        => Houses.Row.SelectMany(x => x).Should().BeEquivalentTo(Pos.All);
}

public class Cols
{
    [Test]
    public void unique_for([Range(0, 8)] int index)
        => Houses.Col[index].Should().HaveCount(9);

    [Test]
    public void all_unique()
        => Houses.Col.SelectMany(x => x).Should().BeEquivalentTo(Pos.All);
}

public class Boxes
{
    [Test]
    public void unique_for([Range(0, 8)] int index)
        => Houses.Box[index].Should().HaveCount(9);

    [Test]
    public void all_unique()
        => Houses.Box.SelectMany(x => x).Should().BeEquivalentTo(Pos.All);
}

public class Diagonals
{
    [Test]
    public void NW_SE()
        => Houses.Diagonal.NW_SE.Should().BeEquivalentTo(
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
        => Houses.Diagonal.NE_SW.Should().BeEquivalentTo(
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
