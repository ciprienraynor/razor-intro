using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _06.StateManagement.StoreReducer.Components.Features.Counter;

public partial class CounterDisplay : LoggingComponent
{
    [Parameter]
    [Watch]
    public int Value { get; set; }
}