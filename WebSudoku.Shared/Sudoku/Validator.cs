namespace apb97.github.io.WebSudoku.Shared.Sudoku;

public class Validator(Neighbors neighbors)
{
    public bool IsValidBoard(Board board)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                if (!IsValid(board, (row, column)))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsValid(Board board, CellPosition position)
    {
        var (row, column) = position;
        int value = board.GetValueAt(position);
        if (value == 0)
        {
            return true;
        }

        return neighbors[row, column].Count(cell => board.GetValueAt(cell) == value) == 1;
    }
}
