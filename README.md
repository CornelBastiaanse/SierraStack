# SierraStack.Mediator

**SierraStack.Mediator** is a lightweight, extensible in-process mediator for .NET. It provides a clean way to handle request/response messaging, supports pipeline behaviors, and is designed to be a modern, open-source alternative to MediatR — built for performance, simplicity, and testability.

---

## ✨ Features

- ✅ Request/Response messaging with `IRequest<T>` and `IRequestHandler<TRequest, TResponse>`
- ✅ In-process event publishing via `INotification`
- ✅ Built-in pipeline behaviors for:
    - Logging
    - Validation (FluentValidation)
    - Retry (Polly)
    - Caching
    - Performance measurement
    - Exception handling
- ✅ Zero external dependencies in core
- ✅ Seamless integration with Microsoft.Extensions.DependencyInjection

---


## 📦 Installation

Coming soon on NuGet:

```bash
dotnet add package SierraStack.Mediator
dotnet add package SierraStack.Mediator.Behaviors
dotnet add package SierraStack.Mediator.Extensions.Microsoft
```

📌 Until published, clone this repo and reference the projects directly.

## 🚀 Getting Started
### 1. Define a request
```csharp
public class Ping : IRequest<string> 
{
    public string Message { get; set; } = "Ping!";
}
```
### 2. Define a handler
```csharp
public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> HandleAsync(Ping request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Pong: {request.Message}");
    }
}
```
### 3. Register services (using built-in DI)
```csharp
services.AddSierraStackMediator(typeof(PingHandler).Assembly);
services.AddSierraStackBehaviors(); // optional
```
### 4. Use the mediator
```csharp
var result = await mediator.SendAsync(new Ping());
// result => "Pong: Ping!"
```

## 🔌 Available Behaviors
Install ```SierraStack.Mediator```.Behaviors and register built-in behaviors:
```csharp
services.AddSierraStackBehaviors();
```

Or register them individually:
```csharp
services
    .AddLoggingBehavior()
    .AddValidationBehavior()
    .AddRetryBehavior()
    .AddCachingBehavior()
    .AddPerformanceBehavior()
    .AddExceptionHandlingBehavior();
```

## 🧪 Testing
All core components and built-in behaviors are covered with unit tests using xUnit. To run:
```bash
dotnet test
```

## 📚 Roadmap
- [X] Core mediator interfaces and implementation
- [X] Microsoft.Extensions.DependencyInjection integration
- [X] Built-in behaviors (logging, retry, validation, etc.)
- [X] Unit test coverage for all behaviors
- [ ] Optional source generators
- [ ] Own validation abstraction
- [ ] Roslyn analyzers (usage hints)
- [ ] Benchmarks vs MediatR

## 📄 License
Licensed under the [MIT License](LICENSE).

## 👋 Contributing
Contributions, suggestions, and ideas are welcome!
Please open an issue or pull request to get started.

## 🔗 Repository
https://github.com/CornelBastiaanse/SierraStack