namespace _10.StateManagement.Effects.Providers.Separation;

/// <summary>
/// This Provider is intentionally simple.
///
/// Important:
/// In this example it is NOT strictly necessary, because SelectedProductId
/// is not a shared application concern. The Store could own it directly.
///
/// It is included here only to demonstrate the architectural role of a Provider:
/// - controlled state/value access outside reducer
/// - reusable coordination point
/// - possible future shared concern
///
/// Other Provider benefits in richer flows:
/// - building state across more than one screen
/// - prepare -> navigate -> consume prepared state
/// - load -> navigate
/// - navigate -> load -> persist/share intermediate state
/// - timers, callbacks, listeners, background coordination
/// </summary>
public sealed class CatalogProvider
{
    private int _currentProductId = 1;

    public int GetCurrentProductId()
    {
        return _currentProductId;
    }

    public void SetCurrentProductId(int productId)
    {
        _currentProductId = productId;
    }
}