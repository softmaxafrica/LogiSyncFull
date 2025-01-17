using LogiSyncWebApi.Server.Services;
using LogiSyncWebApi.Server.Shared;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.

builder.Services.AddControllers();
 
 builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB_Connection")));

builder.Services.AddSingleton<IHostedService, ChargesTriggerService>();

builder.Services.AddSingleton<IHostedService, InvoicingService>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*")
                .AllowAnyHeader()
               .AllowAnyMethod();

    });
});

var app = builder.Build();

 
    app.UseSwagger();
    app.UseSwaggerUI();
 
app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
