using _02.Components.Composition.Components;
using Blazor.Diagnostics;
using Blazor.Diagnostics.Circuit;
using Blazor.Diagnostics.SignalR;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Register the filter for ALL hubs (including the internal Blazor one)
builder.Services.AddSignalR(options =>
{
    options.AddFilter<BlazorPayloadSniffer>();
});

builder.Services.AddScoped<CircuitHandler, BlazorTrafficMonitor>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();