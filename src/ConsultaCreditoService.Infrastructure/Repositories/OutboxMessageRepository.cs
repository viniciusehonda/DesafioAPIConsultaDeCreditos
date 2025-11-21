using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Domain.Repository;
using ConsultaCreditoService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ConsultaCreditoService.Infrastructure.Repositories;
public class OutboxMessageRepository(ConsultaCreditoServiceDbContext consultaCreditoServiceDbContext) : IOutboxMessageRepository
{
    public async Task<OutboxMessage> GetNextMessage(CancellationToken cancellationToken = default)
    {
        return await consultaCreditoServiceDbContext.Set<OutboxMessage>()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ProcessedAt == null, cancellationToken);
    }

    public async Task MarkOutboxMessageAsPublished(Guid id, CancellationToken cancellationToken = default)
    {
        OutboxMessage? entity = await consultaCreditoServiceDbContext
            .Set<OutboxMessage>()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (entity == null)
        {
            return;
        }

        entity.Process();
        consultaCreditoServiceDbContext.Update(entity);
        await consultaCreditoServiceDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task PublishMessage(OutboxMessage message, CancellationToken cancellationToken = default)
    {
        await consultaCreditoServiceDbContext.AddAsync(message, cancellationToken);
        await consultaCreditoServiceDbContext.SaveChangesAsync(cancellationToken);
    }
}
