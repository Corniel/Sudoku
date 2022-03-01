namespace SudokuSolver;

public readonly struct Cell
{
    public readonly Location Location;
    public readonly Values Values;

    public Cell(Location location, Values values)
    {
        Location = location;
        Values = values;
    }

    public override string ToString() => $"{Location} {Values}";
}
