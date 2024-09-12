using DrawService.Core.MessagingBroker;
using DrawService.Infrastructure.RabbitMQ;
using DrawService.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<MessagingSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddSingleton<IMessageSender, MessageSender>();
builder.Services.AddSingleton<ConnectionManager>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();