using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SierraStack.Mediator.Behaviors.Extensions;
using SierraStack.Mediator.Core;
using SierraStack.Mediator.Extensions.Microsoft.DependencyInjection;
using SierraStack.Mediator.Sample.Notifications;
using SierraStack.Mediator.Sample.Requests;

var host = Host
    .CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSierraStackMediator(typeof(Program).Assembly);
        services.AddSierraStackBehaviors();
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    })
    .Build();
    
var mediator = host.Services.GetRequiredService<IMediator>();

var request = new Ping { Message = "Hello from SierraStack!" };
var response = await mediator.SendAsync(request);

Console.WriteLine(response);

await mediator.PublishAsync(new Pinged
{
    Source = "PingHandler",
    Timestamp = DateTime.UtcNow
});