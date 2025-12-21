namespace Specs.Indexes_specs;

public class Constants
{
    [Test]
    public void _0_8()
    {
        var indexes = Indexes._0_8;
        indexes.Should().BeEquivalentTo([0, 1, 2, 3, 4, 5, 6, 7, 8]);
    }
}
