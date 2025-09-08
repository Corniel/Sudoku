# Benchmarks

## Value iteration
Test all 512 possible states of `Values`.

| Method | Mean     |
|------- |---------:|
| Sum    | 1.209 us |

## Solvers
Tested on 1000 hard puzzles. The mean is the average per puzzle.

| Method    | Config     | Mean        | Ratio |
|---------- |----------- |------------:|------:|
| Reference | Diabolical | 2,479.82 us | 25.06 |
| Dynamic   | Diabolical |    98.98 us |  1.00 |
|           |            |             |       |
| Reference | Hard       | 1,440.78 us | 30.03 |
| Dynamic   | Hard       |    47.98 us |  1.00 |
|           |            |             |       |
| Reference | Medium     | 1,416.96 us | 28.47 |
| Dynamic   | Medium     |    49.79 us |  1.00 |
|           |            |             |       |
| Reference | Easy       | 1,422.07 us | 29.44 |
| Dynamic   | Easy       |    48.32 us |  1.00 |
| 

## Cracking The Cryptic
| Puzzle                       | Mean     |
|----------------------------- |---------:|
| The Miracle Sudoku Of Eleven | 6.102 ms |
