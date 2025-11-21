using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCreditoService.Infrastructure.Messaging;
public interface IAzureServiceBusPublisher
{
    Task PublishAsync<T>(string topic, T data);
}
