using System.Text.Json;
using DrawService.Core.Entities;
using DrawService.Core.MessagingBroker;

namespace DrawService.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Random _rnd = new();
    private readonly IMessageSender _messageSender;
    
    public Worker(ILogger<Worker> logger, IMessageSender messageSender)
    {
        _logger = logger;
        _messageSender = messageSender;
    }

    private readonly TimeSpan[] _runTimes =
    {
        new TimeSpan(11, 30, 0),
        new TimeSpan(14, 30, 0),
        new TimeSpan(18, 30, 0),
        new TimeSpan(21, 30, 0),
    };

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var currentTime = DateTime.Now.TimeOfDay;
            var currentTimeWithoutSeconds = new TimeSpan(currentTime.Hours, currentTime.Minutes, 0);

            if (_runTimes.Any(time => Math.Abs((currentTimeWithoutSeconds - time).TotalMinutes) < 1))
            {
                _logger.LogInformation("Executing service at: {time}", DateTimeOffset.Now);

                await DrawNumbers(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
    
    private async Task DrawNumbers(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker doing work at: {time}", DateTimeOffset.Now);
        
        if (stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker stopping due to cancellation.");
            return;
        }
        
        var drawnNumbers = new DrawnNumbers();

        for (var i = 1; i <= 5; i++)
        {
            var category = new Category
            {
                CategoryName = $"Category {i}",
                Numbers = []
            };
            
            for (var j = 0; j < 4; j++)
            {
                var random = _rnd.Next(0, 10);
                category.Numbers.Add(random);
            }
            drawnNumbers.Categories.Add(category);
            
            _logger.LogInformation("Generated Category {i} with numbers: {numbers}", i, string.Join(", ", category.Numbers));
            
            if (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker stopping due to cancellation.");
                return;
            }
        }
        
        var jsonNumbers = JsonSerializer.Serialize(drawnNumbers);
        _messageSender.Send(jsonNumbers);
        
        await Task.Delay(1000, stoppingToken);
        _logger.LogInformation("Worker finished at: {time}", DateTimeOffset.Now);
    }
}