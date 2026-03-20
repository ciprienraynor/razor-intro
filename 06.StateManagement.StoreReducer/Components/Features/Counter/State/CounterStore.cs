namespace _06.StateManagement.StoreReducer.Components.Features.Counter.State;

public class CounterStore
{
    public CounterState State { get; private set; } = CounterState.Initial;

    public event Action? Changed;

    public void Dispatch(CounterAction action)
    {
        State = Reduce(State, action);
        Changed?.Invoke();
    }

    private static CounterState Reduce(CounterState state, CounterAction action)
    {
        return action switch
        {
            IncrementAction => state with
            {
                Count = state.Count + 1
            },

            ResetAction => state with
            {
                Count = 0
            },

            _ => state
        };
    }
}