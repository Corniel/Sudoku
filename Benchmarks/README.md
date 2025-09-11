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
| Reference | Diabolical | 2,472.79 us | 25.19 |
| Dynamic   | Diabolical |    98.18 us |  1.00 |
|           |            |             |       |
| Reference | Easy       | 1,130.01 us | 23.34 |
| Dynamic   | Easy       |    48.44 us |  1.00 |
|           |            |             |       |
| Reference | Hard       | 1,142.05 us | 23.40 |
| Dynamic   | Hard       |    48.82 us |  1.00 |
|           |            |             |       |
| Reference | Medium     | 1,135.90 us | 23.83 |
| Dynamic   | Medium     |    47.68 us |  1.00 |
| 

## Cracking The Cryptic
| Puzzle            | Mean       |
|------------------ |-----------:|
| Stepped Themos    | 208.772 ms |
| Miracle Of Eleven |   7.393 ms |
