namespace BetService.Core.MessagingBroker;

public interface IMessageService
{
    void Publish(string queueName, string message);
    List<string> Receive(string queueName);
}