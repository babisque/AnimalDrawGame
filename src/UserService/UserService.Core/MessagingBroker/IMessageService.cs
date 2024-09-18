namespace UserService.Core.MessagingBroker;

public interface IMessageService
{
    void PublishMessage(string queueName, string message);
    List<string> ConsumeMessages(string queueName);
}