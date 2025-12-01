using Puzzles.IO;
using Puzzles.PuzzleBank;
using System.IO;

namespace Specs.IO.BinaryPuzzle_specs;

public class Consts
{
    [Test]
    public void Mask_has_bit_count_100()
        => UInt128.PopCount(BinaryPuzzle.CellsMask).Should().Be(100);

    [Test]
    public void Byte_size_is_23()
        => BinaryPuzzle.ByteSize.Should().Be(23);
}

public class Writes
{
    [Test]
    public void Puzzle()
    {
        var clue = Clues.Parse("12.3.....4...5......6..17....1..68..3...4..7....2...5..1....9....9....68.....9..7");

        Console.WriteLine(clue);

        var cells = Cells.Parse("""
            127│368│594
            493│752│186
            856│491│723
            ───┼───┼───
            571│936│842
            382│145│679
            964│287│351
            ───┼───┼───
            218│674│935
            749│513│268
            635│829│417
            """);

        var puzzle = new BinaryPuzzle(clue, cells);

        using var stream = new MemoryStream();
        puzzle.WriteTo(stream);

        stream.Should().HaveLength(23);
        stream.Position = 0;

        var copy = BinaryPuzzle.Load(stream);

        copy.Solution.Should().Be(cells);
        copy.Clues.Should().BeEquivalentTo(clue);
    }

    [Explicit]
    [Test]
    public void Puzzles()
    {
        using var stream = new MemoryStream();

        foreach (var puzzle in PuzzleBankPuzzle.Diabolical.Take(100))
            new BinaryPuzzle(puzzle).WriteTo(stream);

        stream.Position = 0;

        for (var i = 0; i < 100; i++)
        {
            var puzzle = PuzzleBankPuzzle.Diabolical[i];
            var copy = BinaryPuzzle.Load(stream);

            copy.Solution.Should().Be(puzzle.Solution);
            Cells.New(copy.Clues).Should().Be(Cells.New(puzzle.Clues));
        }
    }

    [Explicit]
    [Test]
    public void PuzzleBank_easy_puzzles()
    {
        using var stream = new FileStream("Easy.bin", FileMode.Create, FileAccess.Write);

        foreach (var puzzle in PuzzleBankPuzzle.Easy)
            new BinaryPuzzle(puzzle).WriteTo(stream);

        stream.Flush();

        Console.WriteLine(stream.Name);

        stream.Length.Should().Be(2_300_000L);
    }
}
