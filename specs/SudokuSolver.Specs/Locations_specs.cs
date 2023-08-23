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

    [Test]
    public void interats()
    {
        var locations = new Locations(lo: 13666849599736162014, hi: 26124);
        locations.Count.Should().Be(43);

        locations.ToArray().Length.Should().Be(43);
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