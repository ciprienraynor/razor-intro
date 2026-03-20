using _08.StateManagement.StoreGovernor.Components.Features.Counter.State;
using _08.StateManagement.StoreGovernor.Components.Shared.Components;
using Microsoft.AspNetCore.Components;  

namespace _08.StateManagement.StoreGovernor.Components.Features.Counter;

public partial class CounterControls : StoreComponent<CounterStore, CounterState>
{
    private void IncrementClicked()
    {
        Store.Increment();
    }

    private void ResetClicked()
    {
        Store.Reset();
    }
    
    // custom hook - cause we could forget base if overriding the real hook
    // much more realistical data loading upon control initialization
    protected sealed override async Task OnStoreInitializedAsync()
    {
        await Store.LoadAsync(); // or if both just await Load();
    }

    // triggered explicitly
    private async Task Load()
    {
        await Store.LoadAsync();
    }
}