using System.Text;
using BetService.Core.MessagingBroker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace BetService.Infrastructure.RabbitMQ;

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;
    private readonly string _hostName;
    private readonly string _queueName;
    private readonly ConnectionManager _connectionManager;

    public MessageService(ILogger<MessageService> logger, IOptions<MessagingSettings> settings, ConnectionManager connectionManager)
    {
        _logger = logger;
        _hostName = settings.Value.HostName;
        _queueName = settings.Value.QueueName;
        _connectionManager = connectionManager;
    }

    public void Publish(string message)
    {
        try
        {
            var channel = _connectionManager.CreateChannel();
            
            channel.ExchangeDeclare("userBet", ExchangeType.Fanout);
            channel.QueueDeclare(queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            channel.QueueBind(queue: _queueName,
                exchange: "userBet",
                routingKey: string.Empty);
            
            var body = Encoding.UTF8.GetBytes(message);
            
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            
            channel.BasicPublish(exchange: "userBet",
                routingKey: string.Empty,
                basicProperties: properties,
                body: body);
            
            _logger.LogInformation("Message sent to {Queue}", _queueName);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending message to {Queue}", _queueName);
        }
    }

    public void Receive()
    {
    }
}