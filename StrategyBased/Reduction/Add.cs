using StrategyBased;
using Sudoku.Common;
using Sudoku.Restrictions;

namespace Sudoku.Reduction;

public static class Add
{
    public static void Cages(Nodes graph)
    {
        var cages = new List<KillerCage>();

        foreach (var house in graph.Houses.Select(h => h.Cells))
        {
            var cage = house;
            var sum = _45;

            foreach (var rule in graph.Rules.Where(r => r.Cells.IsSubsetOf(cage)))
            {
                if (rule is FixedSum fs)
                {
                    sum -= fs.Sum;
                    cage ^= rule.Cells;
                }
            }

            foreach (var cell in cage)
            {
                var value = graph[cell].Digit;
                if (value is not 0)
                {
                    sum -= value;
                    cage ^= cell;
                }
            }

            if (cage.HasSingle)
                graph[cage.First()].Digits = [sum];
            else if (sum is > 0 and < _45)
                cages.Add(new KillerCage(sum, cage));
        }

        graph.Rules += cages;
    }
}
