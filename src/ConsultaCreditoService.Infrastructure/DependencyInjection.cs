using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Domain.Repository;
using ConsultaCreditoService.Infrastructure.Database;
using ConsultaCreditoService.Infrastructure.Messaging;
using ConsultaCreditoService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ConsultaCreditoService.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
         services.AddServices()
            .AddRepositories()
            .AddDatabase(configuration)
            .AddHealthChecks(configuration);

    public static IServiceCollection AddWorkerInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
         services.AddServices()
            .AddRepositories()
            .AddDatabase(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IAzureServiceBusPublisher, AzureServiceBusPublisher>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICreditoRepository, CreditoRepository>();
        services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ConsultaCreditoServiceDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), tags: ["self"])
            .AddNpgSql(configuration.GetConnectionString("Database")!, tags: ["ready"])
            .AddAzureServiceBusTopic(
                connectionString: configuration["AzureServiceBus:ConnectionString"] ?? "",
                topicName: CreditoMessageTopics.IntegrarCreditoConstituidoEntry,
                tags: ["ready"]
            );

        return services;
    }
}
