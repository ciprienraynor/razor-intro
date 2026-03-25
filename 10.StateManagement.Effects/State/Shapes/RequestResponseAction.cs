using _10.StateManagement.Effects.Data.Separation;

namespace _10.StateManagement.Effects.State.Shapes;

public abstract record RequestResponseAction
{
    public sealed record LoadProduct : RequestResponseAction;
    public sealed record LoadProductCompleted(ProductDto Product) : RequestResponseAction;
    public sealed record LoadProductFailed(string Error) : RequestResponseAction;
}