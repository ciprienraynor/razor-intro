namespace _11.StateManagement.SharedState.Core.Models;

public sealed record ShoppingCartItem(
    string Id,
    string Name,
    int Quantity,
    decimal UnitPrice)
{
    public decimal RowTotal => Quantity * UnitPrice;
}