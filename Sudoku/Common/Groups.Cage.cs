namespace Sudoku.Common;

public static partial class Groups
{
    public static Rules Cages(string grid, bool isSet = true)
    {
        var items = Grid.Items(grid);
        var groups = items.OfType<NamedGroup>().ToDictionary(g => g.Name, g => g.Cells);

        List<Rule> rules = [.. items.OfType<GridClue>().Select(c => new Mask(c.Pos, [c.Digit]))];

        foreach (var cage in items.OfType<GridExpression>().Where(e => e.WithDigits && e.Operator is not GridExpression.OperatorKind.Contains))
        {
            foreach (var arg in cage.Args[..^1])
            {
                var cells = groups[arg[0]];
                if (isSet)
                {
                    rules.AddRange(KillerCage(cells, cage.Ints(cells.Count)));
                }
                else
                {
                    rules.AddRange(SumCage(cells, cage.Ints(cells.Count)));
                }
            }
        }

        foreach (var quadruple in items.OfType<GridExpression>().Where(e => e.Operator is GridExpression.OperatorKind.Contains))
        {
            foreach (var arg in quadruple.Args[..^1])
            {
                var cells = groups[arg[0]];
                rules.AddRange(Group.Select(cells, (a, o) => new Quadruple(a, o, quadruple.Digits)));
            }
        }

        foreach (var sum in items.OfType<GridExpression>().Where(e => !e.WithDigits))
        {
            rules.AddRange(SameSum.New([.. sum.Args.Select(name => groups[name[0]].ToImmutableArray())]));
        }

        return rules;
    }

    public static Rules KillerCage(PosSet cells, Ints sum) =>
    [
        new CellSet(cells, "Killer cage"),
        .. SumCage(cells, sum),
    ];

    public static Rules SumCage(PosSet cells, Ints sum) => cells.Count switch
    {
        _ when sum.HasNone => [],
        1 => [new Mask(cells.First(), sum.Digits)],
        2 => [new Cage2(cells.First(), cells.Last(), sum), new Cage2(cells.Last(), cells.First(), sum)],
        _ => Group.Select(cells, (appliesTo, others) => new Cage(appliesTo, others, sum)),
    };
}
