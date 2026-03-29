using _10.StateManagement.Effects.Data.ChainingSync.Remote;
using _10.StateManagement.Effects.Providers.ChainingAsync;
using _10.StateManagement.Effects.State.Abstracts;

namespace _10.StateManagement.Effects.State.ChainingAsync;

/// <summary>
/// Async chaining means:
/// one async result triggers the next async step.
/// Sequence matters.
/// </summary>
public sealed class ChainingAsyncStore : StoreBase<ChainingAsyncState, ChainingAsyncAction>
{
    private readonly OrderApiClient _orderApiClient;
    private readonly CustomerProvider _customerProvider;

    public ChainingAsyncStore(
        OrderApiClient orderApiClient,
        CustomerProvider customerProvider)
        : base(ChainingAsyncState.Default)
    {
        _orderApiClient = orderApiClient;
        _customerProvider = customerProvider;
    }

    public async Task StartChainAsync(CancellationToken cancellationToken = default)
    {
        Dispatch(new ChainingAsyncAction.Start());

        try
        {
            Dispatch(new ChainingAsyncAction.OrderLoadStarted());

            var order = await _orderApiClient.LoadOrderAsync(cancellationToken);
            Dispatch(new ChainingAsyncAction.OrderLoadCompleted(order));

            Dispatch(new ChainingAsyncAction.CustomerLoadStarted());

            var customerName = await _customerProvider.LoadCustomerDisplayNameAsync(order.UserId, cancellationToken);
            Dispatch(new ChainingAsyncAction.CustomerLoadCompleted(customerName));
        }
        catch (OperationCanceledException)
        {
            Dispatch(new ChainingAsyncAction.Failed("Chain cancelled."));
        }
        catch (Exception exception)
        {
            Dispatch(new ChainingAsyncAction.Failed(exception.Message));
        }
    }

    protected override ChainingAsyncState Reduce(ChainingAsyncState state, ChainingAsyncAction action)
    {
        return action switch
        {
            ChainingAsyncAction.Start => state with
            {
                IsLoadingOrder = false,
                IsLoadingCustomer = false,
                Order = null,
                CustomerName = null,
                Error = null,
                Status = "Chain started"
            },

            ChainingAsyncAction.OrderLoadStarted => state with
            {
                IsLoadingOrder = true,
                IsLoadingCustomer = false,
                Error = null,
                Status = "Loading order..."
            },

            ChainingAsyncAction.OrderLoadCompleted completed => state with
            {
                IsLoadingOrder = false,
                Order = completed.Order,
                Status = $"Order loaded: #{completed.Order.Id}"
            },

            ChainingAsyncAction.CustomerLoadStarted => state with
            {
                IsLoadingCustomer = true,
                Status = "Loading customer..."
            },

            ChainingAsyncAction.CustomerLoadCompleted completed => state with
            {
                IsLoadingCustomer = false,
                CustomerName = completed.CustomerName,
                Status = $"Customer loaded: {completed.CustomerName}"
            },

            ChainingAsyncAction.Failed failed => state with
            {
                IsLoadingOrder = false,
                IsLoadingCustomer = false,
                Error = failed.Error,
                Status = "Chain failed"
            },

            _ => state
        };
    }
}