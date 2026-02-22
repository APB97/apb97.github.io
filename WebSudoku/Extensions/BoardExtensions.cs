using apb97.github.io.WebSudoku.Shared.Sudoku;

namespace apb97.github.io.WebSudoku.Extensions;

public static class BoardExtensions
{
    public static string GetValueAsString(this Board board, CellPosition cell)
    {
        var cellValue = board.GetValueAt(cell);
        return cellValue != 0 ? cellValue.ToString() : string.Empty;
    }
}
