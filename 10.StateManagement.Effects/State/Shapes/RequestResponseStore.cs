using _10.StateManagement.Effects.Data.Separation.Remote;
using _10.StateManagement.Effects.State.Abstracts;
namespace _10.StateManagement.Effects.State.Shapes;

public sealed class RequestResponseStore : StoreBase<RequestResponseState, RequestResponseAction>
{
    private readonly ProductApiClient _productApiClient;

    public RequestResponseStore(ProductApiClient productApiClient)
        : base(RequestResponseState.Default)
    {
        _productApiClient = productApiClient;
    }

    public async Task LoadProductAsync(CancellationToken cancellationToken = default)
    {
        Dispatch(new RequestResponseAction.LoadProduct());

        try
        {
            var product = await _productApiClient.LoadProductAsync(1, cancellationToken);
            Dispatch(new RequestResponseAction.LoadProductCompleted(product));
        }
        catch (OperationCanceledException)
        {
            Dispatch(new RequestResponseAction.LoadProductFailed("Request cancelled."));
        }
        catch (Exception exception)
        {
            Dispatch(new RequestResponseAction.LoadProductFailed(exception.Message));
        }
    }

    protected override RequestResponseState Reduce(RequestResponseState state, RequestResponseAction action)
    {
        return action switch
        {
            RequestResponseAction.LoadProduct => state with
            {
                IsLoading = true,
                Product = null,
                Error = null
            },

            RequestResponseAction.LoadProductCompleted completed => state with
            {
                IsLoading = false,
                Product = completed.Product,
                Error = null
            },

            RequestResponseAction.LoadProductFailed failed => state with
            {
                IsLoading = false,
                Product = null,
                Error = failed.Error
            },

            _ => state
        };
    }
}