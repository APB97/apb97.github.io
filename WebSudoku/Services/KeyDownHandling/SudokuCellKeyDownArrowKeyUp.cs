using Microsoft.JSInterop;

namespace apb97.github.io.WebSudoku.Services.KeyDownHandling;

public class SudokuCellKeyDownArrowKeyUp : SudokuCellKeyDownArrowKey
{
    public override async Task OnKeyDown(IJSObjectReference sudokuModule, int row, int column)
    {
        await BlurAsync(sudokuModule, row, column);
        await FocusAsync(sudokuModule, WrapAroundBetweenEdges(row, -1), column);
    }
}
