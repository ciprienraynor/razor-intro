using _10.StateManagement.Effects.Data.ChainingSync;

namespace _10.StateManagement.Effects.State.ChainingAsync;

public abstract record ChainingAsyncAction
{
    public sealed record Start : ChainingAsyncAction;
    public sealed record OrderLoadStarted : ChainingAsyncAction;
    public sealed record OrderLoadCompleted(OrderDto Order) : ChainingAsyncAction;
    public sealed record CustomerLoadStarted : ChainingAsyncAction;
    public sealed record CustomerLoadCompleted(string CustomerName) : ChainingAsyncAction;
    public sealed record Failed(string Error) : ChainingAsyncAction;
}