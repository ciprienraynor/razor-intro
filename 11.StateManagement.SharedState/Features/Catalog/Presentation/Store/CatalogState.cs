namespace _11.StateManagement.SharedState.Features.Catalog.Presentation.Store;

public sealed record CatalogState(
    int CartCount,
    int RemainingSeconds,
    bool IsExpired,
    string StatusMessage)
{
    public static CatalogState Default => new(
        CartCount: 0,
        RemainingSeconds: 0,
        IsExpired: false,
        StatusMessage: "Browse products and add them to cart.");
}