using _10.StateManagement.Effects.Components;
using _10.StateManagement.Effects.Data.Separation.Remote;
using _10.StateManagement.Effects.Providers.Separation;
using _10.StateManagement.Effects.Providers.Shapes;
using _10.StateManagement.Effects.Services;
using _10.StateManagement.Effects.State;
using _10.StateManagement.Effects.State.Definition;
using _10.StateManagement.Effects.State.Separation;
using _10.StateManagement.Effects.State.Shapes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<ProductApiClient>(client =>
{
    client.BaseAddress = new Uri("https://dummyjson.com/");
});

builder.Services.AddScoped<FakeMessageService>();
builder.Services.AddScoped<DefinitionStore>();
builder.Services.AddScoped<CatalogProvider>();
builder.Services.AddScoped<SeparationStore>();
builder.Services.AddScoped<RequestResponseStore>();
builder.Services.AddScoped<AnalyticsProvider>();
builder.Services.AddScoped<FireAndForgetStore>();

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