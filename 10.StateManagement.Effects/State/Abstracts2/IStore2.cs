namespace _10.StateManagement.Effects.State.Abstracts2;

/// <summary>
/// Minimal Store contract:
/// - state
/// - change notification
/// - lifecycle signal from View
/// </summary>
public interface IStore2<out TState>
{
    TState State { get; }
    event Action? Changed;

    /// <summary>
    /// Called by View when its lifetime ends.
    /// Store decides what to do with in-flight effects.
    /// </summary>
    void OnViewDisposed();
}