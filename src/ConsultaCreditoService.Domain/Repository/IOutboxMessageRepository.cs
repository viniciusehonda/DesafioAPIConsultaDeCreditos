using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaCreditoService.Domain.Entities;

namespace ConsultaCreditoService.Domain.Repository;
public interface IOutboxMessageRepository
{
    Task PublishMessage(OutboxMessage message, CancellationToken cancellationToken = default);
}
