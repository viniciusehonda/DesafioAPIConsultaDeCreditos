
using ConsultaCreditoService.Domain.Entities.Outbox;
using ConsultaCreditoService.Domain.Repository;
using ConsultaCreditoService.Infrastructure.Messaging;
using Microsoft.Azure.Amqp.Framing;

namespace ConsultaCreditoService.Api.Workers;

public class OutboxWorker : BackgroundService
{
    private readonly ILogger<OutboxWorker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IAzureServiceBusPublisher _publisher;
    private readonly TimeSpan _interval;

    public OutboxWorker(ILogger<OutboxWorker> logger,
        IConfiguration configuration,
        IAzureServiceBusPublisher publisher,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _publisher = publisher;
        _interval = TimeSpan.FromMilliseconds(500);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("OutboxWorker started at: {Time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("OutboxWorker running at: {Time}", DateTimeOffset.Now);
                }

                await Task.Delay(_interval, stoppingToken);

                using IServiceScope scope = _scopeFactory.CreateScope();

                IOutboxMessageRepository repository = scope.ServiceProvider.GetRequiredService<IOutboxMessageRepository>();

                Domain.Entities.OutboxMessage currentMessage = await repository.GetNextMessage(stoppingToken);

                if (currentMessage == null)
                {
                    continue;
                }

                await _publisher.PublishAsync(currentMessage.Topic, currentMessage.Payload);
                await repository.MarkOutboxMessageAsPublished(currentMessage.Id, stoppingToken);

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("OutboxWorker message processed: {Time}", DateTimeOffset.Now);
                }
            }
            catch (TaskCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while sending the messages from outbox.");
            }
        }
    }
}
