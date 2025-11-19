using ConsultaCreditoService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCreditoService.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ConsultaCreditoServiceDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ConsultaCreditoServiceDbContext>();

        dbContext.Database.Migrate();
    }
}
