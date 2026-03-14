# Blazor Diagnostics

Lightweight diagnostics helpers for **Blazor Server applications**.

The goal is simple: make Blazor Server runtime behavior observable
during development and debugging.

This library exposes three useful observation points:

-   SignalR hub activity (client → server events)
-   Blazor circuit lifecycle and activity
-   Component state logging

------------------------------------------------------------------------

# Wiring in the Consumer Application

Add the diagnostics services in **Program.cs**.

``` csharp
builder.Services.AddBlazorDiagnostics();
```

This enables:

-   SignalR payload sniffing
-   Circuit monitoring
-   Component state logging support

------------------------------------------------------------------------

# Using Component State Logging

Derive your component from `LoggingComponent`.

Example:

``` csharp
public partial class Counter : LoggingComponent
{
    [Watch]
    private int currentCount;

    void Increment()
    {
        currentCount++;
    }
}
```

Every property marked with `[Watch]` will be logged automatically after
renders.

Example output:

    🔄 [INIT] Count: 0
    🔄 [PUSH] Count: 1
    🔄 [PUSH] Count: 2

------------------------------------------------------------------------

# Client Transport Inspection (Browser Console)

Blazor Server uses **binary WebSocket frames (`blazorpack`)**, which are
not readable in browser developer tools.

A useful debugging trick is to monkey‑patch the WebSocket `send` method.

Paste the following snippet into the **browser console**:

``` javascript
(function() {
    const decoder = new TextDecoder();
    const originalSend = WebSocket.prototype.send;

    WebSocket.prototype.send = function(data) {
        console.log("-----------------------------------------");
        console.log("🚀 [SENT TO SERVER] @ " + new Date().toLocaleTimeString());

        try {
            if (data instanceof ArrayBuffer || ArrayBuffer.isView(data)) {
                const decoded = decoder.decode(data).replace(/[^\x20-\x7E]/g, ' ');
                console.log("📄 Content (Decoded):", decoded.trim());
            } else {
                console.log("📄 Content (Plain):", data);
            }
        } catch (e) {
            console.warn("⚠️ Could not decode outgoing binary frame.");
        }

        return originalSend.apply(this, arguments);
    };

    console.log("✅ Outbound SignalR Monitor Active. (Inbound activity is visible through server logs).");
})();
```

This logs **client → server WebSocket frames** triggered by UI events.

Example console output:

    🚀 [SENT TO SERVER] @ 17:45:12
    📄 Content (Decoded): click button

The binary protocol cannot be fully decoded, but this still reveals:

-   event triggers
-   invocation activity
-   approximate payload structure
