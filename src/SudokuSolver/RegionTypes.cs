namespace SudokuSolver;

public static class RegionTypes
{
	public static readonly IReadOnlyCollection<RegionType> All = new[] 
	{
		RegionType.Row, 
		RegionType.Column,
		RegionType.Square,
		RegionType.Miscellaneous,
	};

	public static bool RowOrColumn(this RegionType type)
		=> type == RegionType.Row
		|| type == RegionType.Column;
}
