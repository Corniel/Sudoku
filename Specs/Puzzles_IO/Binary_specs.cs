using Puzzles.IO;
using System.IO;

namespace Specs.Puzzles_IO.Binary_specs;

public class Writes
{
    [Test]
    public void Puzzles()
    {
        var clue = Clues.Parse("12.3.....4...5......6..17....1..68..3...4..7....2...5..1....9....9....68.....9..7");
        var cell = Cells.Parse("127368594493752186856491723571936842382145679964287351218674935749513268635829417");
        var puzzle = new BinaryPuzzle(clue, cell);

        using var stream = new MemoryStream();
        puzzle.WriteTo(stream);
        stream.Position = 0;

        var copy = BinaryPuzzle.Load(stream);

       copy.Should().Be(puzzle);
    }
}
