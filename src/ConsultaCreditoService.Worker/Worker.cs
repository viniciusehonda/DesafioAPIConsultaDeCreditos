using System.Reflection.Metadata;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using ConsultaCreditoService.Application.Abstractions.Messaging;
using ConsultaCreditoService.Application.Credito.AdicionarCreditoConstituido;
using ConsultaCreditoService.Domain.Entities;
using Microsoft.Extensions.Hosting;

namespace ConsultaCreditoService.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _interval;
    private readonly ServiceBusClient _client;
    private readonly ServiceBusReceiver _receiver;

    public Worker(ILogger<Worker> logger,
        IConfiguration configuration,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _interval = TimeSpan.FromMilliseconds(500);
        _client = new ServiceBusClient(configuration["AzureServiceBus:ConnectionString"]);

        string topic = CreditoMessageTopics.IntegrarCreditoConstituidoEntry;
        string subscription = "ConsultaCreditoSubscription";

        _receiver = _client.CreateReceiver(topic, subscription, new ServiceBusReceiverOptions
        {
            ReceiveMode = ServiceBusReceiveMode.PeekLock
        });
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started at: {Time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
                }

                await Task.Delay(_interval, stoppingToken);

                ServiceBusReceivedMessage msg = await _receiver.ReceiveMessageAsync(
                    maxWaitTime: TimeSpan.FromSeconds(1),
                    cancellationToken: stoppingToken
                );

                if (msg == null)
                {
                    continue;
                }

                string body = msg.Body.ToString();

                try
                {
                    List<Credito>? data = JsonSerializer.Deserialize<List<Credito>>(body);

                    if (data == null)
                    {
                        await _receiver.DeadLetterMessageAsync(msg, "processing_error", "Message body is null.", stoppingToken);
                        continue;
                    }

                    using IServiceScope scope = _scopeFactory.CreateScope();
                    ICommandHandler<AdicionarCreditoConstituidoCommand> handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<AdicionarCreditoConstituidoCommand>>();

                    await handler.Handle(new AdicionarCreditoConstituidoCommand(data), stoppingToken);

                    await _receiver.CompleteMessageAsync(msg, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message.");
                    await _receiver.DeadLetterMessageAsync(msg, "processing_error", ex.Message, stoppingToken);
                }
            }
            catch (TaskCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while receiving messages.");
            }
        }

        await _receiver.DisposeAsync();
        await _client.DisposeAsync();
    }
}
