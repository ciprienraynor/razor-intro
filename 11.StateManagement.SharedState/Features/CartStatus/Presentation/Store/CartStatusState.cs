namespace _11.StateManagement.SharedState.Features.CartStatus.Presentation.Store;

public sealed record CartStatusState(
    int CartCount,
    int RemainingSeconds,
    bool IsExpired,
    string StatusText)
{
    public static CartStatusState Default => new(
        CartCount: 0,
        RemainingSeconds: 0,
        IsExpired: false,
        StatusText: "Cart is empty");
}