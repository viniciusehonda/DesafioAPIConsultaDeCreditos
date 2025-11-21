using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Domain.Entities;
using ConsultaCreditoService.Domain.Entities.Outbox;
using ConsultaCreditoService.Domain.Repository;
using ConsultaCreditoService.Infrastructure.Messaging;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace ConsultaCreditoService.Application.Abstractions.Behaviors;
internal static class LoggingDecorator
{
    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> innerHandler,
        IOutboxMessageRepository outboxRepository)
        : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            string queryName = typeof(TQuery).Name;

            Result<TResponse> result = await innerHandler.Handle(query, cancellationToken);

            var message = OutboxMessage.Create(Guid.NewGuid(), OutboxMessageTopics.OutboxQueryAuditTopic, JsonSerializer.Serialize(new QueryAuditDto(queryName, result.IsSuccess, result.Error)));
            await outboxRepository.PublishMessage(message, cancellationToken);

            return result;
        }
    }
}
