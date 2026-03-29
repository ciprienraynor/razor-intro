namespace _11.StateManagement.SharedState.Features.Checkout.Presentation.Store;

public sealed record CheckoutState(
    int CartCount,
    int RemainingSeconds,
    bool IsExpired,
    string StatusMessage)
{
    public static CheckoutState Default => new(
        CartCount: 0,
        RemainingSeconds: 0,
        IsExpired: false,
        StatusMessage: "Checkout waiting for cart.");
}