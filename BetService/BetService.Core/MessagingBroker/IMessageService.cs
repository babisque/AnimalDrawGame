namespace BetService.Core.MessagingBroker;

public interface IMessageService
{
    void Publish(string message);
    void Receive();
}