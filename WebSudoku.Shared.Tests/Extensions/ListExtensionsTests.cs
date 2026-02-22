using apb97.github.io.WebSudoku.Shared.Extensions;
using Shouldly;

namespace apb97.github.io.WebSudoku.Shared.Tests.Extensions;

public class ListExtensionsTests
{
    [Fact]
    public void PopRandomElement_GivenEmptyList_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ListExtensions.PopRandomElement<int>([]));
    }

    [Theory]
    [InlineData(3)]
    [InlineData(55)]
    [InlineData(false)]
    [InlineData(true)]
    [InlineData(null)]
    public void PopRandomElement_GivenOneElementList_PopsTheOnlyElement(object? element)
    {
        List<object?> list = [element];

        ListExtensions.PopRandomElement(list)
            .ShouldBe(element);

        list.ShouldNotContain(element);
        list.ShouldBeEmpty();
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(57 ,4)]
    [InlineData(false, 5)]
    [InlineData(true, 6)]
    [InlineData(null, 9)]
    public void PopRandomElement_GivenSameValueElementList_PopsOnlyOneElement(object? element, int count)
    {
        var list = Enumerable.Repeat(element, count).ToList();

        ListExtensions.PopRandomElement(list)
            .ShouldBe(element);

        list.Count
            .ShouldBe(count - 1);
        list.ShouldContain(element);
    }
}
