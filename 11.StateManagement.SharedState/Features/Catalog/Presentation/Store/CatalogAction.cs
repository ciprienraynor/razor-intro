using _11.StateManagement.SharedState.Core.State;

namespace _11.StateManagement.SharedState.Features.Catalog.Presentation.Store;

public abstract record CatalogAction
{
    public sealed record SharedCartChanged(ShoppingCartState ShoppingCartState) : CatalogAction;
}