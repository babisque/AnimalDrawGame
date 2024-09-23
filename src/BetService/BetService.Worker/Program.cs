using BetService.Core.MessagingBroker;
using BetService.Worker;
using BetService.Worker.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<MessagingSettings>(builder.Configuration.GetSection("MessagingSettings"));
builder.Services.AddHostedService<BetValidationWorker>();

var host = builder.Build();
host.Run();