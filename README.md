# Sudoku Solver

My attempt to write a Sudoku solver.

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

### Rules
The `Rules` contain both the defined sets (such as [houses](#Houses)) and
[restrictions](#Restriction) that apply.
