![Build](https://github.com/CornelBastiaanse/SierraStack/actions/workflows/ci.yml/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/SierraStack.Mediator.svg)](https://www.nuget.org/packages/SierraStack.Mediator)
[![NuGet](https://img.shields.io/nuget/v/SierraStack.Mediator.Behaviors.svg)](https://www.nuget.org/packages/SierraStack.Mediator.Behaviors)
[![NuGet](https://img.shields.io/nuget/v/SierraStack.Mediator.Extensions.Microsoft.svg)](https://www.nuget.org/packages/SierraStack.Mediator.Extensions.Microsoft)
[![License: MIT](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

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

## 🗺️ Roadmap

SierraStack.Mediator is actively evolving toward becoming a robust and extensible in-process messaging library. Below is the full roadmap, broken down by core features, extended functionality, testing infrastructure, distribution, and community goals.

---

### ✅ Core (v1.0)

- [x] Basic mediator with `IRequest<T>` / `INotification`
- [x] Handler registration via `Microsoft.Extensions.DependencyInjection`
- [x] Built-in pipeline support
- [x] Built-in behaviors:
  - [x] Logging
  - [x] Validation (via FluentValidation)
  - [x] Retry (via Polly)
  - [x] Caching (with `IMemoryCache`)
  - [x] Performance measurement
  - [x] Exception handling
- [x] Full unit test coverage for all behaviors and core
- [x] DI extension methods for seamless integration
- [x] NuGet packaging with consistent metadata
- [x] MIT License and detailed README

---

### 🔄 Extended Features (v1.1+)

- [ ] Support for `IRequestPreProcessor<T>` and `IRequestPostProcessor<T>`
- [ ] Custom validator abstraction (`IValidator<T>`) to replace FluentValidation
- [ ] Source generator for compile-time handler resolution
- [ ] Roslyn Analyzer to enforce best practices and flag missing handlers
- [ ] Command batching / aggregate unit-of-work behaviors
- [ ] Pipeline graph dumping / profiling support
- [ ] Timeout behavior to fail slow requests fast
- [ ] Circuit breaker behavior (via Polly)
- [ ] Optional metrics integration (e.g., OpenTelemetry, App Insights)
- [ ] Correlation ID propagation across pipelines
- [ ] CancellationToken-aware pipeline behavior

---

### 🧪 Developer & Testing Tools

- [ ] GitHub Actions CI pipeline
- [ ] Benchmarks vs MediatR using BenchmarkDotNet
- [ ] Coverage reports via Coverlet + ReportGenerator
- [ ] Manual DI registration sample (framework-agnostic example)

---

### 📦 Distribution & Ecosystem

- [ ] Publish all stable packages to NuGet
- [ ] Add `samples/` folder with working app(s)
- [ ] CLI tool to scaffold handlers/requests (optional)
- [ ] Optional Blazor or MAUI integration samples
- [ ] Documentation site (e.g., GitHub Pages or docsify)

---

### 🤝 Community & Collaboration

- [ ] Add `CONTRIBUTING.md` guide
- [ ] Add issue and PR templates
- [ ] Enable GitHub Discussions
- [ ] Label roadmap issues with `[roadmap]` tag
- [ ] Add sponsor/roadmap badge support (optional)

---

Have suggestions? Open a [discussion](https://github.com/CornelBastiaanse/SierraStack/discussions) or [issue](https://github.com/CornelBastiaanse/SierraStack/issues) to shape the future of SierraStack!

## 📄 License
Licensed under the [MIT License](LICENSE).

## 👋 Contributing
Contributions, suggestions, and ideas are welcome!
Please open an issue or pull request to get started.

## 🔗 Repository
https://github.com/CornelBastiaanse/SierraStack