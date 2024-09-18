using RabbitMQ.Client;
using UserService.Core.MessagingBroker;
using Microsoft.Extensions.Options;

namespace UserService.Infrastructure.RabbitMQ;

public class ConnectionManager : IConnectionManager<IModel>
{
    private readonly Dictionary<string, ConnectionFactory> _connectionFactories;

    public ConnectionManager(IOptions<MessagingSettings> settings)
    {
        _connectionFactories = new Dictionary<string, ConnectionFactory>();
        
        foreach (var queueSettings in settings.Value.Queues)
        {
            _connectionFactories[queueSettings.Name] = new ConnectionFactory
            {
                HostName = queueSettings.HostName
            };
        }
    }

    public IModel CreateChannel(string queueName)
    {
        if (_connectionFactories.TryGetValue(queueName, out var connectionFactory))
        {
            var connection = connectionFactory.CreateConnection();
            return connection.CreateModel();
        }

        throw new ArgumentException($"Queue '{queueName}' not found in configuration.");
    }
}