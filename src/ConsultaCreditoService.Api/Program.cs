using System.Reflection;
using ConsultaCreditoService.Api.Extensions;
using ConsultaCreditoService.Api.Workers;
using ConsultaCreditoService.Application;
using ConsultaCreditoService.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Services.AddHostedService<OutboxWorker>();

WebApplication app = builder.Build();

app.MapEndpoints();

app.ApplyMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/self", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("self")
});

app.MapHealthChecks("/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

//app.UseHttpsRedirection();

await app.RunAsync();
