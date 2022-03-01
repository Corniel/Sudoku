namespace Corniel.Sudoku
{
    /// <summary>Represents the structure of a Sudoku puzzle.</summary>
    public class SudokuPuzzle
    {
        public static readonly SudokuPuzzle Struture = new SudokuPuzzle();

        /// <summary>Represents a cell with no valid options.</summary>
        public const uint Invalid = 0;

        /// <summary>Represents a cell with value 1.</summary>
        public const uint Value1 = 0x001;
        /// <summary>Represents a cell with value 2.</summary>
        public const uint Value2 = 0x002;
        /// <summary>Represents a cell with value 3.</summary>
        public const uint Value3 = 0x004;
        /// <summary>Represents a cell with value 4.</summary>
        public const uint Value4 = 0x008;
        /// <summary>Represents a cell with value 5.</summary>
        public const uint Value5 = 0x010;
        /// <summary>Represents a cell with value 6.</summary>
        public const uint Value6 = 0x020;
        /// <summary>Represents a cell with value 7.</summary>
        public const uint Value7 = 0x040;
        /// <summary>Represents a cell with value 8.</summary>
        public const uint Value8 = 0x080;
        /// <summary>Represents a cell with value 9.</summary>
        public const uint Value9 = 0x100;

        /// <summary>Gets the unknown value.</summary>
        public const uint Unknown = 0x1FF;

        /// <summary>Gets the Singleton instance of 3x3 puzzle.</summary>
        private SudokuPuzzle()
        {
            const int size = 3;
            Size2 = size * size;

            Grid = new int[Size2, Size2];
            Regions = new SudokuRegions();
            Lookup = new SudokuRegions[Size2 * Size2];

            InitializeGrid(Size2);

            InitializeRowRegions(Size2);
            InitializeColumnRegions(Size2);
            InitializeSubSquareRegions(size);
            InitializeLookup();
            InitializeIntersected();
        }

        /// <summary>Constructs a Sudoku puzzle based on its size.</summary>
        /// <param name="size">
        /// The size of a single square.
        /// </param>
        /// <summary>Initializes a square with its indexes.</summary>
        private void InitializeGrid(int size2)
        {
            for (var x = 0; x < size2; x++)
            {
                for (var y = 0; y < size2; y++)
                {
                    Grid[x, y] = x + y * size2;
                }
            }
        }

        /// <summary>Initializes all row regions.</summary>
        private void InitializeRowRegions(int size2)
        {
            for (int row = 0; row < size2; row++)
            {
                var region = new SudokuRegion(SudokuRegionType.Row);
                Regions.Add(region);

                for (int width = 0; width < size2; width++)
                {
                    region.Add(Grid[row, width]);
                }
            }
        }

        /// <summary>Initializes all column regions.</summary>
        private void InitializeColumnRegions(int size2)
        {
            for (var column = 0; column < size2; column++)
            {
                var region = new SudokuRegion(SudokuRegionType.Column);
                Regions.Add(region);

                for (var height = 0; height < size2; height++)
                {
                    region.Add(Grid[height, column]);
                }
            }
        }

        /// <summary>Initializes all sub square regions.</summary>
        private void InitializeSubSquareRegions(int size)
        {
            for (var xSub = 0; xSub < size; xSub++)
            {
                for (var ySub = 0; ySub < size; ySub++)
                {
                    var region = new SudokuRegion(SudokuRegionType.Block);
                    Regions.Add(region);

                    for (var x = 0; x < size; x++)
                    {
                        for (var y = 0; y < size; y++)
                        {
                            region.Add(Grid[xSub * size + x, ySub * size + y]);
                        }
                    }
                }
            }
        }

        /// <summary>Initializes a lookup to map each square to its corresponding regions.</summary>
        private void InitializeLookup()
        {
            for (var index = 0; index < Lookup.Length; index++)
            {
                Lookup[index] = new SudokuRegions();
            }
            foreach (var region in Regions)
            {
                foreach (var square in region)
                {
                    Lookup[square].Add(region);
                }
            }
        }

        /// <summary>Initializes the intersected per region.</summary>
        private void InitializeIntersected()
        {
            foreach (var region in Regions)
            {
                region.Intersected.AddRange(Regions.Where(other => region.HasIntersectionOf2OrMoreSquares(other)));
            }
        }

        /// <summary>All (distinct) regions.</summary>
        public SudokuRegions Regions { get; private set; }

        /// <summary>The lookup to map each square to its corresponding regions.</summary>
        public SudokuRegions[] Lookup { get; private set; }

        /// <summary>The grid with the corresponding indexes.</summary>
        private int[,] Grid { get; set; }

        /// <summary>The highest index in the grid.</summary>
        public int MaximumIndex => Grid.Length - 1;

        /// <summary>The squared size of a single sub square.</summary>
        public int Size2 { get; }

        public uint GetSingleValue(int index) => SingleValues.Skip(index).FirstOrDefault();

        /// <summary>Gets the possible (single) values.</summary>
        public ICollection<uint> SingleValues => Mapping.Keys;

        /// <summary>Gets the mapping from value to String.</summary>
        public static readonly Dictionary<uint, string> Mapping = new Dictionary<uint, string>()
        {
            { Value1, "1" },
            { Value2, "2" },
            { Value3, "3" },
            { Value4, "4" },
            { Value5, "5" },
            { Value6, "6" },
            { Value7, "7" },
            { Value8, "8" },
            { Value9, "9" },
        };
    }
}
