using System.Text;
using BetService.Core.MessagingBroker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BetService.Infrastructure.RabbitMQ;

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;
    private readonly ConnectionManager _connectionManager;
    private readonly List<QueueSettings> _queueSettings;

    public MessageService(ILogger<MessageService> logger, IOptions<MessagingSettings> settings, ConnectionManager connectionManager)
    {
        _logger = logger;
        _connectionManager = connectionManager;
        _queueSettings = settings.Value.Queues;
    }

    public void Publish(string queueName, string message)
    {
        var queueSettings = _queueSettings.FirstOrDefault(x => x.Name == queueName);
        if (queueSettings is null)
        {
            _logger.LogError($"Queue '{queueName}' not found in settings.");
            return;
        }
        
        try
        {
            var channel = _connectionManager.CreateChannel(queueName);
            
            channel.ExchangeDeclare(queueSettings.Exchange, ExchangeType.Fanout);
            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            channel.QueueBind(queue: queueName,
                exchange: "userBet",
                routingKey: string.Empty);
            
            var body = Encoding.UTF8.GetBytes(message);
            
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            
            channel.BasicPublish(exchange: "userBet",
                routingKey: string.Empty,
                basicProperties: properties,
                body: body);
            
            _logger.LogInformation("Message sent to {Queue}", queueName);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending message to {Queue}", queueName);
        }
    }

    public List<string> Receive(string queueName)
    {
        var queueSettings = _queueSettings.FirstOrDefault(x => x.Name == queueName);
        if (queueSettings is null)
        {
            _logger.LogError($"Queue '{queueName}' not found in settings.");
            return [];
        }

        try
        {
            List<string> messages = [];
            var channel = _connectionManager.CreateChannel(queueName);
            
            channel.ExchangeDeclare(queueSettings.Exchange, ExchangeType.Fanout);
            
            channel.QueueBind(
                queue: queueName,
                exchange: queueSettings.Exchange,
                routingKey: string.Empty
                );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                messages.Add(message);
            };

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );

            return messages;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error consuming messages from {Queue}", queueName);
            return [];
        }
    }
}