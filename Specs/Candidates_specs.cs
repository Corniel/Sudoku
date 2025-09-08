namespace Specs.Candidates_specs;

public class Flags
{
    private const int _ = 0;
    private const bool true_ = true;

    [TestCase(false, 0)]
    [TestCase(false, 1, 2)]
    [TestCase(false, 7, 8, 9)]
    [TestCase(true_, 1)]
    [TestCase(true_, 2)]
    [TestCase(true_, 3)]
    [TestCase(true_, 4)]
    [TestCase(true_, 5)]
    [TestCase(true_, 6)]
    [TestCase(true_, 7)]
    [TestCase(true_, 8)]
    [TestCase(true_, 9)]
    public void Has_single(bool hasSingle, params int[] values)
    {
        var cell = Candidates.New(values);
        cell.HasSingle.Should().Be(hasSingle);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    [TestCase(3, 4, 9)]
    [TestCase(1, 2, 3, 4, 9)]
    public void First(params int[] values)
    {
        var cell = Candidates.New(values);
        cell.First().Should().Be(values[0]);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    [TestCase(3, 4, 7)]
    [TestCase(1, 2, 3, 4, 9)]
    public void Last(params int[] values)
    {
        var cell = Candidates.New(values);
        cell.Last().Should().Be(values[^1]);
    }

    [Test]
    public void All_1_to_9()
     {
        int[] candidates = [.. Candidates.All];
        candidates.Should().BeEquivalentTo([1, 2, 3, 4, 5, 6, 7, 8, 9]);
    }

    [TestCase(0, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(1, /* => */ _, 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(2, /* => */ 1, _, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(3, /* => */ 1, 2, _, 4, 5, 6, 7, 8, 9)]
    [TestCase(4, /* => */ 1, 2, 3, _, 5, 6, 7, 8, 9)]
    [TestCase(5, /* => */ 1, 2, 3, 4, _, 6, 7, 8, 9)]
    [TestCase(6, /* => */ 1, 2, 3, 4, 5, _, 7, 8, 9)]
    [TestCase(7, /* => */ 1, 2, 3, 4, 5, 6, _, 8, 9)]
    [TestCase(8, /* => */ 1, 2, 3, 4, 5, 6, 7, _, 9)]
    [TestCase(9, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, _)]
    public void Value(int value, params int[] values)
    {
        var reduced = Candidates.All ^ value;
        reduced.Should().Be([.. values.Where(v => v != _)]);
    }
}

public class Maths
{
    [TestCase(0 /*   =>   [ ] */)]
    [TestCase(1, /*  => */ 1)]
    [TestCase(2, /*  => */ 1, 2)]
    [TestCase(3, /*  => */ 1, 2, 3)]
    [TestCase(4, /*  => */ 1, 2, 3, 4)]
    [TestCase(5, /*  => */ 1, 2, 3, 4, 5)]
    [TestCase(6, /*  => */ 1, 2, 3, 4, 5, 6)]
    [TestCase(7, /*  => */ 1, 2, 3, 4, 5, 6, 7)]
    [TestCase(8, /*  => */ 1, 2, 3, 4, 5, 6, 7, 8)]
    [TestCase(9, /*  => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(10, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(11, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(20, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    public void At_most(int value, params int[] values) 
        => Candidates.AtMost(value).Should().Be([.. values]);

    [TestCase(-1, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(+0, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(+1, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(+2, /* => */ 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(+3, /* => */ 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(+4, /* => */ 4, 5, 6, 7, 8, 9)]
    [TestCase(+5, /* => */ 5, 6, 7, 8, 9)]
    [TestCase(+6, /* => */ 6, 7, 8, 9)]
    [TestCase(+7, /* => */ 7, 8, 9)]
    [TestCase(+8, /* => */ 8, 9)]
    [TestCase(+9, /* => */ 9)]
    public void At_least(int value, params int[] values)
       => Candidates.AtLeast(value).Should().Be([.. values]);

    [TestCase(-2, -1 /* => [] */ )]
    [TestCase(+2, +1 /* => [] */ )]
    [TestCase(-1, 3, /* => */ 1, 2, 3)]
    [TestCase(0, 3, /* => */ 1, 2, 3)]
    [TestCase(1, 3, /* => */ 1, 2, 3)]
    [TestCase(4, 7, /* => */ 4, 5, 6, 7)]
    [TestCase(1, 9, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    [TestCase(0, 45, /* => */ 1, 2, 3, 4, 5, 6, 7, 8, 9)]
    public void Between(int min, int max, params int[] values)
      => Candidates.Between(min, max).Should().Be([.. values]);

    [TestCase(1, 0, 1 + 0)]
    [TestCase(1, 2, 1 + 2)]
    [TestCase(3, 5, 3 + 5)]
    [TestCase(4, 5, 4 + 5)]
    [TestCase(0, 5, 0)]
    [TestCase(5, 5, 0)]
    public void Adds(int l, int r, int sum)
    {
        (Candidates.New(l) + r).Should().Be([sum]);
    }

    [Test]
    public void Adds_many()
        => (Candidates.New(1, 3, 4) + 2).Should().Be([3, 5, 6]);

    [TestCase(5, 0, 5 - 0)]
    [TestCase(2, 1, 2 - 1)]
    [TestCase(5, 3, 5 - 3)]
    [TestCase(9, 6, 9 - 6)]
    [TestCase(5, 5, 0)]
    public void Subtracts(int l, int r, int sum)
    {
        (Candidates.New(l) - r).Should().Be([sum]);
    }

    [Test]
    public void Subtracts_many()
        => (Candidates.New(3, 4) - 2).Should().Be([1, 2]);
}
