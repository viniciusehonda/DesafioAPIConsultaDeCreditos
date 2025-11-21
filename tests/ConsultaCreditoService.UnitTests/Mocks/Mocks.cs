using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCreditoService.UnitTests.Mocks;
public static class Mocks
{
    public static ConsultaCreditoServiceDbContext CreateDbContext()
    {
        DbContextOptions<ConsultaCreditoServiceDbContext> options = new DbContextOptionsBuilder<ConsultaCreditoServiceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ConsultaCreditoServiceDbContext(options);
    }
}
