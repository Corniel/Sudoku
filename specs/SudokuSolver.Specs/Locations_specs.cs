namespace Locations_specs;

public class Count
{
    [Test]
    public void sum_lo_and_hi()
    {
        var locations = Locations.None | Location.Index(4) | Location.Index(80);
        locations.Count.Should().Be(2);
    }
}

public class Iterator
{
    [Test]
    public void iterates_lo_and_hi()
    {
        var locations = Locations.None
            | Location.Index(4)
            | Location.Index(42)
            | Location.Index(68)
            | Location.Index(80);

        locations.Should().BeEquivalentTo(new[]
        {
            Location.Index(4),
            Location.Index(42),
            Location.Index(68),
            Location.Index(80)
        });
    }
}

public class Not
{
    [Test]
    public void Negates_collection()
    {
        Locations.None.Not().Should().HaveCount(81);
    }
}