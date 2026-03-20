using _08.StateManagement.StoreGovernor.Components.Shared.State;
using Microsoft.AspNetCore.Components;

namespace _08.StateManagement.StoreGovernor.Components.Shared.Components;

public partial class ErrorView<TState> : ComponentBase
    where TState : IErrorState
{
    [Parameter] public TState State { get; set; } = default!;
}