using apb97.github.io.WebSudoku.Shared.General;

namespace apb97.github.io.WebSudoku.Shared.Sudoku;

public class Solver(Neighbors neighbors)
{
    public virtual bool ShouldStopAtSolution(int currentCount) => true;

    public int[,] Solve(int[,] board, IOptionOrder<int> optionOrder, out int solutionsFound)
    {
        int[,] solvedBoard = new int[9, 9];
        Array.Copy(board, solvedBoard, 81);

        LinkedList<CellPosition> emptyCells = MakeListOfEmptyPositions(solvedBoard);
        solutionsFound = 0;

        if (emptyCells.Count != 0)
        {
            Fill(solvedBoard, emptyCells, optionOrder, ref solutionsFound);
        }
        return solvedBoard;
    }

    private static LinkedList<CellPosition> MakeListOfEmptyPositions(int[,] solvedBoard)
    {
        var emptyCells = new LinkedList<CellPosition>();
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                if (solvedBoard[i, j] == 0)
                    emptyCells.AddLast((i, j));
        return emptyCells;
    }

    private bool Fill(int[,] board, LinkedList<CellPosition> emptyCells, IOptionOrder<int> optionOrder, ref int solutionsFound)
    {
        var cell = emptyCells.First;
        emptyCells.RemoveFirst();

        if (cell is not null)
        {
            var cellNeighbors = neighbors[cell.Value.Row, cell.Value.Column];
            var usedValues = new HashSet<int>(cellNeighbors.Select(position => board[position.Row, position.Column]));

            IEnumerable<int> availableValues = Enumerable.Range(1, 9).Except(usedValues);
            availableValues = optionOrder.Order(availableValues);

            foreach (int option in availableValues)
            {
                board[cell.Value.Row, cell.Value.Column] = option;
                if (emptyCells.Count == 0)
                {
                    solutionsFound++;
                    if (ShouldStopAtSolution(solutionsFound))
                    {
                        return true;
                    }

                    AddAsFirstEmptyCellAtPosition(cell.Value, board, emptyCells);
                    return false;
                }

                if (Fill(board, emptyCells, optionOrder, ref solutionsFound))
                {
                    return true;
                }
            }

            AddAsFirstEmptyCellAtPosition(cell.Value, board, emptyCells);
        }
        return false;
    }

    private static void AddAsFirstEmptyCellAtPosition(CellPosition cell, int[,] board, LinkedList<CellPosition> emptyCells)
    {
        board[cell.Row, cell.Column] = 0;
        emptyCells.AddFirst(cell);
    }
}
