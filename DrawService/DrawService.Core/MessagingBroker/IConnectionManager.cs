namespace DrawService.Core.MessagingBroker;

public interface IConnectionManager<out T>
{
    T CreateChannel();
}