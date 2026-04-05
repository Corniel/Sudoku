using Puzzles.NewYorkTimes;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        using var stream = new FileStream(Path, FileMode.Open, FileAccess.Read);
        var puzzles = NewYorkTimesPuzzle.Load(stream).ToList();
        stream.Close();

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

        var clues = new Clues([.. puzzle.Data.puzzle.Select((d, p) => new Cell(new Pos(p), d)).Where(c => c.Digit is not 0)]);
        var solution = Cells.Empty;

        for (Pos p = Pos.O; p < _9x9; p++)
            solution[p] = puzzle.Data.solution[p];

        return new(puzzle.PrintDate, clues, solution);
    }

    private sealed class Content
    {
        public Puzzle hard { get; init; } = new();
    }
    private sealed class Puzzle
    {
        [JsonPropertyName("print_date")]
        public DateOnly PrintDate { get; init; }

        [JsonPropertyName("puzzle_id")]
        public int Id { get; init; }

        [JsonPropertyName("difficulty")]
        public string Difficulty { get; init; } = string.Empty;

        [JsonPropertyName("puzzle_data")]
        public PuzzleData Data { get; init; } = new();
    }
    private sealed class PuzzleData
    {
        public ImmutableArray<int> puzzle { get; init; } = [];
        public ImmutableArray<int> solution { get; init; } = [];
    }


    private const string Start = """<script type="text/javascript">window.gameData = """;
    private const string End = "</script>";
}

