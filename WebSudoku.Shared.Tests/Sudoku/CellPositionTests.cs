using apb97.github.io.WebSudoku.Shared.Sudoku;
using Shouldly;

namespace apb97.github.io.WebSudoku.Shared.Tests.Sudoku;

public class CellPositionTests
{
    [Fact]
    public void InitAllowsAssignment()
    {
        var initedCell = new CellPosition() { Column = 1, Row = 2 };

        initedCell.Column
            .ShouldBe(1);

        initedCell.Row
            .ShouldBe(2);
    }
}
