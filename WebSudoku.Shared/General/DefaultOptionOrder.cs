namespace apb97.github.io.WebSudoku.Shared.General;

public class DefaultOptionOrder<T> : IOptionOrder<T>
{
    public IEnumerable<T> Order(IEnumerable<T> sequence)
    {
        return sequence;
    }
}
