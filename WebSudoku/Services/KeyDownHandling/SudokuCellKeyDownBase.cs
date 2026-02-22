using Microsoft.JSInterop;

namespace apb97.github.io.WebSudoku.Services.KeyDownHandling;

public abstract class SudokuCellKeyDownBase : ISudokuCellKeyDown
{
    public abstract Task OnKeyDown(IJSObjectReference sudokuModule, int row, int column);

    protected static ValueTask FocusAsync(IJSObjectReference sudokuModule, int row, int column)
    {
        return sudokuModule.InvokeVoidAsync("focusCell", row, column);
    }

    protected static ValueTask BlurAsync(IJSObjectReference sudokuModule, int row, int column)
    {
        return sudokuModule.InvokeVoidAsync("blurCell", row, column);
    }
}