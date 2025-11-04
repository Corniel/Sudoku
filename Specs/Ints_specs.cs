namespace Specs.Ints_specs;

public class Zero
{
    [Test]
    public void reprsents_a_zero_int()
    {
        var ints = Ints.Zero;
        var added = ints + [3];
        added.Should().BeEquivalentTo(
            Ints.New(3));
    }
}

public class Adds
{
    [Test]
    public void digits()
    {
        Ints ints = [42, 43];
        var subtract = ints + [2, 4];
        subtract.Should().BeEquivalentTo(
            Ints.New(
                42 + 4,
                42 + 2,
                43 + 4,
                43 + 2));
    }
}

public class Subtracts
{
    [Test]
    public void digits()
    {
        Ints ints = [42, 43];
        var subtract = ints - [2, 4];
        subtract.Should().BeEquivalentTo(
            Ints.New(
                42 - 4, 
                42 - 2,
                43 - 4,
                43 - 2));
    }
}

public class Divides
{
    [Test]
    public void digits()
    {
        Ints ints = [80];
        var subtract = ints / [1, 2, 3, 4, 5, 8];
        subtract.Should().BeEquivalentTo(
            Ints.New(
                80,
                80 / 2,
                80 / 4,
                80 / 5,
                80 / 8));
    }
}
