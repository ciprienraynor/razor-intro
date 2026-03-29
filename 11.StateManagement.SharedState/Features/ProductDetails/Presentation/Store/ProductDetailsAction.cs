using _11.StateManagement.SharedState.Core.State;

namespace _11.StateManagement.SharedState.Features.ProductDetails.Presentation.Store;

public abstract record ProductDetailsAction
{
    public sealed record SharedCartChanged(ShoppingCartState ShoppingCartState) : ProductDetailsAction;
}