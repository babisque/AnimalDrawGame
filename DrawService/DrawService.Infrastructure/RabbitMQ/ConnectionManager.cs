using DrawService.Core.MessagingBroker;
using RabbitMQ.Client;

namespace DrawService.Infrastructure.RabbitMQ;

public class ConnectionManager : IConnectionManager<IModel>
{
    private readonly ConnectionFactory _factory;

    public ConnectionManager(string hostname)
    {
        _factory = new ConnectionFactory { HostName = hostname };
    }

    public IModel CreateChannel()
    {
        var connection = _factory.CreateConnection();
        return connection.CreateModel();
    }
}