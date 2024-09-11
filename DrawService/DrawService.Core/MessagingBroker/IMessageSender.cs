namespace DrawService.Core.MessagingBroker;

public interface IMessageSender
{
    void Send(string message);
}