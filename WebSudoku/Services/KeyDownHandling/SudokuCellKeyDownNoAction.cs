using Microsoft.JSInterop;

namespace apb97.github.io.WebSudoku.Services.KeyDownHandling;

public class SudokuCellKeyDownNoAction : SudokuCellKeyDownBase
{
    public override Task OnKeyDown(IJSObjectReference sudokuModule, int row, int column)
    {
        return Task.CompletedTask;
    }
}
