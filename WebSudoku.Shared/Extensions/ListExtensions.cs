namespace apb97.github.io.WebSudoku.Shared.Extensions;

public static class ListExtensions
{
    public static T PopRandomElement<T>(this List<T> list, Random? random = null)
    {
        if (list.Count == 0) throw new ArgumentException("List cannot be empty.", nameof(list));
        random ??= Random.Shared;

        var index = random.Next(list.Count);
        var selectedElement = list[index];
        list.RemoveAt(index);
        return selectedElement;
    }
}
