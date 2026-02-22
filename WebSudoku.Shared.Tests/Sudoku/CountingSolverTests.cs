using apb97.github.io.WebSudoku.Shared.Sudoku;
using Shouldly;

namespace apb97.github.io.WebSudoku.Shared.Tests.Sudoku;

public class CountingSolverTests
{
    private static readonly Neighbors neighbors;
    private static readonly Solver solver;

    static CountingSolverTests()
    {
        neighbors = new Neighbors();
        solver = new Solver(neighbors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public void GivenMaxSolutionCountOf1_CountingSolverActsLikeSolver(int solutions)
    {
        var countingSolver = new CountingSolver(neighbors) { MaxSolutionCount = 1 };

        countingSolver.ShouldStopAtSolution(solutions)
            .ShouldBe(solver.ShouldStopAtSolution(solutions));
    }

    [Theory]
    [InlineData(1)]
    public void GivenMaxSolutionCountOf2_CountingSolverActsUnlikeSolver(int solutions)
    {
        var countingSolver = new CountingSolver(neighbors) { MaxSolutionCount = 2 };

        countingSolver.ShouldStopAtSolution(solutions)
            .ShouldNotBe(solver.ShouldStopAtSolution(solutions));
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public void GivenMaxSolutionCountOf2_CountingSolverActsLikeSolverInRemainingCases(int solutions)
    {
        var countingSolver = new CountingSolver(neighbors) { MaxSolutionCount = 1 };

        countingSolver.ShouldStopAtSolution(solutions)
            .ShouldBe(solver.ShouldStopAtSolution(solutions));
    }
}
