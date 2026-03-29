using _11.StateManagement.SharedState.Core.BlazorKit;
using _11.StateManagement.SharedState.Core.Providers;
using _11.StateManagement.SharedState.Core.State;

namespace _11.StateManagement.SharedState.Features.CartStatus.Presentation.Store;

public sealed class CartStatusStore : StoreBase<CartStatusState, CartStatusAction>, IDisposable
{
    private readonly ShoppingCartProvider _shoppingCartProvider;

    public CartStatusStore(ShoppingCartProvider shoppingCartProvider)
        : base(CartStatusState.Default)
    {
        _shoppingCartProvider = shoppingCartProvider;
        _shoppingCartProvider.Changed += OnSharedCartChanged;

        Dispatch(new CartStatusAction.SharedCartChanged(_shoppingCartProvider.CurrentState));
    }

    private void OnSharedCartChanged()
    {
        Dispatch(new CartStatusAction.SharedCartChanged(_shoppingCartProvider.CurrentState));
    }

    protected override CartStatusState Reduce(CartStatusState state, CartStatusAction action)
    {
        return action switch
        {
            CartStatusAction.SharedCartChanged changed => new CartStatusState(
                CartCount: changed.ShoppingCartState.Cart.Count,
                RemainingSeconds: changed.ShoppingCartState.RemainingSeconds,
                IsExpired: changed.ShoppingCartState.IsExpired,
                StatusText: BuildStatus(changed.ShoppingCartState)
            ),
            _ => state
        };
    }

    private static string BuildStatus(ShoppingCartState state)
    {
        if (state.IsExpired) return "Cart expired";
        if (state.Cart.IsEmpty) return "Cart is empty";
        return $"Cart: {state.Cart.Count} item(s), {state.RemainingSeconds}s left";
    }

    public void Dispose()
    {
        _shoppingCartProvider.Changed -= OnSharedCartChanged;
    }
}