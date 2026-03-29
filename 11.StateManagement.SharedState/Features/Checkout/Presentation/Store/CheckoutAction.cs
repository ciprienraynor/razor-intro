using _11.StateManagement.SharedState.Core.State;

namespace _11.StateManagement.SharedState.Features.Checkout.Presentation.Store;

public abstract record CheckoutAction
{
    public sealed record SharedCartChanged(ShoppingCartState ShoppingCartState) : CheckoutAction;
}