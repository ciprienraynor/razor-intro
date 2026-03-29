# 11.StateManagement.SharedState

## Purpose

Demonstrate **application-wide shared state** with:

- Provider-owned runtime behavior
- Cross-page consistency
- Navigation-independent lifecycle (e.g. countdown)
- Clear separation of responsibilities:
  - Domain
  - State
  - Provider
  - Store
  - UI

---

## 1. Story → Workflow

User opens app  
→ browses catalog  
→ adds item to cart  
→ cart is created (if not exists)  
→ countdown starts (e.g. 15 min)

User navigates freely:
- catalog
- product details
- checkout

While navigating:
- cart count visible globally
- countdown visible globally

User actions:
- add/remove items
- proceed to checkout
- clear cart

System behavior:
- countdown ticks globally
- if reaches 0:
  → cart is cleared  
  → expiration flag set  
  → UI reflects expiration anywhere

Optional:
- adding a new item starts a new cart session

---

## 2. Domain → Actors

### Domain

#### ShoppingCart
- Id
- Items (List<ShoppingCartItem>)
- Total
- Count (domain-defined)
- IsEmpty

#### ShoppingCartItem
- Id (SKU / GUID)
- Name
- Quantity
- UnitPrice
- RowTotal

---

### Runtime / Application State

#### ShoppingCartState
- ShoppingCart
- RemainingSeconds
- IsTimerRunning
- IsExpired

> Important:  
> ShoppingCart ≠ ShoppingCartState  
>  
> Cart = domain  
> State = runtime context around the cart

---

### Actors

#### ShoppingCartProvider
- owns ShoppingCartState
- manages timer lifecycle
- mutates state
- survives navigation

#### Feature Stores
- CatalogStore
- ProductDetailsStore
- CheckoutStore
- CartStatusStore

Responsibilities:
- read shared state
- derive view state
- trigger Provider operations

---

### External

#### ApiClient
- network calls
- remote data access
- no business logic

---

## 3. Project Structure

```text
Infrastructure/
    Network/
        Http/
    Data/
        Remote/
        Local/

Core/
    Models/
        ShoppingCart/
            ShoppingCart.cs
            ShoppingCartItem.cs
    State/
        ShoppingCartState.cs
    Providers/
        ShoppingCartProvider.cs
    Components/
    Utilities/
    Extensions/
    Constants/

Features/
    Catalog/
        Data/
        Domain/
        Presentation/
            Pages/
            Components/
            Stores/

    ProductDetails/
        Data/
        Domain/
        Presentation/
            Pages/
            Components/
            Stores/

    Checkout/
        Data/
        Domain/
        Presentation/
            Pages/
            Components/
            Stores/

    CartStatus/
        Presentation/
            Components/
            Stores/

App/
    Program.cs
    Routing/
    Layout/
    Configuration/
```

---

## 4. Architectural Rules

- No top-level Components directory  
  → Components exist only inside:
  - Core (shared)
  - Features/.../Presentation

- Domain is separate from runtime state  
  → ShoppingCart is not ShoppingCartState

- Provider owns:
  - mutation
  - timer
  - lifecycle

- Store:
  - derives state
  - does not own shared concerns

- Component:
  - renders only
  - no business logic

---

## 5. Key Concept

```text
Provider → mutates shared state
Store → derives state
Component → renders state
```

---

## 6. Why this matters

This structure:

- avoids duplicated logic across screens
- supports navigation-independent behavior
- keeps effects centralized and controlled
- prevents UI-driven architecture
- scales without changing mental model

---

## 7. Core Insight

> Shared application concerns (Cart, Auth, Navigation, Localization)  
> should be owned by **Provider + State**, not by Feature Stores.
