using Microsoft.EntityFrameworkCore;
using PagosService.Application.Interfaces;
using PagosService.Infrastructure.Data;
using PagosService.Infrastructure.MySQL;
using PagosService.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var connectionString = builder.Configuration.GetConnectionString("MySql")!;
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<IMongoLogger, MongoLogger>();
builder.Services.AddScoped<IPagoRepository, PagoMySqlAdapter>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("PermitirFrontend");

app.UseAuthorization();
app.MapControllers();

app.Run();
