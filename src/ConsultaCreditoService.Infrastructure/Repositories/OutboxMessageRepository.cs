using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Domain.Repository;
using ConsultaCreditoService.Infrastructure.Database;

namespace ConsultaCreditoService.Infrastructure.Repositories;
public class OutboxMessageRepository(ConsultaCreditoServiceDbContext consultaCreditoServiceDbContext) : IOutboxMessageRepository
{
    public async Task PublishMessage(OutboxMessage message, CancellationToken cancellationToken = default)
    {
        await consultaCreditoServiceDbContext.AddAsync(message, cancellationToken);
        await consultaCreditoServiceDbContext.SaveChangesAsync(cancellationToken);
    }
}
