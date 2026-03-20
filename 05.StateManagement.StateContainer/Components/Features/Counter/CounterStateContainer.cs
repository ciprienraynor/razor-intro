namespace _05.StateManagement.StateContainer.Components.Features.Counter;

public record CounterState(int Count);

public class CounterStateContainer
{
    public CounterState State { get; private set; } = new(0);

    public event Action? Changed;

    public void Increment()
    {
        State = State with { Count = State.Count + 1 };
        Changed?.Invoke();
    }

    public void Reset()
    {
        State = State with { Count = 0 };
        Changed?.Invoke();
    }
}