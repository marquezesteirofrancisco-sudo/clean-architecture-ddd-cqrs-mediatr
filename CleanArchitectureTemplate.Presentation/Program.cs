using CleanArchitectureTemplate.Application.UseCases;
using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Infraestructure.Data;
using CleanArchitectureTemplate.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// 2. 🌟 LA LÍNEA QUE TE FALTA: Registrar Identity
//builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();


// **** INICIO INYECCION DE PRODUCTOS ****
// dependencia de nuestra logica de negocio
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>(); //>>> sin MediatR

// Configurar MediatR
builder.Services.AddMediatR(typeof(ProductService).Assembly); // >>> con MediatR
// **** FIN INYECCION DE PRODUCTOS ****


// **** INICIO INYECCION DE CLIENTES ****
builder.Services.AddScoped<IClientesRepository, ClienteRepository>();
builder.Services.AddScoped<ClienteService>(); // >> sin MediatR

// Configurar MediatR
builder.Services.AddMediatR(typeof(ClienteService).Assembly); // >>> con MediatR
// **** FIN INYECCION DE CLIENTES ****

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
