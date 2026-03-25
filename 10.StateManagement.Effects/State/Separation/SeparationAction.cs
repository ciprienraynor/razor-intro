using _10.StateManagement.Effects.Data.Separation;

namespace _10.StateManagement.Effects.State.Separation;

public abstract record SeparationAction
{
    public sealed record ProductSelected(int ProductId) : SeparationAction;
    public sealed record LoadProduct(int ProductId) : SeparationAction;
    public sealed record LoadProductCompleted(ProductDto Product) : SeparationAction;
    public sealed record LoadProductFailed(string Error) : SeparationAction;
}