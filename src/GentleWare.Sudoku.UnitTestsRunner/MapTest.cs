using System;

namespace GentleWare.Sudoku.UnitTests
{
    public class MapTest
    {
        public void Ctor_None_AreEqual()
        {
            var map = new Map();

            Assert.AreEqual(@"
.........
.........
.........
.........
.........
.........
.........
.........
.........", map.ToString());
        }

        public void Solve_3Number00_AreEqual()
        {
            var map = new Map();
            map.SetFixed(@"
39...2..6
.5..86...
2.......3
.3.7.....
..1.6.8..
4.......7
...43..5.
8..6...32");

            var act = map.Solve();


            Assert.AreEqual(@"
.........
.........
.........
.........
.........
.........
.........
.........
.........", map.ToString());
            Assert.AreEqual(1, act);
        }

        public void Solve_3Number01_AreEqual()
        {
            var map = new Map();
            map.SetFixed(@"
...317.69
16..92..3
..9......
41.7....8
5.39.46.7
7....8.32
......3..
9..83..46
63.549...");

            var act = map.Solve();

            Assert.AreEqual(@"
254317869
168492573
379685124
412763958
583924617
796158432
841276395
925831746
637549281", map.ToString());
            Assert.AreEqual(SolveResult.Single, act);
        }

        public void Solve_3Number02_AreEqual()
        {
            var map = new Map();
            map.SetFixed(@"
4..3.5..8
.........
.29...34.
.3.796.8.
2.6...9.7
.9.....5.
..2...8..
.4..1..2.
85.962.13");

            var act = map.Solve();

            Assert.AreEqual(@"
254317869
168492573
379685124
412763958
583924617
796158432
841276395
925831746
637549281", map.ToString());
            Assert.AreEqual(SolveResult.Single, act);
        }
        
        public void Solve_2Number00_AreEqual()
        {
            var map = new Map(2);
            map.SetFixed(@"
2...
..1.
.2..
...4");

            var act = map.Solve();

            Assert.AreEqual(@"
2143
3412
4231
1324", map.ToString());
            Assert.AreEqual(SolveResult.Single, act);
        }

        public void Solve_2Number1_IsInvalid()
        {
            var map = new Map(2);
            map.SetFixed(@"
22..
..1.
.2..
...4");

            var act = map.Solve();

            Assert.AreEqual(@"
22..
..1.
.2..
...4", map.ToString());
            Assert.AreEqual(SolveResult.Invalid, act);
        }
    }
}
