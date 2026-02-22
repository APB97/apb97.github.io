namespace apb97.github.io.WebSudoku.Shared.Sudoku;

public class CountingSolver(Neighbors neighbors) : Solver(neighbors)
{
    public int MaxSolutionCount { get; set; } = 2;

    public override bool ShouldStopAtSolution(int currentCount) => currentCount >= MaxSolutionCount;
}
