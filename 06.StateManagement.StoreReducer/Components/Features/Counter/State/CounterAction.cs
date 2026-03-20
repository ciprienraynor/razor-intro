namespace _06.StateManagement.StoreReducer.Components.Features.Counter.State;

public abstract record CounterAction;
public record IncrementAction : CounterAction;
public record ResetAction : CounterAction;