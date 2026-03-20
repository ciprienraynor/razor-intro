using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _05.StateManagement.StateContainer.Components.Features.Counter;

public partial class CounterDisplay : LoggingComponent, IDisposable
{
    [Inject]    
    private CounterStateContainer Container { get; set; } = null!;
    
    [Watch]
    private int Value => Container.State.Count;

    protected override void OnInitialized()
    {
        Container.Changed += OnStateChanged;
    }

    private void OnStateChanged()
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        Container.Changed -= OnStateChanged;
    }
}