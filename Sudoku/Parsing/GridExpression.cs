using static Sudoku.Parsing.GridExpression;

namespace Sudoku.Parsing;

public readonly record struct GridExpression(OperatorKind Operator, ImmutableArray<string> Args) : GridItem
{
    public GridExpression(string op, string[] args) : this(
        op switch
        {
            "=" => OperatorKind.EQ,
            "<" => OperatorKind.LT,
            ">" => OperatorKind.GT,
            "≤" => OperatorKind.LE,
            "≥" => OperatorKind.GE,
            ":" => OperatorKind.Contains,
            _ => throw new ArgumentException($"{op} is an unknown operator", nameof(op)),
        },
        [.. args])
        { }

    public enum OperatorKind
    {
        EQ,
        LT,
        GT,
        LE,
        GE,
        Contains,
    }

    public bool WithDigits => Args[^1].Any(char.IsAsciiDigit);

    public Digits Digits
    {
        get
        {
            if (!WithDigits) throw new InvalidOperationException("Digits is only supported for epxressions with digits.");

            var digits = Digits.None;

            foreach (var ch in Args[^1])
                digits |= ch - '0';

            return digits;
        }
    }

    public Ints Ints(int cells)
    {
        if (!WithDigits) throw new InvalidOperationException("Ints is only supported for epxressions with digits.");

        var value = int.Parse(Args[^1]);
        var upper = (cells * _9) + 1;
        if (Operator is OperatorKind.EQ)
        {
            return [value];
        }
        else if (Operator is OperatorKind.LT or OperatorKind.LE)
        {
            if (Operator is OperatorKind.LT) value--;

            Ints min = [.. range(cells, upper)];
            Ints lt = [.. range(1, value)];
            return lt & min;
        }
        else
        {
            if (Operator is OperatorKind.GT) value++;

            Ints max = [.. range(upper)];
            Ints gt = [.. range(value, upper)];
            return gt & max;
        }
    }

    public override string ToString() => string.Join(ToString(Operator), Args);

    private static string ToString(OperatorKind op) => op switch
    {
        OperatorKind.EQ => " = ",
        OperatorKind.LT => " < ",
        OperatorKind.GT => " > ",
        OperatorKind.LE => " ≤ ",
        OperatorKind.GE => " ≥ ",
        OperatorKind.Contains => ": ",
        _ => throw new NotSupportedException($"{op} is unknown."),
    };
}
