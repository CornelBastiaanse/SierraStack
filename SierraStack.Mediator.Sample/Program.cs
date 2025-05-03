using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Core;
using SierraStack.Mediator.Extensions.Microsoft.DependencyInjection;
using SierraStack.Mediator.Sample.RequestHandlers;
using SierraStack.Mediator.Sample.Requests;

// Set up DI
var services = new ServiceCollection();

// Add the mediator
services.AddSierraStackMediator(typeof(PingHandler).Assembly);

var provider = services.BuildServiceProvider();

// Resolve the mediator
var mediator = provider.GetRequiredService<IMediator>();

// Send a request
var response = await mediator.SendAsync(new Ping { Message = "Hello, world!" });
Console.WriteLine(response); // Output: Pong: Hello, world!