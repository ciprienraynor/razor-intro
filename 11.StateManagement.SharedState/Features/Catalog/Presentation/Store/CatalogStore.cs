using _11.StateManagement.SharedState.Core.BlazorKit;
using _11.StateManagement.SharedState.Core.Providers;
using _11.StateManagement.SharedState.Core.State;

namespace _11.StateManagement.SharedState.Features.Catalog.Presentation.Store;

public sealed class CatalogStore : StoreBase<CatalogState, CatalogAction>, IDisposable
{
    private readonly ShoppingCartProvider _shoppingCartProvider;

    public CatalogStore(ShoppingCartProvider shoppingCartProvider)
        : base(CatalogState.Default)
    {
        _shoppingCartProvider = shoppingCartProvider;
        _shoppingCartProvider.Changed += OnSharedCartChanged;

        Dispatch(new CatalogAction.SharedCartChanged(_shoppingCartProvider.CurrentState));
    }

    public void AddCoffee()
    {
        _shoppingCartProvider.AddItem("coffee", "Coffee", 3.50m);
    }

    public void AddTea()
    {
        _shoppingCartProvider.AddItem("tea", "Tea", 2.20m);
    }

    public void ClearCart()
    {
        _shoppingCartProvider.ClearCart();
    }

    private void OnSharedCartChanged()
    {
        Dispatch(new CatalogAction.SharedCartChanged(_shoppingCartProvider.CurrentState));
    }

    protected override CatalogState Reduce(CatalogState state, CatalogAction action)
    {
        return action switch
        {
            CatalogAction.SharedCartChanged changed => new CatalogState(
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
        if (state.IsExpired) return "Previous cart expired. Start again.";
        if (state.Cart.IsEmpty) return "Browse products and add them to cart.";
        return $"Catalog sees {state.Cart.Count} item(s) in cart.";
    }

    public void Dispose()
    {
        _shoppingCartProvider.Changed -= OnSharedCartChanged;
    }
}