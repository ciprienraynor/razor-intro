using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _07.StateManagement.Fluxor.Components.Features.Counter;

public partial class CounterDisplaySection : LoggingComponent
{
    [Parameter]
    [Watch]
    public int Value { get; set; }
}