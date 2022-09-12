using Dapr.Client;
using Dapr.Example.Dotnet.Frontend.Models;
using Dapr.Example.Dotnet.Frontend.Services.EventCatalog;
using Dapr.Example.Dotnet.Frontend.Services.Ordering;
using Dapr.Example.Dotnet.Frontend.Services.Ordering.InMemory;
using Dapr.Example.Dotnet.Frontend.Services.ShoppingBasket;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
if (String.IsNullOrEmpty(daprPort))
{
    // we're not running in DAPR - use regular service invocation and an in-memory basket
    Console.WriteLine("NOT USING DAPR - mocks");
    builder.Services.AddSingleton<IEventCatalogService, InMemoryEventCatalogService>();
    builder.Services.AddSingleton<IShoppingBasketService, InMemoryShoppingBasketService>();
    builder.Services.AddSingleton<IOrderSubmissionService, InMemoryOrderSubmissionService>();
}
else
{
    Console.WriteLine("USING DAPR");
    builder.Services.AddDaprClient();
  
    builder.Services.AddSingleton<IEventCatalogService>(sp=> 
        new EventCatalogService(DaprClient.CreateInvokeHttpClient("catalog"), sp.GetService<ILogger<EventCatalogService>>()));
    builder.Services.AddScoped<IShoppingBasketService, ShoppingBasketService>();
    builder.Services.AddScoped<IOrderSubmissionService, OrderSubmissionService>();
}
builder.Services.AddSingleton<Settings>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();