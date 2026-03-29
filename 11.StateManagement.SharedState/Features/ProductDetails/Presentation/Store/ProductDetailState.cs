namespace _11.StateManagement.SharedState.Features.ProductDetails.Presentation.Store;

public sealed record ProductDetailsState(
    int CartCount,
    int RemainingSeconds,
    bool IsExpired,
    string StatusMessage)
{
    public static ProductDetailsState Default => new(
        CartCount: 0,
        RemainingSeconds: 0,
        IsExpired: false,
        StatusMessage: "Product details page.");
}