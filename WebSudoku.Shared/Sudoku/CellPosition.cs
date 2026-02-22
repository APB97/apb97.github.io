namespace apb97.github.io.WebSudoku.Shared.Sudoku;

[Serializable]
public readonly record struct CellPosition(int Row, int Column)
{
    public static implicit operator CellPosition((int row, int column) value)
    {
        return new CellPosition(value.row, value.column);
    }
}
