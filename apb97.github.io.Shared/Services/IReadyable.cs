namespace apb97.github.io.Shared.Services;

public interface IReadyable
{
    bool IsReady { get; }
    Action OnReady { get; set; }
}
