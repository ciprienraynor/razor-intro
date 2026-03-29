using _11.StateManagement.SharedState.Core.Models;

namespace _11.StateManagement.SharedState.Core.State;

public sealed record ShoppingCartState(
    ShoppingCart Cart,
    int RemainingSeconds,
    bool IsTimerRunning,
    bool IsExpired)
{
    public static ShoppingCartState Default => new(
        Cart: ShoppingCart.Empty,
        RemainingSeconds: 0,
        IsTimerRunning: false,
        IsExpired: false);
}