using _08.StateManagement.StoreGovernor.Components.Shared.State;

namespace _08.StateManagement.StoreGovernor.Components.Features.Counter.State;

public record CounterState(
    int Count,
    bool IsLoading,
    string? ErrorMessage
) : ILoadableState, IErrorState
{
    public static CounterState Initial => new(0, false, null);
    public bool IsError => throw new NotImplementedException();
}