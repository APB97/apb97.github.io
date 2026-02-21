namespace apb97.github.io.Services
{
    public interface ILayoutStateChangeHandler
    {
        LayoutMode LayoutMode { get; }
        Task ToggleLayoutMode();
        event Func<Task> OnStateChanging;
        Task NotifyStateChanged();
    }
}
