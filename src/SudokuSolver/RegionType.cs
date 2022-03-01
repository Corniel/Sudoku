namespace SudokuSolver;

public enum RegionType
{
	Unknown = 0,
	Row,
	Column,
	Block,
	Miscellaneous,
}

public static class RegionTypeExtensions
{
	public static bool RowOrColumn(this RegionType type) 
		=> type == RegionType.Row 
		|| type == RegionType.Column;
}