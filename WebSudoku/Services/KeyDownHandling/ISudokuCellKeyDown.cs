namespace apb97.github.io.WebSudoku.Services.KeyDownHandling
{
    public interface ISudokuCellKeyDown
    {
        Task OnKeyDown(Microsoft.JSInterop.IJSObjectReference sudokuModule, int row, int column);
    }
}