using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.Logging;

namespace Blazor.Diagnostics.Circuit;

public class BlazorTrafficMonitor : CircuitHandler
{
    private readonly ILogger<BlazorTrafficMonitor> _logger;

    public BlazorTrafficMonitor(ILogger<BlazorTrafficMonitor> logger)
    {
        _logger = logger;
    }

    public override Task OnCircuitOpenedAsync(
        Microsoft.AspNetCore.Components.Server.Circuits.Circuit circuit,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("🟢 Circuit {CircuitId} connected.", circuit.Id);
        return Task.CompletedTask;
    }

    public override Func<CircuitInboundActivityContext, Task> CreateInboundActivityHandler(
        Func<CircuitInboundActivityContext, Task> next)
    {
        return async context =>
        {
            _logger.LogInformation(
                "📥 [TRANSPORT] Inbound activity started on circuit {CircuitId}",
                context.Circuit.Id);

            await next(context);

            _logger.LogInformation(
                "📤 [TRANSPORT] Server-side processing finished on circuit {CircuitId}",
                context.Circuit.Id);
        };
    }

    public override Task OnCircuitClosedAsync(
        Microsoft.AspNetCore.Components.Server.Circuits.Circuit circuit,
        CancellationToken cancellationToken)
    {
        _logger.LogWarning("🔴 Circuit {CircuitId} disconnected.", circuit.Id);
        return Task.CompletedTask;
    }
}