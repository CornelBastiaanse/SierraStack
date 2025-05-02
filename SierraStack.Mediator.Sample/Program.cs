using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Core;
using SierraStack.Mediator.Sample.RequestHandlers;
using SierraStack.Mediator.Sample.Requests;

// Set up DI
var services = new ServiceCollection();

// Add the mediator manually
services.AddSingleton<IMediator, Mediator>();

// Register handler explicitly (we'll automate this later)
services.AddTransient<IRequestHandler<Ping, string>, PingHandler>();

var provider = services.BuildServiceProvider();

// Resolve the mediator
var mediator = provider.GetRequiredService<IMediator>();

// Send a request
var response = await mediator.SendAsync(new Ping { Message = "Hello, world!" });
Console.WriteLine(response); // Output: Pong: Hello, world!