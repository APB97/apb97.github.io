using apb97.github.io.WebSudoku.Shared.General;
using apb97.github.io.WebSudoku.Shared.Sudoku;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace apb97.github.io.WebSudoku.Shared.Tests.Sudoku;

public class ValidatorTests
{
    private const int MinimumAttemptsToRemove = 1;
    private const int LowValueOfTargetBlanks = 20;
    private static readonly Validator validator;
    private static readonly Neighbors neighbors;

    static ValidatorTests()
    {
        neighbors = new Neighbors();
        validator = new Validator(neighbors);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 8)]
    [InlineData(8, 0)]
    [InlineData(8, 8)]
    [InlineData(5, 5)]
    public void GivenEmptyBoard_DefaultEmptyCellValue_IsValid(int row, int column)
    {
        validator.IsValid(new Board(), (row, column))
            .ShouldBeTrue();
    }

    [Theory]
    [InlineData(0, 0, 1)]
    [InlineData(0, 0, 5)]
    [InlineData(0, 0, 9)]
    public void GivenEmptyBoard_EnteredSingleValue_IsValid(int row, int column, int value)
    {
        Board emptyboard = new();
        emptyboard.FillCell((row, column), value);
        
        validator.IsValid(emptyboard, (row, column))
            .ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(GivenBoardWithNearlyFilledColumn_OnlyRemainingValue_IsValid_Data))]
    public void GivenBoardWithNearlyFilledColumn_OnlyRemainingValue_IsValid(int row, int column, int value, int[] valuesInColummn, bool expectedValue)
    {
        Board board = new();
        int valueIndex = 0;
        foreach (var currentRow in Enumerable.Range(0, 9).Where(r => r != row))
        {
            board.FillCell((currentRow, column), valuesInColummn[valueIndex++]);
        }

        board.FillCell((row, column), value);

        validator.IsValid(board, (row, column))
            .ShouldBe(expectedValue);
    }

    [Fact]
    public void GivenEmptyBoard_IsValidBoard()
    {
        Board board = new();

        validator.IsValidBoard(board)
            .ShouldBeTrue();
    }

    [Fact]
    public void GivenDefaultOptionOrderSolvedBoard_IsValidBoard()
    {
        Board board = new(new Solver(neighbors), new DefaultOptionOrder<int>());

        validator.IsValidBoard(board)
            .ShouldBeTrue();
    }

    [Fact]
    public void GivenReverseOptionOrderSolvedBoard_IsValidBoard()
    {
        Board board = new(new Solver(neighbors), new ReverseOptionOrder<int>());

        validator.IsValidBoard(board)
            .ShouldBeTrue();
    }

    [Fact]
    public void GivenBlankedSolvedBoard_IsValidBoard()
    {
        CountingSolver solver = new(neighbors);
        Board board = new(solver, new DefaultOptionOrder<int>(), new Blanker(solver), LowValueOfTargetBlanks, MinimumAttemptsToRemove);

        validator.IsValidBoard(board)
            .ShouldBeTrue();
    }

    [Fact]
    public void GivenBlankedSolvedBoard_FilledWithSameDigit_IsNotValidBoard()
    {
        CountingSolver solver = new(neighbors);
        Board board = new(solver, new DefaultOptionOrder<int>(), new Blanker(solver), LowValueOfTargetBlanks, MinimumAttemptsToRemove);

        foreach (var emptyCell in board.EmptyCells)
        {
            board.FillCell(emptyCell, 7);
        }

        validator.IsValidBoard(board)
            .ShouldBeFalse();
    }

    [ExcludeFromCodeCoverage]
    public static TheoryData<int, int, int, int[], bool> GivenBoardWithNearlyFilledColumn_OnlyRemainingValue_IsValid_Data()
    {
        return new TheoryData<int, int, int, int[], bool>
        {
            { 0, 0, 1, Enumerable.Range(1, 9).Where(v => v != 1).ToArray(), true },
            { 0, 0, 5, Enumerable.Range(1, 9).Where(v => v != 5).ToArray(), true },
            { 0, 0, 9, Enumerable.Range(1, 9).Where(v => v != 9).ToArray(), true },
            { 0, 0, 1, Enumerable.Range(1, 9).Where(v => v != 2).ToArray(), false },
            { 0, 0, 5, Enumerable.Range(1, 9).Where(v => v != 4).ToArray(), false },
            { 0, 0, 9, Enumerable.Range(1, 9).Where(v => v != 7).ToArray(), false },
            { 5, 5, 6, Enumerable.Range(1, 9).Where(v => v != 5).ToArray(), false },
            { 8, 8, 7, Enumerable.Range(1, 9).Where(v => v != 6).ToArray(), false }
        };
    }
}
