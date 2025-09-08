namespace SudokuSolver.Solvers;

/// <summary>
/// Straight-forward multi-demensional aray based backtracking Sudoku solver.
/// </summary>
/// <remarks>
/// See; https://www.geeksforgeeks.org/dsa/sudoku-backtracking-7/.
/// </remarks>
public static class Backtracker
{
    public static Cells Solve(Clues clues)
    {
        var mat = new int[9, 9];
        foreach (var (r, c, v) in clues)
        {
            mat[r, c] = v;
        }

        Solve(mat, 0, 0);
        return mat.ToCells();
    }

    // Function to solve the Sudoku problem
    private static bool Solve(int[,] mat, int row, int col)
    {
        // base case: Reached nth column of the last row
        if (row == 8 && col == 9)
            return true;

        // If last column of the row go to the next row
        if (col == 9)
        {
            row++;
            col = 0;
        }

        // If cell is already occupied then move forward
        if (mat[row, col] != 0)
            return Solve(mat, row, col + 1);

        for (int num = 1; num <= 9; num++)
        {
            // If it is safe to place num at current position
            if (IsSafe(mat, row, col, num))
            {
                mat[row, col] = num;
                if (Solve(mat, row, col + 1))
                    return true;
                mat[row, col] = 0;
            }
        }

        return false;
    }

    // Function to check if it is safe to place num at mat[row][col]
    private static bool IsSafe(int[,] mat, int row, int col, int num)
    {
        // Check if num exists in the row
        for (int x = 0; x < 9; x++)
            if (mat[row, x] == num)
                return false;

        // Check if num exists in the col
        for (int x = 0; x < 9; x++)
            if (mat[x, col] == num)
                return false;

        // Check if num exists in the 3x3 sub-matrix
        int startRow = row - (row % 3), startCol = col - (col % 3);

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (mat[i + startRow, j + startCol] == num)
                    return false;

        return true;
    }

    private static Cells ToCells(this int[,] mat)
    {
        var cells = Cells.Empty;

        for (var r = 0; r < 9; r++)
            for (var c = 0; c < 9; c++)
                cells[r, c] = mat[r, c];

        return cells;
    }
}
