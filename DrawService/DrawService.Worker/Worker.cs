namespace DrawService.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    private readonly TimeSpan[] _runTimes =
    [
        new TimeSpan(11, 30, 0),
        new TimeSpan(14, 30, 0),
        new TimeSpan(18, 30, 0),
        new TimeSpan(21, 30, 0)
    ];

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var currentTime = DateTime.Now.TimeOfDay;
            
            if (!_runTimes.Any(time => Math.Abs((currentTime - time).TotalMinutes) < 1))
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
        
        
        await Task.Delay(1000, stoppingToken);
    }
}