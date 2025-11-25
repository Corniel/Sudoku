using Puzzles.NewYorkTimes;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace Sudoku.App;

public static class NewYorkTimesCollector
{
    private static readonly Uri Url = new("https://www.nytimes.com/puzzles/sudoku/hard");
    private const string Path = "C:/code/sudoku/Puzzles/NewYorkTimes/Hard.txt";

    public static async Task<DateOnly> Load()
    {
        using var client = new HttpClient();
        var content = await client.GetStringAsync(Url);
        var puzzle = Parse(content);

        var puzzles = NewYorkTimesPuzzle.Hard.ToList();

        if (!puzzles.Any(p => p.Date == puzzle.Date))
        {
            puzzles.Add(puzzle);

            using var writer = new StreamWriter(Path, false);
            foreach (var p in puzzles.OrderBy(p => p.Date))
                p.WriteTo(writer);

            await writer.FlushAsync();
        }
        return puzzle.Date;
    }

    public static NewYorkTimesPuzzle Parse(string content)
    {
        var span = content.AsSpan(content.IndexOf(Start) + Start.Length);
        span = span[..span.IndexOf(End)];

        var puzzle = JsonSerializer.Deserialize<Content>(span)!.hard;

        var clues = new Clues([.. puzzle.puzzle_data.puzzle.Select((d, p) => new Cell(new Pos(p), d)).Where(c => c.Digit is not 0)]);
        var solution = Cells.Empty;
        
        for (Pos p = Pos.O; p < _9x9; p++)
            solution[p] = puzzle.puzzle_data.solution[p];

        return new(puzzle.print_date, clues, solution);
    }

    private sealed class Content
    {
        public Puzzle hard { get; init; } = new();
    }
    private sealed class Puzzle
    {
        public DateOnly print_date { get; init; }
        public int puzzle_id { get; init; }
        public string difficulty { get; init; } = string.Empty;
        public PuzzleData puzzle_data { get; init; } = new();
    }
    private sealed class PuzzleData
    {
        public ImmutableArray<int> puzzle { get; init; } = [];
        public ImmutableArray<int> solution { get; init; } = [];
    }


    private const string Start = """<script type="text/javascript">window.gameData = """;
    private const string End = "</script>";
}

