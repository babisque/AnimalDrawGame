namespace UserService.Core.MessagingBroker;

public class MessagingSettings
{
    public List<QueueSettings> Queues { get; set; } = [];
}