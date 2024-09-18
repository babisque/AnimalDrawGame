using UserService.Core.MessagingBroker;
using UserService.Infrastructure.RabbitMQ;
using UserService.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<MessagingSettings>(builder.Configuration.GetSection("MessagingSettings"));
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();