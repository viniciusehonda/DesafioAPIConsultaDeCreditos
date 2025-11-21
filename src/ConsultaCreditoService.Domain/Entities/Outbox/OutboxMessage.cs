using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCreditoService.Domain.Entities;
public class OutboxMessage
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public string Topic { get; } = default!;
    public string Payload { get; } = default!;
    public DateTime? ProcessedAt { get; private set; }

    private OutboxMessage() { }

    public OutboxMessage(Guid id, DateTime createdAt, string topic, string payload)
    {
        Id = id;
        CreatedAt = createdAt;
        Topic = topic;
        Payload = payload;
    }

    public void Process()
    {
        ProcessedAt = DateTime.UtcNow;
    }

    public static OutboxMessage Create(Guid id, string topic, string payload)
    {
        return new OutboxMessage(id, DateTime.UtcNow, topic, payload);
    }
}
