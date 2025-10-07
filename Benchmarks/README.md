# Benchmarks

## Value iteration
Test all 512 possible states of `Values`.

| Method | Mean     |
|------- |---------:|
| Sum    | 1.209 us |

## Solvers
Tested on 1000 puzzles. Results per puzzle.

| Method       | Config     | Mean       | Ratio |
|------------- |----------- |-----------:|------:|
| Reference    | Diabolical | 2,464.0 us |  5.42 |
| Default      | Diabolical |   455.7 us |  1.00 |
| Backtracking | Diabolical |   632.5 us |  1.39 |
| Hidden       | Diabolical |   427.9 us |  0.94 |
| Pairs        | Diabolical |   670.8 us |  1.47 |
| Simple       | Diabolical |   488.6 us |  1.07 |
| All          | Diabolical |   575.3 us |  1.26 |
|              |            |            |       |
| Reference    | Hard       | 1,140.3 us |  8.93 |
| Default      | Hard       |   127.9 us |  1.00 |
| Backtracking | Hard       |   223.6 us |  1.75 |
| Hidden       | Hard       |   124.6 us |  0.98 |
| Pairs        | Hard       |   199.0 us |  1.56 |
| Simple       | Hard       |   161.7 us |  1.27 |
| All          | Hard       |   252.0 us |  1.97 |
|              |            |            |       |
| Reference    | Medium     | 1,150.4 us |  9.48 |
| Default      | Medium     |   121.4 us |  1.00 |
| Backtracking | Medium     |   222.7 us |  1.84 |
| Hidden       | Medium     |   120.4 us |  0.99 |
| Pairs        | Medium     |   203.5 us |  1.68 |
| Simple       | Medium     |   160.6 us |  1.32 |
| All          | Medium     |   255.3 us |  2.10 |
|              |            |            |       |
| Reference    | Easy       | 1,136.4 us |  9.28 |
| Default      | Easy       |   122.5 us |  1.00 |
| Backtracking | Easy       |   225.7 us |  1.84 |
| Hidden       | Easy       |   120.4 us |  0.98 |
| Pairs        | Easy       |   196.5 us |  1.61 |
| Simple       | Easy       |   162.0 us |  1.32 |
| All          | Easy       |   256.8 us |  2.10 |
