using DynamicSolver;

namespace Specs.Tools;

public static class Logger
{
    public static IDisposable Options() => new OptionsLogger();

    private sealed class OptionsLogger : IDisposable
    {
        public OptionsLogger() => Array.Clear(Iterator.Options);

        public void Dispose()
        {
            Console.WriteLine();
            Console.WriteLine("## Options");

            for (var d = 1; d <= _9; d++)
            {
                if (Iterator.Options[d] is not 0)
                    Console.WriteLine($"{d,-3} = {Iterator.Options[d],10:#,##0}");
            }
        }
    }
}
