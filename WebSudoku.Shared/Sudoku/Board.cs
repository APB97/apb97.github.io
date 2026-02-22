using apb97.github.io.WebSudoku.Shared.General;

namespace apb97.github.io.WebSudoku.Shared.Sudoku;
public class Board
{
    private const string InvalidDimensions = "Invalid dimensions";
    public const int BoardSize = 9;
    private const int TotalCells = BoardSize * BoardSize;
    private readonly HashSet<CellPosition> emptyCells = [];

    public IReadOnlySet<CellPosition> EmptyCells => emptyCells;

    private readonly int[,] cells;
    private readonly bool[,] predefined;

    public Board()
    {
        cells = new int[BoardSize, BoardSize];
        predefined = new bool[BoardSize, BoardSize];
    }

    public Board(Solver solver, IOptionOrder<int> optionOrder)
    {
        cells = new int[BoardSize, BoardSize];
        predefined = new bool[BoardSize, BoardSize];

        cells = solver.Solve(cells, optionOrder, out _);
        MarkAllAsPredefined();
    }

    public Board(BoardState state)
    {
        ThrowIfInvalidDimensions(state.Cells, nameof(state.Cells));
        ThrowIfInvalidDimensions(state.Predefined, nameof(state.Predefined));

        cells = new int[BoardSize, BoardSize];
        predefined = new bool[BoardSize, BoardSize];

        for (int row = 0; row < BoardSize; row++)
        {
            for (int column = 0; column < BoardSize; column++)
            {
                var index = row * BoardSize + column;
                cells[row, column] = state.Cells[index];
                predefined[row, column] = state.Predefined[index];
            }
        }

        DetermineEmptyCells();
    }

    public static void ThrowIfInvalidDimensions(Array array, string paramName)
    {
        if (array.Rank != 1 || array.GetLength(0) != TotalCells) throw new ArgumentException(InvalidDimensions, paramName);
    }

    private void MarkAllAsPredefined()
    {
        for (int row = 0; row < BoardSize; row++)
        {
            for (int column = 0; column < BoardSize; column++)
            {
                predefined[row, column] = true;
            }
        }
    }

    public Board(Solver solver, IOptionOrder<int> optionOrder, Blanker blanker, int targetBlanks, int attemptsToRemove)
        : this(solver, optionOrder)
    {
        blanker.MakeBlanks(this, targetBlanks, attemptsToRemove);
        DetermineEmptyCells();
    }

    private void DetermineEmptyCells()
    {
        emptyCells.Clear();
        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (cells[row, col] == 0)
                {
                    emptyCells.Add((row, col));
                }
            }
        }
    }

    public int UndefineCell(CellPosition cellPosition)
    {
        var value = cells[cellPosition.Row, cellPosition.Column];

        cells[cellPosition.Row, cellPosition.Column] = 0;
        predefined[cellPosition.Row, cellPosition.Column] = false;

        return value;
    }

    public void RedefineCell(CellPosition cellPosition, int value)
    {
        cells[cellPosition.Row, cellPosition.Column] = value;
        predefined[cellPosition.Row, cellPosition.Column] = true;
    }

    public void ClearCell(CellPosition cellPosition)
    {
        cells[cellPosition.Row, cellPosition.Column] = 0;
        emptyCells.Add(cellPosition);
    }

    public void FillCell(CellPosition cellPosition, int value)
    {
        cells[cellPosition.Row, cellPosition.Column] = value;
        emptyCells.Remove(cellPosition);
    }

    public int GetValueAt(CellPosition cellPosition)
    {
        return cells[cellPosition.Row, cellPosition.Column];
    }

    public bool IsPredefined(CellPosition cellPosition)
    {
        return predefined[cellPosition.Row, cellPosition.Column];
    }

    public HashSet<CellPosition> GetInvalidCells(Validator validator)
    {
        var invalidCells = new HashSet<CellPosition>();

        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (!predefined[row, col] && !validator.IsValid(this, (row, col)))
                {
                    invalidCells.Add((row, col));
                }
            }
        }

        return invalidCells;
    }

    public bool HasOneAndOnlySolution(CountingSolver solver)
    {
        _ = solver.Solve(cells, new DefaultOptionOrder<int>(), out int solutionCount);
        return solutionCount == 1;
    }

    public BoardState GetState()
    {
        int[] cellValuesFlattened = new int[TotalCells];
        bool[] predefinedValuesFlattened = new bool[TotalCells];
        for (int row = 0; row < BoardSize; row++)
        {
            for (int column = 0; column < BoardSize; column++)
            {
                var index = row * BoardSize + column;
                cellValuesFlattened[index] = cells[row, column];
                predefinedValuesFlattened[index] = predefined[row, column];
            }
        }
        return new BoardState(cellValuesFlattened, predefinedValuesFlattened);
    }
}
