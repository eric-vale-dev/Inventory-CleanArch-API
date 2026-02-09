using Inventario.Infrastructure.Persistence;
using Inventario.Infrastructure.Repositories;
using Inventario.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Inventario.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// 1. OBTENER LA CADENA DE CONEXIÓN
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. CONECTAR A POSTGRES
// Le decimos a la app que use el DbContext que creamos en Infrastructure
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. INYECTAR EL REPOSITORIO
// Cada vez que alguien pida "IProductoRepository", dale un "ProductoRepository"
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Activar la validación automática de FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Registrar todos los validadores que estén en el mismo proyecto que 'ProductoValidator'
builder.Services.AddValidatorsFromAssemblyContaining<ProductoValidator>();

builder.Services.AddControllers();

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();