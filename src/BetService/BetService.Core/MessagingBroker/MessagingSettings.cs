namespace BetService.Core.MessagingBroker;

public class MessagingSettings
{
    public List<QueueSettings> Queues { get; set; } = [];
}