using _10.StateManagement.Effects.Data.ChainingSync;

namespace _10.StateManagement.Effects.State.ChainingAsync;

public sealed record ChainingAsyncState(
    bool IsLoadingOrder,
    bool IsLoadingCustomer,
    OrderDto? Order,
    string? CustomerName,
    string Status,
    string? Error)
{
    public static ChainingAsyncState Default => new(
        IsLoadingOrder: false,
        IsLoadingCustomer: false,
        Order: null,
        CustomerName: null,
        Status: "Idle",
        Error: null);
}