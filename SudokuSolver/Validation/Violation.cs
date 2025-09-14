namespace SudokuSolver.Validation;

public sealed class Violation(int value, Candidates allowed, Pos cell, Constraint constraint, Restriction? restriction = null)
{
    public int Value { get; } = value;

    public Candidates Allowed { get; } = allowed;

    public Pos Cell { get; } = cell;

    public Constraint Constraint { get; } = constraint;

    public Restriction Restriction { get; } = restriction ?? new Peer();

    public override string ToString() => $"{Cell} = {Value}, Allowed = {Allowed}, Constraint = {Constraint}, Restrction = {Restriction}";

    private sealed class Peer : Restriction
    {
        public Pos AppliesTo => default;

        public Candidates Restrict(Cells cells) => Candidates.None;

        public override string ToString() => "Peers must have different values";
    }
}
