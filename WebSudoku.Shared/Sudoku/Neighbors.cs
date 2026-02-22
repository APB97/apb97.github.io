namespace apb97.github.io.WebSudoku.Shared.Sudoku;

public class Neighbors
{
    private readonly HashSet<CellPosition>[,] cellNeighbors;

    public IReadOnlySet<CellPosition> this[int row, int column]
    {
        get => cellNeighbors[row, column];
    }

    public Neighbors()
    {
        cellNeighbors = new HashSet<CellPosition>[Board.BoardSize, Board.BoardSize];

        InitializeCellNeighbors(CreateSquareCellsArray());
    }

    private void InitializeCellNeighbors(IEnumerable<CellPosition>[,] squareCells)
    {
        for (int row = 0; row < Board.BoardSize; row++)
        {
            for (int column = 0; column < Board.BoardSize; column++)
            {
                cellNeighbors[row, column] = new(WithinRow(row).Concat(WithinColumn(column)).Concat(squareCells[row / 3, column / 3]));
            }
        }
    }

    private static IEnumerable<CellPosition>[,] CreateSquareCellsArray()
    {
        var squareCells = new IEnumerable<CellPosition>[3, 3];
        for (int squareRow = 0; squareRow < 3; squareRow++)
        {
            for (int squareColumn = 0; squareColumn < 3; squareColumn++)
            {
                squareCells[squareRow, squareColumn] = WithinSquare(squareRow, squareColumn);
            }
        }

        return squareCells;
    }

    public static IEnumerable<CellPosition> WithinSquare(int row, int column)
    {
        return Enumerable.Range(row * 3, 3)
            .Join(Enumerable.Range(column * 3, 3), outer => 0, inner => 0, (r, c) => new CellPosition(r, c));
    }

    public static IEnumerable<CellPosition> WithinRow(int row)
    {
        return Enumerable.Range(0, Board.BoardSize).Select(column => new CellPosition(row, column));
    }

    public static IEnumerable<CellPosition> WithinColumn(int column)
    {
        return Enumerable.Range(0, Board.BoardSize).Select(row => new CellPosition(row, column));
    }
}
