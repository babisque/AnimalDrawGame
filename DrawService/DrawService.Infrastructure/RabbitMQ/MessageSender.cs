using System.Text;
using DrawService.Core.MessagingBroker;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace DrawService.Infrastructure.RabbitMQ;

public class MessageSender : IMessageSender
{
    private readonly ILogger<MessageSender> _logger;
    private readonly string _hostName;
    private readonly string _queueName;
    private readonly ConnectionManager _connectionManager;

    public MessageSender(ILogger<MessageSender> logger, string hostName, string queueName)
    {
        _logger = logger;
        _hostName = hostName;
        _queueName = queueName;
        _connectionManager = new ConnectionManager(_hostName);
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