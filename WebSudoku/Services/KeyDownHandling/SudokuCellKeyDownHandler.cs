using apb97.github.io.WebSudoku.Shared.Keys;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace apb97.github.io.WebSudoku.Services.KeyDownHandling
{
    public class SudokuCellKeyDownHandler(IJSObjectReference sudokuModule, Func<KeyboardEventArgs, int, int, Task> defaultAction)
    {
        private readonly Dictionary<string, ISudokuCellKeyDown> actions = new()
        {
            { KeyCodes.Enter, new SudokuCellKeyDownEnter() },
            { KeyCodes.CapsLock, new SudokuCellKeyDownNoAction() },
            { KeyCodes.Tab, new SudokuCellKeyDownNoAction() },
            { KeyCodes.ArrowLeft, new SudokuCellKeyDownArrowKeyLeft() },
            { KeyCodes.ArrowRight, new SudokuCellKeyDownArrowKeyRight() },
            { KeyCodes.ArrowUp, new SudokuCellKeyDownArrowKeyUp() },
            { KeyCodes.ArrowDown, new SudokuCellKeyDownArrowKeyDown() },
        };

        public Task Handle(KeyboardEventArgs args, int row, int column)
        {
            if (actions.TryGetValue(args.Code, out var action))
                return action.OnKeyDown(sudokuModule, row, column);

            return defaultAction(args, row, column);
        }
    }
}
