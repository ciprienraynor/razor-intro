using _11.StateManagement.SharedState.Core.BlazorKit;
using _11.StateManagement.SharedState.Core.Providers;
using _11.StateManagement.SharedState.Core.State;

namespace _11.StateManagement.SharedState.Features.ProductDetails.Presentation.Store;

public sealed class ProductDetailsStore : StoreBase<ProductDetailsState, ProductDetailsAction>, IDisposable
{
    private readonly ShoppingCartProvider _shoppingCartProvider;

    public ProductDetailsStore(ShoppingCartProvider shoppingCartProvider)
        : base(ProductDetailsState.Default)
    {
        _shoppingCartProvider = shoppingCartProvider;
        _shoppingCartProvider.Changed += OnSharedCartChanged;

        Dispatch(new ProductDetailsAction.SharedCartChanged(_shoppingCartProvider.CurrentState));
    }

    public void AddCookie()
    {
        _shoppingCartProvider.AddItem("cookie", "Cookie", 1.80m);
    }

    private void OnSharedCartChanged()
    {
        Dispatch(new ProductDetailsAction.SharedCartChanged(_shoppingCartProvider.CurrentState));
    }

    protected override ProductDetailsState Reduce(ProductDetailsState state, ProductDetailsAction action)
    {
        return action switch
        {
            ProductDetailsAction.SharedCartChanged changed => new ProductDetailsState(
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
        if (state.IsExpired) return "Cart expired while on Product Details.";
        if (state.Cart.IsEmpty) return "No active cart right now.";
        return $"Cart alive: {state.RemainingSeconds}s remaining.";
    }

    public void Dispose()
    {
        _shoppingCartProvider.Changed -= OnSharedCartChanged;
    }
}