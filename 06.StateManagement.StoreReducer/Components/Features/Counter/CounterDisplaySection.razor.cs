using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _06.StateManagement.StoreReducer.Components.Features.Counter;

public partial class CounterDisplaySection : LoggingComponent
{
    [Parameter]
    [Watch]
    public int Value { get; set; }
}