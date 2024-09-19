using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using UserService.Core.DTO.Messages;
using UserService.Core.Entities;
using UserService.Core.MessagingBroker;

namespace UserService.Worker.Workers;

public class BetValidationWorker : BackgroundService
{
    private readonly ILogger<BetValidationWorker> _logger;
    private readonly IMessageService _messageService;
    private readonly UserManager<ApplicationUser> _userManager;

    public BetValidationWorker(ILogger<BetValidationWorker> logger, IMessageService messageService, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _messageService = messageService;
        _userManager = userManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var betsToValidate = _messageService.ConsumeMessages("UserQueue");

            if (betsToValidate.Count == 0)
            {
                _logger.LogInformation("No bets to validate at this moment.");
            }
            else
            {
                foreach (var message in betsToValidate)
                {
                    var bet = JsonConvert.DeserializeObject<BetValidationMessage>(message);
                    if (bet == null)
                    {
                        _logger.LogError("Failed to deserialize bet validation message.");
                        continue;
                    }

                    var user = await _userManager.FindByIdAsync(bet.UserId);
                    if (user == null)
                    {
                        _logger.LogWarning($"User with ID {bet.UserId} not found.");
                        continue;
                    }

                    if (user.Balance < bet.BetAmount)
                    {
                        _logger.LogWarning($"User {bet.UserId} has insufficient balance for bet amount {bet.BetAmount}.");
                        continue;
                    }

                    _messageService.PublishMessage("UserBetValidation", JsonConvert.SerializeObject(bet));
                    _logger.LogInformation($"Bet for user {bet.UserId} has been validated and published.");
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
