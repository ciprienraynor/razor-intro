using _11.StateManagement.SharedState.Core.BlazorKit;
using _11.StateManagement.SharedState.Core.Providers;
using _11.StateManagement.SharedState.Core.State;

namespace _11.StateManagement.SharedState.Features.Checkout.Presentation.Store;

public sealed class CheckoutStore : StoreBase<CheckoutState, CheckoutAction>, IDisposable
{
    private readonly ShoppingCartProvider _shoppingCartProvider;

    public CheckoutStore(ShoppingCartProvider shoppingCartProvider)
        : base(CheckoutState.Default)
    {
        _shoppingCartProvider = shoppingCartProvider;
        _shoppingCartProvider.Changed += OnSharedCartChanged;

        Dispatch(new CheckoutAction.SharedCartChanged(_shoppingCartProvider.CurrentState));
    }

    public void ClearCart()
    {
        _shoppingCartProvider.ClearCart();
    }

    private void OnSharedCartChanged()
    {
        Dispatch(new CheckoutAction.SharedCartChanged(_shoppingCartProvider.CurrentState));
    }

    protected override CheckoutState Reduce(CheckoutState state, CheckoutAction action)
    {
        return action switch
        {
            CheckoutAction.SharedCartChanged changed => new CheckoutState(
                CartCount: changed.ShoppingCartState.Cart.Count,
                RemainingSeconds: changed.ShoppingCartState.RemainingSeconds,
                IsExpired: changed.ShoppingCartState.IsExpired,
                StatusMessage: BuildMessage(changed.ShoppingCartState)
            ),
            _ => state
        };
    }

    private static string BuildMessage(ShoppingCartState state)
    {
        if (state.IsExpired) return "Cart expired. Checkout no longer possible.";
        if (state.Cart.IsEmpty) return "Cart is empty.";
        return $"Ready to checkout {state.Cart.Count} item(s).";
    }

    public void Dispose()
    {
        _shoppingCartProvider.Changed -= OnSharedCartChanged;
    }
}