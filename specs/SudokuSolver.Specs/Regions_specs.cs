using SudokuSolver;
using System.Linq;

namespace Regions_specs;

public class All
{
    [Test]
    public void Rows()
    {
        var regions = Regions.Rows().Select(r => r.ToArray());
        regions.Should().BeEquivalentTo(new[]
        {
            new[]{ 00, 01, 02, 03, 04, 05, 06, 07, 08 },
            new[]{ 09, 10, 11, 12, 13, 14, 15, 16, 17 },
            new[]{ 18, 19, 20, 21, 22, 23, 24, 25, 26 },
            new[]{ 27, 28, 29, 30, 31, 32, 33, 34, 35 },
            new[]{ 36, 37, 38, 39, 40, 41, 42, 43, 44 },
            new[]{ 45, 46, 47, 48, 49, 50, 51, 52, 53 },
            new[]{ 54, 55, 56, 57, 58, 59, 60, 61, 62 },
            new[]{ 63, 64, 65, 66, 67, 68, 69, 70, 71 },
            new[]{ 72, 73, 74, 75, 76, 77, 78, 79, 80 },
        }.Select(row => row.Select(i => Location.Index(i))));
    }

    [Test]
    public void Columns()
    {
        var regions = Regions.Columns().Select(r => r.ToArray());
        regions.Should().BeEquivalentTo(new[]
        {
            new[]{ 00, 09, 18, 27, 36, 45, 54, 63, 72 },
            new[]{ 01, 10, 19, 28, 37, 46, 55, 64, 73 },
            new[]{ 02, 11, 20, 29, 38, 47, 56, 65, 74 },
            new[]{ 03, 12, 21, 30, 39, 48, 57, 66, 75 },
            new[]{ 04, 13, 22, 31, 40, 49, 58, 67, 76 },
            new[]{ 05, 14, 23, 32, 41, 50, 59, 68, 77 },
            new[]{ 06, 15, 24, 33, 42, 51, 60, 69, 78 },
            new[]{ 07, 16, 25, 34, 43, 52, 61, 70, 79 },
            new[]{ 08, 17, 26, 35, 44, 53, 62, 71, 80 },
        }.Select(row => row.Select(i => Location.Index(i))));
    }

    [Test]
    public void Blocks()
    {
        var regions = Regions.Blocks().Select(r => r.ToArray());

        foreach(var array in regions)
        {
            Console.WriteLine($"new[]{{ {string.Join(", ", array)} }},");
        }

        regions.Should().BeEquivalentTo(new[]
        {
            new[]{ 00, 01, 02, /**/ 09, 10, 11, /**/ 18, 19, 20 },
            new[]{ 03, 04, 05, /**/ 12, 13, 14, /**/ 21, 22, 23 },
            new[]{ 06, 07, 08, /**/ 15, 16, 17, /**/ 24, 25, 26 },

            new[]{ 27, 28, 29, /**/ 36, 37, 38, /**/ 45, 46, 47 },
            new[]{ 30, 31, 32, /**/ 39, 40, 41, /**/ 48, 49, 50 },
            new[]{ 33, 34, 35, /**/ 42, 43, 44, /**/ 51, 52, 53 },

            new[]{ 54, 55, 56, /**/ 63, 64, 65, /**/ 72, 73, 74 },
            new[]{ 57, 58, 59, /**/ 66, 67, 68, /**/ 75, 76, 77 },
            new[]{ 60, 61, 62, /**/ 69, 70, 71, /**/ 78, 79, 80 },
        }.Select(row => row.Select(i => Location.Index(i))));
    }
}
