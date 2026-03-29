namespace _11.StateManagement.SharedState.Core.BlazorKit;

public abstract class StoreBase<TState, TAction> : IStore<TState>
{
    private TState _state;

    public TState State => _state;

    public event Action? Changed;

    protected StoreBase(TState initialState)
    {
        _state = initialState;
    }

    public void Dispatch(TAction action)
    {
        var newState = Reduce(_state, action);
        
        if (EqualityComparer<TState>.Default.Equals(_state, newState))
            return;

        _state = newState;
        Changed?.Invoke();
    }

    protected abstract TState Reduce(TState state, TAction action);
}