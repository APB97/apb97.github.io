namespace apb97.github.io.WebSudoku.Shared.General;

public interface IOptionOrder<T>
{
    IEnumerable<T> Order(IEnumerable<T> sequence);
}