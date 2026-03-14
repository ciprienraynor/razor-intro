using Blazor.Diagnostics;
using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _02.Components.Composition.Components.Shared;

public partial class CounterPanel : LoggingComponent
{
    [Parameter]
    [Watch]
    public int Value { get; set; }

    [Parameter]
    public EventCallback OnIncrement { get; set; }

    [Parameter]
    public EventCallback OnReset { get; set; }

    private async Task IncrementClicked()
    {
        await OnIncrement.InvokeAsync();
    }

    private async Task ResetClicked()
    {
        await OnReset.InvokeAsync();
    }
}