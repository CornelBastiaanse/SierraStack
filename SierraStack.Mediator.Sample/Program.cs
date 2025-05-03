using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Core;
using SierraStack.Mediator.Extensions.Microsoft.DependencyInjection;
using SierraStack.Mediator.Pipeline;
using SierraStack.Mediator.Sample.Behaviors;
using SierraStack.Mediator.Sample.RequestHandlers;
using SierraStack.Mediator.Sample.Requests;

// Set up DI
var services = new ServiceCollection();

// Add the mediator
services.AddSierraStackMediator(typeof(PingHandler).Assembly);

// Add a behavior
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

var provider = services.BuildServiceProvider();

// Resolve the mediator
var mediator = provider.GetRequiredService<IMediator>();

// Send a request
var request = new Ping { Message = "Hello, world!" };
var response = await mediator.SendAsync(request);
Console.WriteLine(response); // Output: Pong: Hello, world!