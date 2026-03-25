using _10.StateManagement.Effects.Services;
using _10.StateManagement.Effects.State.Abstracts;

namespace _10.StateManagement.Effects.State.Definition;

public sealed class DefinitionStore : StoreBase<DefinitionState, DefinitionAction>
{
    private readonly FakeMessageService _fakeMessageService;

    public DefinitionStore(FakeMessageService fakeMessageService)
        : base(DefinitionState.Default)
    {
        _fakeMessageService = fakeMessageService;
    }

    protected override DefinitionState Reduce(DefinitionState state, DefinitionAction action)
    {
        return action switch
        {
            DefinitionAction.Increment =>
                state with
                {
                    Count = state.Count + 1
                },

            DefinitionAction.LoadMessage =>
                state with
                {
                    IsLoading = true,
                    Message = "Loading..."
                },

            DefinitionAction.LoadMessageCompleted completed =>
                state with
                {
                    IsLoading = false,
                    Message = completed.Message
                },

            _ => state
        };
    }

    public async Task LoadMessageAsync()
    {
        Dispatch(new DefinitionAction.LoadMessage());

        var message = await _fakeMessageService.LoadAsync();

        Dispatch(new DefinitionAction.LoadMessageCompleted(message));
    }
}