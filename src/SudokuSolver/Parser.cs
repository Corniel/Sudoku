namespace SudokuSolver;

internal static class Parser
{
    public static Cells Parse(string str)
    {
        if (str is null) throw new ArgumentNullException(nameof(str));

        var tokens = str.Select(Tokenized).Where(t => t != Token.Invalid).ToArray();

        //if (!Dimensions(tokens, 9, 9)) throw new FormatException("Not a valid sudoku puzzle.");

        return new Cells(tokens.Where(t => t != Token.NewLine).Select(Value).ToArray());
    }
    static bool Dimensions(IEnumerable<Token> tokens, int rows, int cols)
    {
        var r = 0;
        var c = 0;

        foreach (var token in tokens)
        {
            if (token == Token.NewLine)
            {
                if (c == 9)
                {
                    c = 0;
                    r++;
                }
                else if (c != 0) return false;
            }
            else
            {
                if (++c > cols) return false;
            }
        }
        return r == rows && c == cols;
    }

    private static uint Value(Token token) => (uint)Cells[token];
    private static Token Tokenized(char ch) => Tokens.TryGetValue(ch, out var token) ? token : Token.Invalid;

    private static readonly Dictionary<Token, Values> Cells = new()
    {
        { Token.Num1, Values.Value1 },
        { Token.Num2, Values.Value2 },
        { Token.Num3, Values.Value3 },
        { Token.Num4, Values.Value4 },
        { Token.Num5, Values.Value5 },
        { Token.Num6, Values.Value6 },
        { Token.Num7, Values.Value7 },
        { Token.Num8, Values.Value8 },
        { Token.Num9, Values.Value9 },
        { Token.Unknown, Values.Unknown },
    };
    private static readonly Dictionary<char, Token> Tokens = new()
    {
        { '.', Token.Unknown },
        { '?', Token.Unknown },
        { '1', Token.Num1 },
        { '2', Token.Num2 },
        { '3', Token.Num3 },
        { '4', Token.Num4 },
        { '5', Token.Num5 },
        { '6', Token.Num6 },
        { '7', Token.Num7 },
        { '8', Token.Num8 },
        { '9', Token.Num9 },
        { '\n', Token.NewLine },
    };

    /// <summary>Represents the possible tokens of a Sudoku puzzle.</summary>
    enum Token
    {
        Invalid,
        Unknown,
        Num1,
        Num2,
        Num3,
        Num4,
        Num5,
        Num6,
        Num7,
        Num8,
        Num9,
        NewLine,
    }
}
