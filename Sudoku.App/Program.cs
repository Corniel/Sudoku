namespace Sudoku.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args is not { Length: > 0 })
            Console.WriteLine("No command");

        else if (args[0] == "nyt")
            Console.WriteLine($"Downloaded NYT {await NewYorkTimesCollector.Load():yyy-MM-dd}");

        else if (args[0] == "gen")
        {
            var size = args.Length > 1 && int.TryParse(args[1], out var val0) ? val0 : 1000;
            var seed = args.Length > 2 && int.TryParse(args[2], out var val1) ? val1 : Random.Shared.Next();
            Generator.Generate(size, seed);
        }
        else if (args[0] == "ctc")
        {
            switch ((args.Length > 1 ? args[1] : string.Empty))
            {
                case "?": Cracking_the_Cryptic.Run(p => p.Duration is Puzzles.O.Unknown); break;
                case "oo": Cracking_the_Cryptic.Run(p => p.Duration is Puzzles.O.oo); break;
                case "100": Cracking_the_Cryptic.Run(p => p.Duration <= Puzzles.O.s100); break;
                default: Cracking_the_Cryptic.Run(); break;
            }
        }

        else if (args[0] == "test")
        {
            var all = args.Length > 1 && args[1] is "all";
            TestSets.SolveAll(dlx: all, refr: all);
        }
        else
            Console.WriteLine($"Unknown command '{args[0]}'");
    }

}
