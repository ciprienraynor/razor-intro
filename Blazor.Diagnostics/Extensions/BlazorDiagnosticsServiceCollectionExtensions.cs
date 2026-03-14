using Blazor.Diagnostics.Circuit;
using Blazor.Diagnostics.SignalR;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Diagnostics.Extensions;

public static class BlazorDiagnosticsServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorDiagnostics(this IServiceCollection services)
    {
        services.AddSignalR(options =>
        {
            options.AddFilter<BlazorPayloadSniffer>();
        });

        services.AddScoped<CircuitHandler, BlazorTrafficMonitor>();

        return services;
    }
}