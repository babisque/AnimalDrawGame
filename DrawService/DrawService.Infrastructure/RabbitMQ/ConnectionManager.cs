using DrawService.Core.MessagingBroker;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace DrawService.Infrastructure.RabbitMQ;

public class ConnectionManager : IConnectionManager<IModel>
{
    private readonly ConnectionFactory _factory;

    public ConnectionManager(IOptions<MessagingSettings> settings)
    {
        var hostname = settings.Value.HostName;
        _factory = new ConnectionFactory { HostName = hostname };
    }

    public IModel CreateChannel()
    {
        var connection = _factory.CreateConnection();
        return connection.CreateModel();
    }
}