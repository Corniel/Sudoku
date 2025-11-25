using Sudoku.App;
using System.IO;

namespace Specs.App.NewYorkTimesCollector_specs;

public class Parses
{
    [Test]
    public void Content()
    {
        using var stream = GetType().Assembly.GetManifestResourceStream("Specs.App.Content.html")!;
        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();

        var puzzle = NewYorkTimesCollector.Parse(content);

        TestSolver.Solve(puzzle).Should().Be(puzzle.Solution);
    }
}
