# Benchmarks

## Value iteration
Test all 512 possible states of `Values`.

| Method | Mean     |
|------- |---------:|
| Sum    | 1.209 us |

## Solvers
Tested on 1000 hard puzzles. The mean is the average per puzzle.

| Method    | Config     | Mean       | Ratio |
|---------- |----------- |-----------:|------:|
| Reference | Diabolical | 2,441.8 us |  6.58 |
| Dynamic   | Diabolical |   371.1 us |  1.00 |
|           |            |            |       |
| Reference | Hard       | 1,402.5 us | 12.12 |
| Dynamic   | Hard       |   115.7 us |  1.00 |
|           |            |            |       |
| Reference | Medium     | 1,393.8 us | 12.08 |
| Dynamic   | Medium     |   115.5 us |  1.00 |
|           |            |            |       |
| Reference | Easy       | 1,390.5 us | 12.04 |
| Dynamic   | Easy       |   115.5 us |  1.00 |


## Cracking The Cryptic
| Puzzle                       | Mean     |
|----------------------------- |---------:|
| The Miracle Sudoku Of Eleven | 1.291 ms |
