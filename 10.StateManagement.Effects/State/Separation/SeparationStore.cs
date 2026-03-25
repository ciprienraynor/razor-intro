using _10.StateManagement.Effects.Data.Separation.Remote;
using _10.StateManagement.Effects.Providers.Separation;
using _10.StateManagement.Effects.State.Abstracts;

namespace _10.StateManagement.Effects.State.Separation;

/// <summary>
/// Store is the feature center:
/// - owns state
/// - dispatches actions
/// - reduces state purely
/// - starts effects OUTSIDE reducer
/// 
/// Note:
/// Provider is used here only to demonstrate collaboration.
/// Since SelectedProductId is not shared application state,
/// this Store could own it directly and Provider could be removed.
/// </summary>
public sealed class SeparationStore : StoreBase<SeparationState, SeparationAction>
{
    private readonly CatalogProvider _catalogProvider;
    private readonly ProductApiClient _productApiClient;

    public SeparationStore(
        CatalogProvider catalogProvider,
        ProductApiClient productApiClient)
        : base(SeparationState.CreateDefault(catalogProvider.GetCurrentProductId()))
    {
        _catalogProvider = catalogProvider;
        _productApiClient = productApiClient;
    }

    public void SelectProduct(int productId)
    {
        // Provider interaction happens OUTSIDE reducer
        _catalogProvider.SetCurrentProductId(productId);

        Dispatch(new SeparationAction.ProductSelected(productId));
    }

    public async Task LoadCurrentProductAsync(CancellationToken cancellationToken = default)
    {
        var productId = _catalogProvider.GetCurrentProductId();

        Dispatch(new SeparationAction.LoadProduct(productId));

        try
        {
            // Effect starts here, outside Reduce(...)
            var product = await _productApiClient.LoadProductAsync(productId, cancellationToken);

            // Result comes back through a new action
            Dispatch(new SeparationAction.LoadProductCompleted(product));
        }
        catch (OperationCanceledException)
        {
            Dispatch(new SeparationAction.LoadProductFailed("Request was cancelled."));
        }
        catch (Exception exception)
        {
            Dispatch(new SeparationAction.LoadProductFailed(exception.Message));
        }
    }

    protected override SeparationState Reduce(SeparationState state, SeparationAction action)
    {
        return action switch
        {
            SeparationAction.ProductSelected selected => state with
            {
                SelectedProductId = selected.ProductId,
                Error = null
            },

            SeparationAction.LoadProduct started => state with
            {
                SelectedProductId = started.ProductId,
                IsLoading = true,
                Product = null,
                Error = null
            },

            SeparationAction.LoadProductCompleted completed => state with
            {
                IsLoading = false,
                Product = completed.Product,
                Error = null
            },

            SeparationAction.LoadProductFailed failed => state with
            {
                IsLoading = false,
                Product = null,
                Error = failed.Error
            },

            _ => state
        };
    }
}