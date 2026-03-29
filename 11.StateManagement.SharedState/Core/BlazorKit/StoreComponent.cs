using Microsoft.AspNetCore.Components;

namespace _11.StateManagement.SharedState.Core.BlazorKit;

public abstract class StoreComponent<TStore, TState> : ComponentBase, IDisposable
    where TStore : IStore<TState>
{
    [Inject]
    protected TStore Store { get; set; } = default!;

    protected TState State => Store.State;

    protected sealed override void OnInitialized()
    {
        Store.Changed += OnStoreChanged;
    }

    protected sealed override async Task OnInitializedAsync()
    {
        await OnStoreInitializedAsync();
    }

    protected virtual Task OnStoreInitializedAsync()
    {
        return Task.CompletedTask;
    }

    private void OnStoreChanged()
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        Store.Changed -= OnStoreChanged;
        GC.SuppressFinalize(this);
    }
}