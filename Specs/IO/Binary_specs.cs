using Generator;
using Puzzles;
using Puzzles.NewYorkTimes;
using Puzzles.PuzzleBank;
using Sudoku.IO;
using System.Numerics;

namespace Specs.IO.Binary_specs;

public class Constants
{
    [Test]
    public void MaxPosis_7_5() => Binary.MaxPos.Should().Be(new Pos(7, 5));

    [Test]
    public void Max_index_fits_in_104_bit()
    {
        var maximums = new byte[_9x9];

        foreach(var pos in Pos.All)
        {
            var (row, col) = pos;
            // index within the box.
            var box = (col % 3) + ((row % 3) * 3);
            maximums[pos] = (byte)(9 - int.Max(int.Max(row, col), box));
        }
        Log.Cells(maximums);

        maximums.Should().BeEquivalentTo(new byte[]
        {
            9, 8, 7, 6, 5, 4, 3, 2, 1,
            6, 5, 4, 6, 5, 4, 3, 2, 1,
            3, 2, 1, 3, 2, 1, 3, 2, 1,
            6, 6, 6, 6, 5, 4, 3, 2, 1,
            5, 5, 4, 5, 5, 4, 3, 2, 1,
            3, 2, 1, 3, 2, 1, 3, 2, 1,
            3, 3, 3, 3, 3, 3, 3, 2, 1,
            2, 2, 2, 2, 2, 2, 2, 2, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1,
        });

        GetMax(maximums).GetBitLength().Should().Be(104);
    }

    [Explicit]
    [Test]
    public void Sampling_results_in_100_bit()
    {
        byte[] maximums = [.. range(_9x9).Select(_ => (byte)1)];

        IEnumerable<Puzzle> puzzles =
        [
            .. PuzzleBankPuzzle.Easy,
            .. PuzzleBankPuzzle.Medium,
            .. PuzzleBankPuzzle.Hard,
            .. PuzzleBankPuzzle.Diabolical,
            .. NewYorkTimesPuzzle.Hard,
        ];

        foreach (var puzzle in puzzles.Select(p => p.Solution))
        {
            var indexes = Binary.ToIndexes(puzzle);

            for (var pos = Pos.O; pos <= Binary.MaxPos; pos++)
                maximums[pos] = byte.Max(maximums[pos], (byte)(1 + indexes[pos]));
        }

        Log.Cells(maximums);

        maximums.Should().BeEquivalentTo(new byte[]
        {
            9, 8, 7, 6, 5, 4, 3, 2, 1,
            6, 5, 4, 6, 5, 4, 3, 2, 1,
            3, 2, 1, 3, 2, 1, 3, 2, 1,
            6, 6, 6, 6, 5, 4, 3, 2, 1,
            5, 5, 4, 5, 5, 4, 3, 2, 1,
            3, 2, 1, 3, 2, 1, 3, 2, 1,
            3, 3, 3, 3, 3, 3, 1, 1, 1,
            2, 2, 2, 2, 2, 2, 1, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1,
        });

        GetMax(maximums).GetBitLength().Should().Be(100);
    }

    private static BigInteger GetMax(IEnumerable<byte> factors)
    {
        var max = BigInteger.Zero;
        foreach (var factor in factors)
        {
            max *= factor;
            max += factor - 1;
        }
        return max;
    }
}

public class To_UIn128
{
    [Test]
    public void Compresses_cells()
    {
        var cells = Cells.Parse("""
            127│368│594
            493│752│186
            856│491│723
            ───┼───┼───
            571│936│842
            382│145│679
            964│287│351
            ───┼───┼───
            218│674│935
            749│513│268
            635│829│417
            """);

        var num = Binary.ToUInt128(cells);
        num.Should().Be(UInt128.Parse("5165370590714509078530500664"));
    }
}

public class To_Cells
{
    [Test]

    public void Decompresses_bits()
    {
        var num = UInt128.Parse("5165370590714509078530500664");
        var cells = Binary.TolCells(num);

        cells.Should().Be("""
            127│368│594
            493│752│186
            856│491│723
            ───┼───┼───
            571│936│842
            382│145│679
            964│287│351
            ───┼───┼───
            218│674│935
            749│513│268
            635│829│417
            """);
    }
}

public class Roundtrips
{
    [Test]
    public void without_issues()
    {
        foreach(var cells in new Grids(new()).Take(1000))
        {
            var num = Binary.ToUInt128(cells);
            var bac = Binary.TolCells(num);
            bac.Should().Be(cells);
        }
    }

    [Test]
    public void Puzzle()
    {
        var cells = Cells.Parse("""
            284│359│176
            315│627│894
            679│841│523
            ───┼───┼───
            857│294│631
            426│713│958
            931│586│742
            ───┼───┼───
            192│478│365
            568│932│417
            743│165│289
            """);

        var indexes = Binary.ToIndexes(cells);

        Pos.All.Should()
            .AllSatisfy(pos => indexes[pos]
            .Should()
            .BeLessThan(Binary.Factor[pos], $"For pos {pos}"));

        Log.Cells(indexes);
        Console.WriteLine();
        Log.Cells(Binary.Factor);


        var num = Binary.ToUInt128(cells);
        var bac = Binary.TolCells(num);
        bac.Should().Be(cells);
    }
}

file static class Log
{
    public static void Cells<T>(IReadOnlyList<T> square)
    {
        for (var row = 0; row < _9; row++)
        {
            for (var col = 0; col < _9; col++)
            {
                Console.Write(square[new Pos(row, col)]);
                Console.Write(", ");
            }
            Console.WriteLine();
        }
    }
}
