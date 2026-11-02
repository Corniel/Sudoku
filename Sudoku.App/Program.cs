using Puzzles;
using Puzzles.CrackingTheCryptic;
using Qowaiv.Text;
using System.CommandLine;
using System.Threading;

namespace Sudoku.App;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        AppDomain.CurrentDomain.ProcessExit += (_, _) => Console.Write(Ansi.Reset);
        Console.CancelKeyPress += (_, _) => Console.Write(Ansi.Reset);
        return await Cli(args).InvokeAsync();
    }

    private static ParseResult Cli(string[] args)
    {
        var nyt = new Command("nyt", "Download the New York Times puzzle of the day.");
        
        var filter = new Argument<string>("filter")
        {
            Description = "Which puzzles to run: classic, ? (unknown durations, default), oo (never finishes), 100 (up to 100 seconds), or all.",
            Arity = ArgumentArity.ZeroOrOne,
            DefaultValueFactory = _ => "?",
        };
        filter.AcceptOnlyFromAmong("all", "classic", "?", "oo", "100");

        var ctc = new Command("ctc", "Run Cracking the Cryptic puzzles.")
        {
            Arguments = { filter },
        };

        var dlx = new Option<bool>("--dlx") { Description = "Also solve with the Dancing Links solver." };
        var refr = new Option<bool>("--reference", "-r") { Description = "Also solve with the reference solver." };
        var all = new Option<bool>("--all", "-a") { Description = "Solve with all solvers (implies --dlx and --reference)." };

        Command test = new("test", "Solve sets of traditional Sudokus with different solvers.")
        {
            Options = { dlx, refr, all },
        };

        var size = new Option<int?>("--size", "-s") { Description = "Number of puzzles to generate." };
        var seed = new Option<int?>("--seed") { Description = "Seed for the random number generator." };

        Command gen = new("gen", "Generates Sudoku puzzles.")
        {
            Aliases = { "generate" },
            Options = { size, seed },
        };

        Command redo = new("redo", "Re-apply generation process to generated puzzles.");

        RootCommand root = new("Corniel’s Sudoku Solver")
        {
            Subcommands = { nyt, ctc, test, gen, redo },
        };

        ctc.SetAction(Ctc);
        gen.SetAction(Generate);
        nyt.SetAction(Nyt);
        redo.SetAction(Redo);
        test.SetAction(Test);

        return root.Parse(args);

        Task<int> Generate(ParseResult p, CancellationToken _)
        {
            Generator.Generate(p.GetValue(size) ?? 1000, p.GetValue(seed) ?? Random.Shared.Next());
            return Task.FromResult(0);
        }

        async Task<int> Nyt(ParseResult _, CancellationToken __)
        {
            var date = await NewYorkTimesCollector.Load();
            Console.WriteLine($"Downloaded {Ansi.Black | Ansi.BG.White}T{Ansi.Reset}{Ansi.Italic}he New York Times’{Ansi.Reset} hard puzzle for {Ansi.Gray(127)}{date:yyyy-MM-dd}{Ansi.Reset}");
            return 0;
        }

        Task<int> Ctc(ParseResult p, CancellationToken _)
        {
            Func<Puzzle, bool>? selected = p.GetValue(filter) switch
            {
                "classic" => puzzle => puzzle is CtcPuzzle { IsClassic: true },
                "?" => puzzle => puzzle.Duration is O.Unknown,
                "oo" => puzzle => puzzle.Duration is O.oo,
                "100" => puzzle => puzzle.Duration <= O.s100,
                _ => null,
            };
            Cracking_the_Cryptic.Run(selected);
            return Task.FromResult(0);
        }

        Task<int> Redo(ParseResult _, CancellationToken __)
        {
            Generator.ReApply();
            return Task.FromResult(0);
        }

        async Task<int> Test(ParseResult parsed, CancellationToken _)
        {
            var everything = parsed.GetValue(all);

            try
            {
                TestSets.SolveAll(
                    dlx: everything || parsed.GetValue(dlx),
                    refr: everything || parsed.GetValue(refr));
            }
            finally
            {
                Console.Write(Ansi.Reset);
            }
            return 0;
        }
    }
}
