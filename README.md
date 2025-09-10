# Sudoku Solver

My attempt to write a [Sudoku](https://en.wikipedia.org/wiki/Sudoku) solver.

## Dynamic solver
The approach of my solver is that I specify both [clues](#Clues) and the
(potentially custom) [constraints](#Constraint) to apply when trying to solve
the puzzle.

## Standard Sudoku
To let the dynamic solver solve a standard Sudoku puzzle the following code
will do the trick:

``` csharp
var clues = Clues.Parse("""
    8..|...|...
    ..3|6..|...
    .7.|.9.|2..
    ---+---+---
    .5.|..7|...
    ...|.45|7..
    ...|1..|.3.
    ---+---+---
    ..1|...|.68
    ..8|5..|.1.
    .9.|...|4..
    """);

var solution = DynamicSolver.Solve(clues);
```

## Hyper Sudoku
Hyper Sudoku (also called Windoku) adds for extra 3x3 regions:

```
...|...|...
.11|1.2|22.
.11|1.2|22.
---+---+---
.11|1.2|22.
...|...|...
.33|3.4|44.
---+---+---
.33|3.4|44.
.33|3.4|44.
...|...|...
```

To let te dynamic solver solve these puzzles:

``` csharp
var clues = Clues.Parse("""
    .4.|...|..9
    9..|...|8..
    .1.|3..|...
    ---+---+---
    ...|4.2|..8
    ...|.3.|...
    ...|...|7.5
    ---+---+---
    ...|.9.|...
    .67|..4|...
    ...|..5|4..
    """);

var solution = DynamicSolver.Solve(clues, Rules.Hyper);
```

## Killer Sudoku
The dynamic solver can also solve [Killer Sudoku's](https://en.wikipedia.org/wiki/Killer_sudoku).
As there is no standard plain text format to describe these (that I'm aware of) there are
two support formats that seem logical:

``` csharp
var rules = KillerCages.Parse("""
    AAB|BBC|DEF
    GGH|HCC|DEF
    GGI|ICJ|KKF
    ---+---+---
    LMM|INJ|KOF
    LPP|QNJ|OOR
    SPT|QNU|VVR
    ---+---+---
    STT|QWU|UXX
    SYZ|WWa|aXX
    SYZ|Wbb|bcc

    A = 3   B = 15  C = 22  D = 4
    E = 16  F = 15  G = 25  H = 17
    I = 9   J = 8   K = 20  L = 6
    M = 14  N = 17  O = 17  P = 13
    Q = 20  R = 12  S = 27  T = 6
    U = 20  V = 6   W = 10  X = 14
    Y = 8   Z = 16  a = 15  b = 13  c = 17
""");

var rules_ = KillerCages.Parse("""
    27 = (0,0) + (0,1) + (1,0) + (2,0)
    13 = (0,2) + (1,1) + (1,2) + (2,1)
    15 = (0,3) + (1,3) + (2,3) + (3,3) + (4,3)
    28 = (2,2) + (3,0) + (3,1) + (3,2)
    17 = (4,0) + (5,0) + (4,1) + (4,2)
    17 = (5,1) + (5,2) + (5,3) + (5,4)
    20 = (6,0) + (6,1) + (6,2) + (7,2)
    25 = (7,0) + (7,1) + (8,0) + (8,1) + (8,2)
    33 = (5,5) + (6,5) + (6,4) + (6,3) + (7,3)
    16 = (0,4) + (1,4) + (1,5) + (1,6)
    16 = (0,5) + (0,6) + (0,7) + (0,8)
    27 = (1,7) + (1,8) + (2,7) + (2,8)
""");

var solution = DynamicSolver.Solve(Clues.Empty, rules);
```

## X Sudoku
To let the dynamic solver solve an X Sudoku puzzle:

``` csharp
var clues = Clues.Parse("""
    .1.|2.3|.4.
    8..|...|6.5
    .7.|...|...
    ---+---+---
    4..|...|..6
    ...|...|...
    2..|...|..7
    ---+---+---
    ...|...|.9.
    7.9|...|..8
    .2.|3.4|.5.
    """);

var solution = DynamicSolver.Solve(clues, Rules.XSudoku);
```

## Cracking The Cryptic
[Cracking Tye Cryptic](https://www.youtube.com/@CrackingTheCryptic) is a YouTube
channel dedicated to solving world-class puzzles (their wording, not mine). With
the extra [constraints](#Constraint) implemented, the Dynamic solver has been
able to solve the following puzzles (so far):

* [2025-05-21: Stepped Themos](Puzzles/CrackingTheCryptic/2025_05_21.cs)
* [2025-08-21: Miracle Of Eleven](Puzzles/CrackingTheCryptic/2025_08_21.cs)

## Models

## Candidates
The `Candidates` contain all possible values for a specified [cell](#Cell).
The underlying `uint` ranges from `0` (no options) to `0b_111_111_111_0` when
all 9 digits are candidate values. A single candidate flag is calculated by
`1 << candidate`, hence the zero-th bit will allways be zero. Using bit
operators (such as `&`, `|`, `^`, and `~`) it allows manipulation of the
candidates.

### Cell
The `Cell` contains the [position](#Pos) and the value of the cell. The `0` value
indicates that the value of the cell is not known.

### Cells
The `Cells` contain all [cell](#Cell)s with their values. It is a wrapper for
an `array`, and the values of cells can be changed.

### Clues
The `Clues` contain all given [cells](#Cell) for a puzzle.

## Constraint
The `Constraint` specfies the involved [positions](#PosSet) and the
[restrictions](#Restriction) per involved position.

### Houses
The `Houses` contain al houses as [sets](#PosSet) that are commonly used: rows,
columns, 3x3 boxes, and diagonals.

### Position
The `Pos` is an index based value type that can be deconstructed in a row and
column component. Its `ToString()` value also does this to help while debugging.

### PosSet
The `PosSet` is a a set of [positions](#Position) that uses bitmask
manipulation (similar to [Candidates](#Candidates)). It's iterator is fast too,
but iterating an `ImmutableArray<Pos>` is even faster, so while solving, the
latter is preferred.

### Restriction
The `Restricton` is defined on a [cell](#Cell), with a referenced to other
involved cells. It is able, based on a given state of [cells](#Cells), to
return a (restricted) set of [candidates](#Candidates).

