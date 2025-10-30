using BugStore.Data;
using BugStore.Data.Repositories.CustomerRepository;
using BugStore.Extensions;
using BugStore.Infra;
using BugStore.Models;
using BugStore.Services.CostumerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddSqliteDatabase(builder.Configuration);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/v1/customers", async ([FromServices] ICustomerService service, CancellationToken cancellationToken) =>
{
    var result = await service.GetAllAsync(cancellationToken);
    return result.Error is null
        ? Results.Ok(result.Value)
        : Results.Problem(result.Error.Message);
});

app.MapGet("/v1/customers/{id}", async ([FromRoute] Guid id, [FromServices] ICustomerService service, CancellationToken cancellationToken) =>
{
    var result = await service.GetByIdAsync(id, cancellationToken);
    return result.Error is null
        ? Results.Ok(result.Value)
        : Results.Problem(result.Error.Message);
});

app.MapPost("/v1/customers", async ([FromBody] Customer customer, [FromServices] ICustomerService service, CancellationToken cancellationToken) =>
{
    var result = await service.AddAsync(customer, cancellationToken);
    return result.Error is null
        ? Results.Created($"/v1/customers/{customer.Id}", customer)
        : Results.Problem(result.Error.Message);
});

app.MapPut("/v1/customers/{id}", async ([FromRoute] Guid id, [FromBody] Customer customer, [FromServices] ICustomerService service, CancellationToken cancellationToken) =>
{
    var result = await service.UpdateAsync(id, customer, cancellationToken);
    return result.Error is null
        ? Results.NoContent()
        : Results.Problem(result.Error.Message);
});

app.MapDelete("/v1/customers/{id}", async ([FromRoute] Guid id, [FromServices] ICustomerService service, CancellationToken cancellationToken) =>
{
    var result = await service.DeleteAsync(id, cancellationToken);
    return result.Error is null
        ? Results.NoContent()
        : Results.Problem(result.Error.Message);
});

app.MapGet("/v1/products", () => "Hello World!");
app.MapGet("/v1/products/{id}", () => "Hello World!");
app.MapPost("/v1/products", () => "Hello World!");
app.MapPut("/v1/products/{id}", () => "Hello World!");
app.MapDelete("/v1/products/{id}", () => "Hello World!");

app.MapGet("/v1/orders/{id}", () => "Hello World!");
app.MapPost("/v1/orders", () => "Hello World!");

app.Run();
