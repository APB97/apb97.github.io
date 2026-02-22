namespace apb97.github.io.WebSudoku.Shared.General;

public class ReverseOptionOrder<T> : IOptionOrder<T>
{
    public IEnumerable<T> Order(IEnumerable<T> sequence)
    {
        return sequence.Reverse();
    }
}
