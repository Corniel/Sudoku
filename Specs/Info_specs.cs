using SudokuSolver.Common;
using SudokuSolver.Restrictions;

namespace Specs.Info_specs;

public class Bits
{
    private const double epsilon = 0.001;

    [TestCase(0, 0.63387)]
    [TestCase(1, 0.50695)]
    [TestCase(2, 0.28239)]
    public void Dutch_whipser(int skip, double bits) => new DutchWhisper.Neighbors(Pos.O, Pos.O, skip)
        .Bits.Should().BeApproximately(bits, epsilon);

    [Test]
    public void German_whipser() => new DeltaMin(Pos.O, Pos.O, 5).Bits.Should().BeApproximately(1.33086, epsilon);

    [TestCase(0, 0)]
    [TestCase(1, 0.16992)]
    [TestCase(2, 0.15002)]
    [TestCase(3, 0.13039)]
    [TestCase(4, 0.11103)]
    [TestCase(5, 0.09192)]
    [TestCase(6, 0.07306)]
    [TestCase(7, 0.05444)]
    [TestCase(8, 0.03606)]
    [TestCase(9, 0.01792)]
    public void Peer(int candidates, double bits)
        => Info.Peer(candidates).Should().BeApproximately(bits, epsilon);

    [TestCase(1, 3.1699)]
    [TestCase(2, 2.1699)]
    [TestCase(3, 1.5849)]
    [TestCase(4, 1.1699)]
    [TestCase(5, 0.8479)]
    [TestCase(6, 0.5849)]
    [TestCase(7, 0.3625)]
    [TestCase(8, 0.1699)]
    [TestCase(9, 0)]
    public void Cell(int count, double bits)
        => Info.Cell(count).Should().BeApproximately(bits, epsilon);
}

