namespace _10.StateManagement.Effects.State.Definition;

public sealed record DefinitionState(
    int Count,
    bool IsLoading,
    string Message)
{
    public static DefinitionState Default => new(
        Count: 0,
        IsLoading: false,
        Message: "No message loaded");
}