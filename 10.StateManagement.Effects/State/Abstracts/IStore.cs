namespace _10.StateManagement.Effects.State.Abstracts;

public interface IStore<out TState>
{
    TState State { get; }
    event Action? Changed;
}