namespace _11.StateManagement.SharedState.Core.Models;

public sealed record ShoppingCart(
    string Id,
    IReadOnlyList<ShoppingCartItem> Items)
{
    public static ShoppingCart Empty => new(
        Id: Guid.NewGuid().ToString("N"),
        Items: Array.Empty<ShoppingCartItem>());

    public bool IsEmpty => Items.Count == 0;

    public int Count => Items.Sum(item => item.Quantity);

    public decimal Total => Items.Sum(item => item.RowTotal);
}