namespace BetService.Core.MessagingBroker;

public class QueueSettings
{
    public required string Name { get; set; }
    public required string HostName { get; set; }
    public required string Exchange { get; set; }
}