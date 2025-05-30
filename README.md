![Build](https://github.com/CornelBastiaanse/SierraStack/actions/workflows/ci.yml/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/SierraStack.Mediator.svg)](https://www.nuget.org/packages/SierraStack.Mediator)
[![NuGet](https://img.shields.io/nuget/v/SierraStack.Mediator.Behaviors.svg)](https://www.nuget.org/packages/SierraStack.Mediator.Behaviors)
[![NuGet](https://img.shields.io/nuget/v/SierraStack.Mediator.Extensions.Microsoft.svg)](https://www.nuget.org/packages/SierraStack.Mediator.Extensions.Microsoft)
[![License: MIT](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

# SierraStack.Mediator

**SierraStack.Mediator** is a lightweight, extensible in-process mediator for .NET. It provides a clean way to handle request/response messaging, supports pipeline behaviors, and is designed to be a modern, open-source alternative to MediatR — built for performance, simplicity, and testability.

---

💬 **We're actively working on v0.2.0 and want your input!**  
📌 Join the conversation in [this pinned issue](https://github.com/sierrastack/SierraStack/issues/4)  
Let us know what you'd love to see next.

## ✨ Features

- ✅ Request/Response messaging with `IRequest<TResponse>` and `IRequestHandler<TRequest, TResponse>`
- ✅ In-process event publishing via `INotification`
- ✅ Built-in pipeline behaviors for:
    - Logging
    - Validation (FluentValidation + Custom Abstraction)
    - Retry (Polly)
    - Caching
    - Performance measurement
    - Exception handling
- ✅ Zero external dependencies in core
- ✅ Seamless integration with Microsoft.Extensions.DependencyInjection

---


## 📦 Installation

Install from NuGet:

```bash
dotnet add package SierraStack.Mediator
dotnet add package SierraStack.Mediator.Behaviors
dotnet add package SierraStack.Mediator.Extensions.Microsoft
```

📌 See NuGet Gallery for all available packages.

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
    .AddValidationBehavior(options =>
    {
        options.ThrowOnFailure = true;
        options.OnFailure = failures =>
        {
            foreach (var failure in failures)
                Console.WriteLine($"Validation failed: {failure.PropertyName} - {failure.ErrorMessage}");
        };
    });
```

Validation uses a SierraStack-specific abstraction and can integrate with FluentValidation or custom providers.

## 🧪 Testing
All core components and built-in behaviors are covered with unit tests using xUnit. To run:
```bash
dotnet test
```

## 📄 License
Licensed under the [MIT License](LICENSE).

## 👋 Contributing
Contributions, suggestions, and ideas are welcome!
Please open an issue or pull request to get started.

## 🔗 Repository
https://github.com/sierrastack/SierraStack

📍 See the [full roadmap](./ROADMAP.md) for upcoming features and plans.

# SierraStack.Mediator Benchmarks

These benchmarks use BenchmarkDotNet to measure mediator performance across common scenarios.

| Scenario               | Mean     | Allocated |
|------------------------|----------|-----------|
| Simple Request         | 633 ns   | 952 B     |
| + Logging Behavior     | TBD      | TBD       |
| + Logging + Retry      | TBD      | TBD       |

Run with:  
```bash
dotnet run -c Release --project SierraStack.Mediator.Benchmarks
```
