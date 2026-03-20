using _08.StateManagement.StoreGovernor.Components.Features.Counter.Services;
using _08.StateManagement.StoreGovernor.Components.Shared.State;

namespace _08.StateManagement.StoreGovernor.Components.Features.Counter.State;

public class CounterStore : IStore<CounterState>
{
    private readonly ICounterService _service;

    public CounterStore(ICounterService counterService)
    {
        _service = counterService;
    }

    public CounterState State { get; private set; } = CounterState.Initial;

    public event Action? Changed;
    
    public void Increment()
    {
        Dispatch(new IncrementAction());
    }

    public void Reset()
    {
        Dispatch(new ResetAction());
    }

    public async Task LoadAsync()
    {
        Dispatch(new SetLoadingAction(true));

        try
        {
            var value = await _service.GetRemoteCount();
            Dispatch(new LoadSuccessAction(value));
        }
        catch (Exception ex)
        {
            Dispatch(new LoadErrorAction(ex.Message));
        }
    }

    private void Dispatch(CounterAction action)
    {
        State = Reduce(State, action);
        Changed?.Invoke();
    }

    private static CounterState Reduce(CounterState state, CounterAction action)
    {
        return action switch
        {
            IncrementAction =>
                state with { Count = state.Count + 1 },

            ResetAction =>
                state with { Count = 0 },

            SetLoadingAction a =>
                state with { IsLoading = a.IsLoading, ErrorMessage = null },

            LoadSuccessAction a =>
                state with { Count = a.Value, IsLoading = false },

            LoadErrorAction a =>
                state with { ErrorMessage = a.ErrorMessage, IsLoading = false },

            _ => state
        };
    }
}