using ABMProductos.Data;
using ABMProductos.Repositories.Implementations;
using ABMProductos.Repositories.Interfaces;
using ABMProductos.Services.Implementations;
using ABMProductos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ABMProductosContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("ABMProductosDBConnectionString")
    )
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();