using System.Globalization;
using System.IO;

namespace Puzzles.NewYorkTimes;

public sealed class NewYorkTimesPuzzle(DateOnly date, Clues clues, Cells solution) : Puzzle
{
    public DateOnly Date { get; } = date;

    public override string Title => Date.ToString("yyyy-MM-dd");

    public override Uri? Url => new("https://www.nytimes.com/puzzles/sudoku");

    public override Clues Clues { get; } = clues;

    public override Cells Solution { get; } = solution;

    public void WriteTo(StreamWriter writer)
    {
        writer.Write(Title);
        writer.Write(' ');
        for (var p = Pos.O; p < _9x9; p++)
        {
            writer.Write(Clues.FirstOrDefault(c => c.Pos == p).Digit);
        }
        writer.Write(' ');
        for (var p = Pos.O; p < _9x9; p++)
        {
            writer.Write(Solution[p]);
        }
        writer.Write('\n');
    }

    public static ImmutableArray<NewYorkTimesPuzzle> Hard => [.. Load(nameof(Hard))];

    public static IEnumerable<NewYorkTimesPuzzle> Load(string file)
    {
        using var stream = typeof(NewYorkTimesPuzzle).Assembly.GetManifestResourceStream($"Puzzles.NewYorkTimes.{file}.txt")!;
        using var reader = new StreamReader(stream);

        while (reader.ReadLine() is { } line)
        {
            if (line.Split(' ', StringSplitOptions.RemoveEmptyEntries) is { Length: 3 } parts)
            {
                yield return new NewYorkTimesPuzzle(
                    DateOnly.Parse(parts[0], CultureInfo.InvariantCulture),
                    Clues.Parse(parts[1]),
                    Cells.Parse(parts[2]));
            }
        }
    }
}
