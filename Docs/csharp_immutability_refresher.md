
# C# Immutability Refresher (Records, Immutable Collections, and State Containers)

This document is a **language-level refresher** for working with immutable state patterns in C#.  
It assumes familiarity with **UDF-style architectures** (Redux, MVI, TCA, etc.) and focuses only on **syntax and language constructs** that support immutable state handling.

The goal is not to explain architectural philosophy but to refresh the **C# tools that make immutable state practical**.

---

# 1. Records

Records are C# reference types designed for **value-like semantics**.

Key characteristics:

- Value equality
- Compiler-generated `Equals`, `GetHashCode`, `ToString`
- `with` expression support
- `init` property setters
- Suitable for **state snapshots and DTO-like objects**

Records are **immutable-friendly**, not strictly immutable.

---

# 2. Positional Record Syntax

Compact syntax typically used for simple DTO/state containers.

```csharp
public record User(
    int Id,
    string Name,
    string Email
);
```

Usage:

```csharp
var user = new User(1, "Igor", "igor@example.com");

var updated = user with { Name = "Igor Makis" };
```

Conceptually generated members include:

```
init-only properties
value equality
Deconstruct()
ToString()
with-clone support
```

---

# 3. Property-Based Record Syntax

Used when additional logic or methods are needed.

```csharp
public record User
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }

    public User(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}
```

Usage remains identical:

```csharp
var user = new User(1, "Igor", "igor@example.com");

var updated = user with { Email = "new@example.com" };
```

---

# 4. Record Equality

Records use **value equality**.

```csharp
var a = new User(1, "Igor", "a@x.com");
var b = new User(1, "Igor", "a@x.com");

Console.WriteLine(a == b); // true
```

Classes would compare **reference identity**, records compare **data**.

---

# 5. Immutability Caveat

Records do **not enforce deep immutability**.

Example:

```csharp
public record Order
{
    public List<string> Items { get; init; } = new();
}
```

This is still mutable:

```csharp
order.Items.Add("Item1");
```

Immutability depends on **contained types**.

---

# 6. init-only Properties

`init` setters allow assignment **only during initialization**.

```csharp
public record User
{
    public int Id { get; init; }
    public string Name { get; init; }
}
```

Valid:

```csharp
var user = new User { Id = 1, Name = "Igor" };
```

Invalid:

```csharp
user.Name = "Other"; // compile error
```

---

# 7. Immutable Collections (System.Collections.Immutable)

Namespace:

```
System.Collections.Immutable
```

Install package if needed:

```
System.Collections.Immutable
```

Immutable collections return **new structures on modification**.

Original instance remains unchanged.

---

# 8. Immutable Collection Types

| Mutable Type | Immutable Equivalent |
|--------------|---------------------|
| List<T> | ImmutableList<T> |
| T[] | ImmutableArray<T> |
| HashSet<T> | ImmutableHashSet<T> |
| Dictionary<TKey,TValue> | ImmutableDictionary<TKey,TValue> |
| SortedSet<T> | ImmutableSortedSet<T> |
| SortedDictionary<TKey,TValue> | ImmutableSortedDictionary<TKey,TValue> |

---

# 9. ImmutableArray

Often used for **state snapshots** due to compact layout and fast iteration.

```csharp
public record State
{
    public ImmutableArray<string> Items { get; init; }
}
```

Mutation pattern:

```csharp
state = state with
{
    Items = state.Items.Add("NewItem")
};
```

Properties:

```
struct
contiguous memory
fast enumeration
cheap copying
```

Downside:

```
Add/Remove rebuilds array
```

Best for **read-heavy collections**.

---

# 10. ImmutableList

Better when frequent insert/remove operations occur.

```csharp
public record State
{
    public ImmutableList<string> Items { get; init; }
        = ImmutableList<string>.Empty;
}
```

Mutation:

```csharp
state = state with
{
    Items = state.Items.Add("Item")
};
```

Underlying structure uses **persistent trees** with structural sharing.

---

# 11. ImmutableHashSet

Equivalent to `HashSet<T>`.

```csharp
public record State
{
    public ImmutableHashSet<int> SelectedIds { get; init; }
        = ImmutableHashSet<int>.Empty;
}
```

Add:

```csharp
state = state with
{
    SelectedIds = state.SelectedIds.Add(id)
};
```

Remove:

```csharp
state = state with
{
    SelectedIds = state.SelectedIds.Remove(id)
};
```

---

# 12. ImmutableDictionary

Equivalent to `Dictionary<TKey,TValue>`.

```csharp
public record State
{
    public ImmutableDictionary<int, Order> Orders { get; init; }
        = ImmutableDictionary<int, Order>.Empty;
}
```

Update:

```csharp
state = state with
{
    Orders = state.Orders.SetItem(order.Id, order)
};
```

Remove:

```csharp
state = state with
{
    Orders = state.Orders.Remove(order.Id)
};
```

---

# 13. Structural Sharing

Immutable collections avoid copying entire structures.

Instead they reuse internal nodes.

Conceptually:

```
old structure
   |
   |---- reused nodes
   |
   ---- new branch
```

This makes updates:

```
memory efficient
safe for snapshots
cheap to copy
```

---

# 14. Typical State Container Example

```csharp
public record AppState
{
    public ImmutableDictionary<int, Order> Orders { get; init; }
        = ImmutableDictionary<int, Order>.Empty;

    public ImmutableHashSet<int> SelectedOrders { get; init; }
        = ImmutableHashSet<int>.Empty;

    public ImmutableArray<string> Notifications { get; init; }
        = ImmutableArray<string>.Empty;
}
```

Update example:

```csharp
state = state with
{
    Orders = state.Orders.SetItem(order.Id, order),
    SelectedOrders = state.SelectedOrders.Add(order.Id),
    Notifications = state.Notifications.Add("Order added")
};
```

---

# 15. Interface Exposure Patterns

Even when immutable collections are used internally, APIs often expose read-only interfaces.

Example:

```csharp
public IReadOnlyList<Order> Orders => _orders;
```

Common exposure interfaces:

```
IReadOnlyList<T>
IReadOnlyCollection<T>
IReadOnlyDictionary<TKey,TValue>
```

This simplifies API usage while preserving internal immutability.

---

# Summary

Key constructs supporting immutable state in C#:

```
records
init-only properties
with expressions
System.Collections.Immutable collections
structural sharing collections
```

Most commonly used immutable containers:

```
ImmutableArray
ImmutableList
ImmutableHashSet
ImmutableDictionary
```

These provide practical language-level support for **state snapshot patterns** without requiring runtime frameworks.
