namespace SudokuSolver.Techniques;

/// <summary>Uses steering wheel symmetry to reduce cells.</summary>
/// <remarks></remarks>
/// all digits in {b} should also be in {a}
///
/// a..|aaa|..a
/// .bb|...|bb.
/// .bb|...|bb.
/// ---+---+---
/// a..|...|..a
/// a..|...|..a
/// a..|...|..a
/// ---+---+---
/// .bb|...|bb.
/// .bb|...|bb.
/// a..|aaa|..a
/// </remarks>
public class SteeringWheel : Technique
{
    private static readonly int[] outer = new[]
    {
        00, /**/ 03, 04, 05, /**/ 08,
        //
        27, /*                 */ 35,
        36, /*                 */ 44,
        45, /*                 */ 53,
        //
        72, /**/ 75, 76, 77, /**/ 80,
    };

    private static readonly int[] inner = new[]
    {
        10, 11, /**/ 15, 16,
        19, 20, /**/ 24, 25,
        //
        55, 56, /**/ 60, 61,
        64, 65, /**/ 69, 70
    };

    public Cells? Reduce(Cells cells, Regions regions)
    {
        Cells reduced = cells;

        var _inner = inner.Select(index => cells[index]).Where(c => c.SingleValue()).ToList();
        var _outer = outer.Select(index => cells[index]).Where(c => c.SingleValue()).ToList();

        var inner_undecided = 16 - _inner.Count;
        var outer_undecided = 16 - _outer.Count;

        foreach (var value in Values.Singles)
        {
            while (_inner.Contains(value) && _outer.Contains(value))
            {
                _inner.Remove(value);
                _outer.Remove(value);
            }
        }

        if (inner_undecided == _outer.Count)
        {
            var mask = _outer.Or();

            foreach(var index in inner)
            {
                Values cell = reduced[index];
                
                if (cell.IsUndecided())
                {
                    reduced = reduced.And(Location.Index(index), mask);
                }
            }
        }
        if (outer_undecided == _inner.Count)
        {
            var mask = _inner.Or();

            foreach (var index in outer)
            {
                Values cell = reduced[index];

                if (cell.IsUndecided())
                {
                    reduced = reduced.And(Location.Index(index), mask);
                }
            }
        }
        return reduced == cells ? null : reduced;
    }
}
