namespace _11.StateManagement.SharedState.Core.BlazorKit;

public interface IStore<out TState>
{
    TState State { get; }
    event Action? Changed;
}