using ConsultaCreditoService.Application;
using ConsultaCreditoService.Infrastructure;
using ConsultaCreditoService.Worker;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddApplication()
    .AddWorkerInfrastructure(builder.Configuration);

builder.Services.AddHostedService<Worker>();

IHost host = builder.Build();
await host.RunAsync();
