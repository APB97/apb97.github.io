using apb97.github.io.WebSudoku.Shared.Sudoku;
using Microsoft.JSInterop;

namespace apb97.github.io.WebSudoku.Services.KeyDownHandling;

public class SudokuCellKeyDownEnter : SudokuCellKeyDownBase
{
    public override async Task OnKeyDown(IJSObjectReference sudokuModule, int row, int column)
    {
        await BlurAsync(sudokuModule, row, column);
        MoveToNextCell(ref row, ref column);
        await FocusAsync(sudokuModule, row, column);
    }

    private static void MoveToNextCell(ref int row, ref int column)
    {
        column++;
        if (column <= Board.BoardSize)
            return;
        WrapAroundToNextRow(ref row, ref column);
    }

    private static void WrapAroundToNextRow(ref int row, ref int column)
    {
        column -= Board.BoardSize;
        row++;
        if (row <= Board.BoardSize)
            return;
        row = WrapAroundToStart(row);
    }

    private static int WrapAroundToStart(int row)
    {
        return row - Board.BoardSize;
    }
}
