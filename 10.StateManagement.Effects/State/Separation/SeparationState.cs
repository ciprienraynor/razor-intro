using _10.StateManagement.Effects.Data.Separation;

namespace _10.StateManagement.Effects.State.Separation;

public sealed record SeparationState(
    int SelectedProductId,
    bool IsLoading,
    ProductDto? Product,
    string? Error)
{
    public static SeparationState CreateDefault(int selectedProductId)
    {
        return new SeparationState(
            SelectedProductId: selectedProductId,
            IsLoading: false,
            Product: null,
            Error: null);
    }
}