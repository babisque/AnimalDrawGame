using DrawService.Core.MessagingBroker;
using DrawService.Infrastructure.RabbitMQ;
using DrawService.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<IMessageSender, MessageSender>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();