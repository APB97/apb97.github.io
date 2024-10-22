namespace apb97.github.io.Services;

public interface IReadyable
{
    bool IsReady { get; }
    Action OnReady { get; set; }
}
