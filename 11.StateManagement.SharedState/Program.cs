using _11.StateManagement.SharedState.App;
using _11.StateManagement.SharedState.Core.Providers;
using _11.StateManagement.SharedState.Features.CartStatus.Presentation.Store;
using _11.StateManagement.SharedState.Features.Catalog.Presentation.Store;
using _11.StateManagement.SharedState.Features.Checkout.Presentation.Store;
using _11.StateManagement.SharedState.Features.ProductDetails.Presentation.Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ShoppingCartProvider>();

builder.Services.AddScoped<CartStatusStore>();
builder.Services.AddScoped<CatalogStore>();
builder.Services.AddScoped<ProductDetailsStore>();
builder.Services.AddScoped<CheckoutStore>();


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
app.MapRazorComponents<AppRoot>()
    .AddInteractiveServerRenderMode();

app.Run();