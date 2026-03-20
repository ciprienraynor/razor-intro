using _08.StateManagement.StoreGovernor.Components.Shared.State;
using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _08.StateManagement.StoreGovernor.Components.Shared.Components;

public abstract class StoreComponent<TStore, TState> : LoggingComponent, IDisposable
    where TStore : IStore<TState>
{
    [Inject]
    protected TStore Store { get; set; } = default!;
    
    protected TState State => Store.State;

    protected sealed override void OnInitialized()
    {
        Store.Changed += OnStoreChanged;
        OnStoreInitialized();
    }

    protected virtual void OnStoreInitialized()
    {
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