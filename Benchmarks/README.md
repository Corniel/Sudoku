# Benchmarks

## Value iteration
Test all 512 possible states of `Values`.

| Method | Mean     |
|------- |---------:|
| Sum    | 1.209 us |

## Solvers
Tested on 1000 puzzles. Results per puzzle.

| Method       | Config     | Mean        | Ratio |
|------------- |----------- |------------:|------:|
| Reference    | Diabolical | 2,472.49 us | 23.31 |
| Dancing      | Diabolical |    82.86 us |  0.78 |
| Default      | Diabolical |   106.09 us |  1.00 |
| Backtracking | Diabolical |   129.26 us |  1.22 |
| Simple       | Diabolical |   177.52 us |  1.67 |
| All          | Diabolical |   379.71 us |  3.58 |
|              |            |             |       |
| Reference    | Hard       | 1,131.37 us | 23.38 |
| Dancing      | Hard       |    66.36 us |  1.37 |
| Default      | Hard       |    48.40 us |  1.00 |
| Backtracking | Hard       |    61.19 us |  1.26 |
| Simple       | Hard       |   117.80 us |  2.43 |
| All          | Hard       |   284.44 us |  5.88 |
|              |            |             |       |
| Reference    | Medium     | 1,149.13 us | 22.89 |
| Dancing      | Medium     |    64.24 us |  1.28 |
| Default      | Medium     |    50.23 us |  1.00 |
| Backtracking | Medium     |    60.43 us |  1.20 |
| Simple       | Medium     |   116.65 us |  2.32 |
| All          | Medium     |   290.64 us |  5.79 |
|              |            |             |       |
| Reference    | Easy       | 1,130.10 us | 22.65 |
| Dancing      | Easy       |    66.21 us |  1.33 |
| Default      | Easy       |    49.89 us |  1.00 |
| Backtracking | Easy       |    61.69 us |  1.24 |
| Simple       | Easy       |   120.04 us |  2.41 |
| All          | Easy       |   290.36 us |  5.82 |
