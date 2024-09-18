using UserService.Core.MessagingBroker;

namespace UserService.Worker.Workers;

public class BetValidationWorker : BackgroundService
{
    /**
     * TODO: Serialize the message into a object to validate the message
     */
    
    private readonly ILogger<BetValidationWorker> _logger;
    private readonly IMessageService _messageService;

    public BetValidationWorker(ILogger<BetValidationWorker> logger, IMessageService messageService)
    {
        _logger = logger;
        _messageService = messageService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var betsToValidate = _messageService.ConsumeMessages("UserQueue");
        if (betsToValidate.Count != 0)
        {
            
        }
    }
}