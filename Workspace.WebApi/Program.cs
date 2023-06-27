using System.Text.Json.Serialization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Workspace.DAL;
using Workspace.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureSwagger();
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.ConfigureManagers();

builder.Services.AddCors();

var app = builder.Build();

using var serviceScope = app.Services.CreateScope();

try
{
    RuntimeMigrations.Migrate(serviceScope.ServiceProvider);
}
catch (Exception exc)
{
    app.Logger.LogError(message: "Ошибка миграции базы данных: {exc}", exc);
    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddExceptionMiddleware();

app.UseCors(builder => builder.WithOrigins("http://localhost:8080", "http://localhost")
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.None
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
