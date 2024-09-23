using BetService.Core.MessagingBroker;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace BetService.Infrastructure.RabbitMQ;

/// <summary>
/// The ConnectionManager class is responsible for managing the creation of communication channels (models) with RabbitMQ,
/// using the IConnectionManager interface for abstraction.
/// </summary>
/// <remarks>
/// It uses the settings provided in MessagingSettings (via dependency injection) to configure the connection factory (_connectionFactory).
/// The CreateChannel() method establishes a connection to the RabbitMQ server and returns a new channel (IModel) for publishing and consuming messages.
/// </remarks>
public class ConnectionManager : IConnectionManager<IModel>
{
    private readonly Dictionary<string, ConnectionFactory> _connectionFactories;

    /// <summary>
    /// Initializes a new instance of the ConnectionManager class with the provided settings.
    /// </summary>
    /// <param name="settings">The messaging settings containing information such as the RabbitMQ hostname.</param>
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

    /// <summary>
    /// Creates a communication channel (IModel) with RabbitMQ.
    /// </summary>
    /// <returns>Returns an IModel instance used for publishing and consuming messages.</returns>
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