using System.Text;
using DrawService.Core.MessagingBroker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace DrawService.Infrastructure.RabbitMQ;

public class MessageSender : IMessageSender
{
    private readonly ILogger<MessageSender> _logger;
    private readonly string _hostName;
    private readonly string _queueName;
    private readonly ConnectionManager _connectionManager;

    public MessageSender(ILogger<MessageSender> logger, IOptions<MessagingSettings> settings, ConnectionManager connectionManager)
    {
        _logger = logger;
        _hostName = settings.Value.HostName;
        _queueName = settings.Value.QueueName;
        _connectionManager = connectionManager;
    }

    
    public void Send(string message)
    {
        try
        {
            var channel = _connectionManager.CreateChannel();
            channel.QueueDeclare(queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: string.Empty,
                routingKey: _queueName,
                basicProperties: properties,
                body: body);

            _logger.LogInformation(" [x] Sent {message}", message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, " [x] Failed to send {message}", message);
        }
    }
}