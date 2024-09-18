namespace UserService.Core.MessagingBroker;

public interface IConnectionManager<out T>
{
    T CreateChannel(string queueName);
}