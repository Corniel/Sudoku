using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool Hidden(Cells cells, Context rules)
    {
        var reduce = false;

        foreach (var house in rules.Houses)
        {
            var hidden = new PosSet[_9 + 1];

            foreach (var p in house)
            {
                foreach (var v in rules[p].Candidates)
                {
                    hidden[v] |= p;
                }
            }
            for (var val = 1; val <= _9; val++)
            {
                var count = hidden[val].Count;

                if (count is 1)
                {
                    reduce |= Remove(hidden[val].First(), val, cells, rules);
                }
                else if (count is 2)
                {
                    var pair = hidden[val];
                    reduce |= Pair(hidden, val, pair, house);
                }
            }
        }
        return reduce;

        bool Pair(PosSet[] hidden, int val, PosSet pair, Rule house)
        {
            for (var s = val + 1; s <= _9; s++)
            {
                if (hidden[s] == pair)
                {
                    var values = Candidates.New(val, s);
                    return Remove(house.Cells ^ pair, ~values, house.Cells, rules);
                }
            }
            return reduce;
        }
    }
}
