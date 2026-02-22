namespace apb97.github.io.WebSudoku.Shared.Sudoku;

public readonly record struct GameState(string Version, BoardState Board, TimeSpan? Timer) { }
