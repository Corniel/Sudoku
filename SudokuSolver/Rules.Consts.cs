using SudokuSolver.Houses;

namespace SudokuSolver;

public readonly partial struct Rules
{
    /// <summary>An empty set of rules.</summary>
    public static readonly Rules None = new([], [.. range(_9x9).Select(p => Constraint.None(new Pos(p)))]);

    /// <summary>The basic set of rules (rows and columns only).</summary>
    public static readonly Rules Basic = None + Row.All + Col.All;

    /// <summary>The standard set of houses.</summary>
    public static readonly Rules Standard = Basic + Box.All;

    /// <summary>The standard set of housed extended with the <see cref="Constraints.AntiKing"/> restrictions.</summary>
    public static readonly Rules AntiKing = Standard + Common.AntiKing.All;

    /// <summary>The standard set of housed extended with the <see cref="Constraints.AntiKnight"/> restrictions.</summary>
    public static readonly Rules AntiKnight = Standard + Common.AntiKnight.All;

    /// <summary>The standard set of houses extended with the four windows.</summary>
    public static readonly Rules Hyper = Standard + Window.All;

    /// <summary>The standard set of houses extended with both diagonals.</summary>
    public static readonly Rules XSudoku = Standard + Diagonal.NE_SW + Diagonal.NW_SE;

    /// <summary>The rows, columsn and jigsaw shaped houses.</summary>
    public static Rules Jigsaw(string jigsaws) => Basic + Common.Jigsaw.Parse(jigsaws);

    /// <summary>The rows, columsn and jigsaw shaped houses.</summary>
    public static Rules Killer(string killer) => Standard + Common.KillerCages.Parse(killer);
}
