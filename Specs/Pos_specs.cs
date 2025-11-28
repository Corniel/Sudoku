namespace Specs.Pos_specs;

public class Decomposes
{
    [Test]
    public void Rows([Range(0, 80)]int index)
    {
        var pos = new Pos(index);
        var (row, _) = pos;
        row.Should().Be(pos.Row);
    }

    [Test]
    public void Cols([Range(0, 80)] int index)
    {
        var pos = new Pos(index);
        var (_, col) = pos;
        col.Should().Be(pos.Col);
    }
}
