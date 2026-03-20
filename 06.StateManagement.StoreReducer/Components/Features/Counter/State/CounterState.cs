namespace _06.StateManagement.StoreReducer.Components.Features.Counter.State;

public record CounterState(
    int Count,
    bool IsLoading,
    string? ErrorMessage)
{
    public bool IsError => !string.IsNullOrWhiteSpace(ErrorMessage);

    public static CounterState Initial => new(
        Count: 0,
        IsLoading: false,
        ErrorMessage: null
    );
}