using _06.StateManagement.StoreReducer.Components.Features.Counter.State;
using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _06.StateManagement.StoreReducer.Components.Features.Counter;

public partial class CounterDisplay : LoggingComponent, IDisposable
{
    [Inject]
    private CounterStore Store { get; set; } = null!;

    [Watch]
    private int Value => Store.State.Count;

    protected override void OnInitialized()
    {
        Store.Changed += OnStoreChanged;
    }

    private void OnStoreChanged()
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        Store.Changed -= OnStoreChanged;
    }
}