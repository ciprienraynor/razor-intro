using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _07.StateManagement.Fluxor.Components.Features.Counter;

public partial class CounterHost : LoggingComponent
{
    [Parameter]
    [Watch]
    public int Value { get; set; }

    [Parameter]
    public EventCallback OnIncrement { get; set; }

    [Parameter]
    public EventCallback OnReset { get; set; }
}