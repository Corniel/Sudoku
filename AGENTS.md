# AGENTS.md

## Project

Sudoku solver that supports many Sudoku variants (standard, Killer, Jigsaw, X,
Hyper, Anti-Knight, and many more) by combining clues with an extensible
`RuleSet`. See `README.md` for an overview, supported variants, and benchmark
results.

## Stack

- .NET 10 (`net10.0`), C# 14
- Solution: `sudoku.slnx` (root-level `.net.csproj` is for IDE tooling)
- Tests: NUnit 4 + AwesomeAssertions (in `Specs/`)
- Benchmarks: BenchmarkDotNet (in `Benchmark/`)
- Central package management via `Directory.Packages.props`; keep
  `packages.lock.json` files in sync
- Analyzers enforced at build (`EnforceCodeStyleInBuild=true`): StyleCop,
  SonarAnalyzer, Qowaiv, DotNetProjectFile. Code must build warning-free.

## Layout

| Directory          | Purpose |
|--------------------|---------|
| `Sudoku/`          | Core library: models (`Cell`, `Cells`, `Clues`, `Digits`, `Pos`, `PosSet`), rules (`Constraints/`, `Restrictions/`, `Sets/`), `Common/` rule builders (thermos, whispers, arrows, cages, ...) |
| `Puzzles/`         | Puzzle definitions: `CrackingTheCryptic/`, `NewYorkTimes/`, `Killer/`, `Kaggle/`, `PuzzleBank/`, `SudokuPad/` |
| `Sudoku.App/`      | Executable app |
| `Specs/`           | NUnit test specs |
| `Benchmark/`       | BenchmarkDotNet benchmarks |
| `Dlx/`, `DynamicSolver/`, `Reference/`, `StrategyBased/`, `DancingLinks/` | Solver implementations |
| `Generator/`       | Puzzle generator |

## Commands

```powershell
dotnet build                      # build the solution (warnings are errors in practice)
dotnet test                       # run all specs
dotnet test --project Specs       # run specs explicitly
dotnet run --project Sudoku.App   # run the app
dotnet build --no-restore         # fast rebuild after a restore
```

Before finishing a task, run `dotnet build` and `dotnet test` and make sure both
pass cleanly.

## Conventions

- Follow `.editorconfig`: CRLF line endings, UTF-8, 4-space indentation for C#,
  2-space for `.csproj`/`.props`.
- No comments unless they add value; use the existing sparse style.
- File-scoped namespaces (`namespace Sudoku;`).
- Prefer expression-bodied members where `.editorconfig` allows.
- Test files are named `<Subject>_specs.cs` with `namespace Specs.<Subject>_specs;`.
- Assertions use AwesomeAssertions (`x.Should().Be(...)`), not NUnit asserts.
- Use the `TestSolver.Solve(...)` helper (`Specs/TestSolver.cs`) and the
  `SolverType` enum to pick a solver in tests. The default is `Dynamic`.
- Per-puzzle spec timing: `[Test, Explicit]` for slow puzzles; fast puzzles run
  by default in `Specs/Puzzles_specs.cs`.
- Raw puzzle text blocks use C# raw string literals (`"""`) in the `Cells` /
  `Clues` / `RuleSet` grid format (`─` box separators, `.` for empty cells).
- Do not hardcode package versions in `.csproj`; add them to
  `Directory.Packages.props` and reference by name only.

## Adding a Cracking The Cryptic puzzle

1. Add `Puzzles/CrackingTheCryptic/YYYY/YYYY-MM-DD.cs`:
   - Class `_YYYY_MM_DD` inheriting `Puzzles.CrackingTheCryptic.CtcPuzzle`.
   - `Title` (the puzzle name), `Author`, `Url` (YouTube link).
   - `Duration` as an order of magnitude using the `O` enum (`O.ms`,
     `O.s`, ...) so the puzzle is picked up by the fast/slow spec buckets.
   - `Solution` via `Cells.New(...)`.
   - Override `GetConstraints()` to return the `RuleSet` (see
     `Sudoku/Common/` and `Puzzles/CrackingTheCryptic/2026/2026-07-15.cs` for
     examples).
2. The puzzle is auto-discovered via `CtcPuzzle.All` reflection; no registration
   needed.
3. Update the table in `README.md` with the puzzle and its measured speed.

## Adding a rule / variant

- Variants are composed of rules: `Constraint` (validity predicate),
  `Restriction` (allowed digits per cell), and `Set` (houses that must be
  unique). Look at existing examples in `Sudoku/` before adding new ones.
- Add factory helpers on `RuleSet` (see `RuleSet.Consts.cs` and `RulesExtender.cs`).
- Add a spec in `Specs/` proving the rule solves a known puzzle (include the
  expected solution grid), and add a `RuleSet` sample to `README.md`.

## Notes

- Git history uses conventional-ish messages like
  `Solved 2026-07-15: Cataract` for puzzle additions. Match the surrounding
  commit style.
- Only commit when the user explicitly asks.
