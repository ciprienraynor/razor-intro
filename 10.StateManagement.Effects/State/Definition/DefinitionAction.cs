namespace _10.StateManagement.Effects.State.Definition;

public abstract record DefinitionAction
{
    public sealed record Increment : DefinitionAction;
    public sealed record LoadMessage : DefinitionAction;
    public sealed record LoadMessageCompleted(string Message) : DefinitionAction;
}