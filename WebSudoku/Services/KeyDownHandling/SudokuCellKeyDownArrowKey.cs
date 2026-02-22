using apb97.github.io.WebSudoku.Shared.Sudoku;

namespace apb97.github.io.WebSudoku.Services.KeyDownHandling;

public abstract class SudokuCellKeyDownArrowKey : SudokuCellKeyDownBase
{
    public static int WrapAroundBetweenEdges(int positionOnAxis, int change)
    {
        if (positionOnAxis < 1 || positionOnAxis > 9) throw new ArgumentOutOfRangeException(nameof(positionOnAxis));
        if (Math.Abs(change) > 1) throw new ArgumentOutOfRangeException(nameof(change));

        // Replaces previously used ternary operators with calculation using modulo.
        // Note that adding Board.BoardSize before using modulo allows wrapping around position from 1 to 9
        return (Board.BoardSize + positionOnAxis - 1 + change) % Board.BoardSize + 1;
    }
}
