namespace _08.StateManagement.StoreGovernor.Components.Features.Counter.State;

public abstract record CounterAction();

public record IncrementAction : CounterAction;
public record ResetAction : CounterAction;

public record SetLoadingAction(bool IsLoading) : CounterAction;
public record LoadSuccessAction(int Value) : CounterAction;
public record LoadErrorAction(string ErrorMessage) : CounterAction;