namespace Specs.Domino_specs;

public class Collections
{
    [Test]
    public void All()
        => Dominos.All.Should().OnlyHaveUniqueItems().And.HaveCount(144 + 128);

    [Test]
    public void Orthongal()
        => Dominos.Ort.Should().OnlyHaveUniqueItems().And.HaveCount(144);

    [Test]
    public void Diogonal()
        => Dominos.Dig.Should().OnlyHaveUniqueItems().And.HaveCount(128);

    [Test]
    public void Horizontal()
        => Dominos.Hor.Should().OnlyHaveUniqueItems().And.HaveCount(72);

    [Test]
    public void Vertical()
        => Dominos.Ver.Should().OnlyHaveUniqueItems().And.HaveCount(72);
}

