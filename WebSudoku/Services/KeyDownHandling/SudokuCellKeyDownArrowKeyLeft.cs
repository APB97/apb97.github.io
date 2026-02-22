using Microsoft.JSInterop;

namespace apb97.github.io.WebSudoku.Services.KeyDownHandling;

public class SudokuCellKeyDownArrowKeyLeft : SudokuCellKeyDownArrowKey
{
    public override async Task OnKeyDown(IJSObjectReference sudokuModule, int row, int column)
    {
        await BlurAsync(sudokuModule, row, column);
        await FocusAsync(sudokuModule, row, WrapAroundBetweenEdges(column, -1));
    }
}
