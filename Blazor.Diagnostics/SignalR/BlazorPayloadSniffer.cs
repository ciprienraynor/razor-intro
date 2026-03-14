using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Blazor.Diagnostics.SignalR;

public sealed class BlazorPayloadSniffer : IHubFilter
{
    private readonly ILogger<BlazorPayloadSniffer> _logger;

    public BlazorPayloadSniffer(ILogger<BlazorPayloadSniffer> logger)
    {
        _logger = logger;
    }

    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
    {
        var methodName = invocationContext.HubMethodName;

        var args = string.Join(
            ", ",
            invocationContext.HubMethodArguments.Select(a => a?.ToString() ?? "null"));

        _logger.LogInformation("🕵️ [HUB CALL] {Method}({Args})", methodName, args);

        return await next(invocationContext);
    }
}