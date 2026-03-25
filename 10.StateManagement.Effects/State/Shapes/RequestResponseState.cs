using _10.StateManagement.Effects.Data.Separation;

namespace _10.StateManagement.Effects.State.Shapes;

public sealed record RequestResponseState(
    bool IsLoading,
    ProductDto? Product,
    string? Error)
{
    public static RequestResponseState Default => new(
        IsLoading: false,
        Product: null,
        Error: null);
}