namespace _08.StateManagement.StoreGovernor.Components.Shared.State;

public interface IErrorState
{
    string? ErrorMessage { get; }
    bool IsError { get; }
}