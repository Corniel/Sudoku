namespace Sudoku;

public readonly partial struct RuleSet
{
    /// <summary>An empty set of rules.</summary>
    public static readonly RuleSet None = new([]);

    /// <summary>The basic set of rules (rows and columns only).</summary>
    public static readonly RuleSet Basic = None + Houses.Rows + Houses.Cols;

    /// <summary>The standard set of houses.</summary>
    public static readonly RuleSet Standard = Basic + Houses.Boxes;

    /// <summary>The standard set of housed extended with the <see cref="Anti.Knight"/> restrictions.</summary>
    public static readonly RuleSet AntiKnight = Standard + Anti.Knight;

    /// <summary>The standard set of houses extended with the four windows.</summary>
    public static readonly RuleSet Hyper = Standard + Houses.Windows;

    /// <summary>The standard set of houses extended with both diagonals.</summary>
    public static readonly RuleSet XSudoku = Standard + Diagonal.NE_SW + Diagonal.NW_SE;

    /// <summary>The rows, columns and jigsaw shaped houses.</summary>
    public static RuleSet Jigsaw(string grid) => Basic + Sudoku.Sets.Jigsaw.New(grid);

    /// <summary>The rows, columsn and jigsaw shaped houses.</summary>
    public static RuleSet Killer(string grid) => Standard + Common.Groups.Cages(grid);
}
