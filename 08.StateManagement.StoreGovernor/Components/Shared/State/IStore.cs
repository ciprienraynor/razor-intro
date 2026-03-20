namespace _08.StateManagement.StoreGovernor.Components.Shared.State;

public interface IStore<out TState>
{
    TState State { get; }
    event Action? Changed;
}