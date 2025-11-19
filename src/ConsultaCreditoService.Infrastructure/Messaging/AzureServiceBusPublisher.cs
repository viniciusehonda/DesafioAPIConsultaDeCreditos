using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace ConsultaCreditoService.Infrastructure.Messaging;
public class AzureServiceBusPublisher : IDisposable
{
    private readonly ServiceBusClient _serviceBusClient;
    public AzureServiceBusPublisher(IConfiguration config)
    {
        _serviceBusClient = new ServiceBusClient(config["AzureServiceBus:ConnectionString"]);
    }

    public async Task PublishAsync<T>(string topic, T data)
    {
        ServiceBusSender sender = _serviceBusClient.CreateSender(topic);

        string jsonData = JsonSerializer.Serialize(data);
        var message = new ServiceBusMessage(jsonData)
        {
            ContentType = "application/json"
        };

        await sender.SendMessageAsync(message);
    }

    public async void Dispose()
    {
        await _serviceBusClient.DisposeAsync();
    }
}
