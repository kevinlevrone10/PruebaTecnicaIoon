using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIoon.Data;
using PruebaTecnicaIoon.Repositorio.IRepositorio;
using PruebaTecnicaIoon.Repositorio;

var builder = WebApplication.CreateBuilder(args);

//Configuramos la conexion a Sql Server

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
}
);


builder.Services.AddScoped<ICommerceRepository, CommerceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ICommerceRepository, CommerceRepository>();



// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
