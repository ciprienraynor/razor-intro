using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _07.StateManagement.Fluxor.Components.Features.Counter;

public partial class CounterDisplay : LoggingComponent
{
    [Watch]
    public int Value { get; set; }
}