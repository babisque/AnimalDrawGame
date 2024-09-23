using BetService.Core.Dto.Messaging;
using BetService.Core.MessagingBroker;
using BetService.Core.Repositories;
using Newtonsoft.Json;

namespace BetService.Worker.Workers;

public class BetValidationWorker : BackgroundService
{
    private readonly ILogger<BetValidationWorker> _logger;
    private readonly IMessageService _messageService;
    private readonly IBetRepository _betRepository;

    public BetValidationWorker(ILogger<BetValidationWorker> logger, IMessageService messageService, IBetRepository betRepository)
    {
        _logger = logger;
        _messageService = messageService;
        _betRepository = betRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var validatedBets = _messageService.Receive("UserBetValidation");
            if (validatedBets.Count == 0)
                _logger.LogInformation("There are no validated bets.");
            else
            {
                foreach (var validatedBet in validatedBets.Select(JsonConvert.DeserializeObject<BetValidationRes>))
                {
                    if (validatedBets == null)
                    {
                        _logger.LogError("Failed to deserialize bet validation message.");
                        continue;
                    }
                    
                    var bet = _betRepository.GetByIdAsync(validatedBet!.Id).Result;
                    bet.Confirmed = true;

                    await _betRepository.UpdateAsync(bet);
                    _logger.LogInformation("Successfully validated bet.");
                }
            }
        }
        
        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
    }
}