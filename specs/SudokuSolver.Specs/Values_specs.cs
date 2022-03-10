namespace Values_specs;

public class Constants
{
    [Test]
    public void _9_Singles()
    {
        Values.Singles.Should().HaveCount(9);
        Values.Singles.Should().AllSatisfy(v => v.Count.Should().Be(1));
        Values.Singles.Should().BeEquivalentTo(Values.Singles.Distinct());
    }

    [Test]
    public void _36_Pairs()
    {
        Values.Pairs.Should().HaveCount(36);
        Values.Pairs.Should().AllSatisfy(v => v.Count.Should().Be(2));
        Values.Pairs.Should().BeEquivalentTo(Values.Pairs.Distinct());
    }

    [Test]
    public void _84_Triples()
    {
        Values.Triples.Should().HaveCount(84);
        Values.Triples.Should().AllSatisfy(v => v.Count.Should().Be(3));
        Values.Triples.Should().BeEquivalentTo(Values.Triples.Distinct());
    }

    [Test]
    public void _126_Quads()
    {
        Values.Quads.Should().HaveCount(126);
        Values.Quads.Should().AllSatisfy(v => v.Count.Should().Be(4));
        Values.Quads.Should().BeEquivalentTo(Values.Quads.Distinct());
    }
}
