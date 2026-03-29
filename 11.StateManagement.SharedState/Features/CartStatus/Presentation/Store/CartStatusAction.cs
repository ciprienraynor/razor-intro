using _11.StateManagement.SharedState.Core.State;

namespace _11.StateManagement.SharedState.Features.CartStatus.Presentation.Store;

public abstract record CartStatusAction
{
    public sealed record SharedCartChanged(ShoppingCartState ShoppingCartState) : CartStatusAction;
}