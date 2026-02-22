using apb97.github.io.WebSudoku.Shared.Sudoku;
using Shouldly;

namespace apb97.github.io.WebSudoku.Shared.Tests.Sudoku
{
    public class NeighborsTests
    {
        private static readonly Neighbors neighbors = new();

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 8)]
        [InlineData(8, 0)]
        [InlineData(8, 8)]
        [InlineData(2, 1)]
        [InlineData(2, 4)]
        [InlineData(2, 7)]
        [InlineData(4, 1)]
        [InlineData(4, 4)]
        [InlineData(4, 7)]
        [InlineData(7, 1)]
        [InlineData(7, 4)]
        [InlineData(7, 7)]
        public void GivenCellPosition_HasConstantAmountOfNeighborsIncludingItself(int row, int column)
        {
            var cells = neighbors[row, column];

            cells.Count.ShouldBe(Board.BoardSize + 8 + 4,
                customMessage: "each cell is in one column, row, and 3x3 square and some cells appear in 2 exact postions of row, column or 3x3 square.");
        }
    }
}
