using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCreditoService.Domain.Entities.Outbox;
public static class OutboxMessageTopics
{
    public const string OutboxQueryAuditTopic = "query-audit-entry";
}
