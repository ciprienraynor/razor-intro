using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _05.StateManagement.StateContainer.Components.Features.Counter;

public partial class CounterDisplay : LoggingComponent
{
    [Parameter]
    [Watch]
    public int Value { get; set; }
}