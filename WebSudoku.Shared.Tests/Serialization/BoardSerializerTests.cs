using apb97.github.io.WebSudoku.Shared.Serialization;
using apb97.github.io.WebSudoku.Shared.Sudoku;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace apb97.github.io.WebSudoku.Shared.Tests.Serialization;

public class BoardSerializerTests
{
    [Theory]
    [InlineData(0, 0, 7)]
    [InlineData(0, 8, 5)]
    [InlineData(8, 0, 6)]
    [InlineData(8, 8, 8)]
    public void SerializeToJson_CanBeDeserializedFromJson_PreservingPredefinedCellState(int row, int column, int value)
    {
        var board = new Board();
        board.RedefineCell((row, column), value);

        var deserializedBoard = BoardSerializer.DeserializeFromJson(BoardSerializer.SerializeToJson(board));
        
        deserializedBoard.ShouldNotBeNull();
        deserializedBoard!.GetValueAt((row, column))
            .ShouldBe(value);
        deserializedBoard.IsPredefined((row, column))
            .ShouldBeTrue();
    }

    [Theory]
    [InlineData(0, 0, 7)]
    [InlineData(0, 8, 5)]
    [InlineData(8, 0, 6)]
    [InlineData(8, 8, 8)]
    public void SerializeToJson_CanBeDeserializedFromJson_PreservingFilledCellState(int row, int column, int value)
    {
        var board = new Board();
        board.FillCell((row, column), value);

        var deserializedBoard = BoardSerializer.DeserializeFromJson(BoardSerializer.SerializeToJson(board));
        
        deserializedBoard.ShouldNotBeNull();
        deserializedBoard!.GetValueAt((row, column))
            .ShouldBe(value);
        deserializedBoard.IsPredefined((row, column))
            .ShouldBeFalse();
    }

    [Fact]
    public void DeserializeFromJsonV2_GivenGarbageInput_ReturnsNullBoardAndTimer()
    {
        BoardSerializer.DeserializeFromJson("<Random garbage here>", out var timer)
            .ShouldBeNull();
        timer.HasValue
            .ShouldBeFalse();
    }

    [Theory]
    [MemberData(nameof(TestDataV2))]
    public void SerializeToJsonV2_CanBeDeserializedFromJsonV2_PreservingPredefinedCellStateWithTimer(int row, int column, int value, TimeSpan time)
    {
        var board = new Board();
        board.RedefineCell((row, column), value);

        var deserializedBoard = BoardSerializer.DeserializeFromJson(BoardSerializer.SerializeToJson(board, time, GameStateVersion.V2), out var timer);
        
        deserializedBoard.ShouldNotBeNull();
        deserializedBoard!.GetValueAt((row, column))
            .ShouldBe(value);
        deserializedBoard.IsPredefined((row, column))
            .ShouldBeTrue();
        timer.HasValue
            .ShouldBeTrue();
        timer!.Value
            .ShouldBe(time);
    }

    [Theory]
    [MemberData(nameof(TestDataV2))]
    public void SerializeToJsonV2_CanBeDeserializedFromJsonV2_PreservingFilledCellStateWithTimer(int row, int column, int value, TimeSpan time)
    {
        var board = new Board();
        board.FillCell((row, column), value);

        var deserializedBoard = BoardSerializer.DeserializeFromJson(BoardSerializer.SerializeToJson(board, time, GameStateVersion.V2), out var timer);
        
        deserializedBoard.ShouldNotBeNull();
        deserializedBoard!.GetValueAt((row, column))
            .ShouldBe(value);
        deserializedBoard.IsPredefined((row, column))
            .ShouldBeFalse();
        timer.HasValue
            .ShouldBeTrue();
        timer!.Value
            .ShouldBe(time);
    }

    [ExcludeFromCodeCoverage]
    public static TheoryData<int, int, int, TimeSpan> TestDataV2()
    {
        return new TheoryData<int, int, int, TimeSpan>()
        {
            { 0, 0, 7, TimeSpan.Zero },
            { 0, 8, 5, TimeSpan.FromSeconds(25) },
            { 8, 0, 6, TimeSpan.FromMinutes(2) },
            { 8, 8, 8, TimeSpan.FromMinutes(3) },
        };
    }
}
