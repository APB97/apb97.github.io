using System.Diagnostics.CodeAnalysis;

namespace apb97.github.io.WebSudoku.Shared.General;

[ExcludeFromCodeCoverage]
public class RandomOptionOrder<T> : IOptionOrder<T>
{
    public IEnumerable<T> Order(IEnumerable<T> sequence)
    {
        return sequence.OrderBy(_ => Random.Shared.NextDouble());
    }
}
