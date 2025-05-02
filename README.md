# SierraStack

**SierraStack** is a modular .NET framework built for clean, maintainable architecture. It starts with a powerful mediator library for in-process messaging—ideal for CQRS, request/response, and event-driven designs.

## 🔍 Overview

SierraStack.Mediator provides a simple and extensible alternative to MediatR:

- Minimal dependencies
- Clean request/response & notification pattern
- Built-in pipeline behaviors (logging, validation, etc.)
- Full DI integration with Microsoft.Extensions.DependencyInjection
- Foundation for a larger full-stack service framework

## 📦 Packages

- `SierraStack.Mediator` – Core mediator logic
- `SierraStack.Mediator.Extensions.Microsoft` – Extensions for .NET DI container

## 📁 Example

```csharp
public class Ping : IRequest<string> { }

public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        => Task.FromResult("Pong");
}

var services = new ServiceCollection();
services.AddSierraStackMediator(typeof(PingHandler).Assembly);
```

## 🚀 Roadmap

### ✅ Core Features
- [ ] Basic mediator (Send/Publish)
- [ ] Request/Response handlers
- [ ] Notification handlers
- [ ] DI support via Microsoft.Extensions.DependencyInjection

### 🔄 Pipeline Behaviors
- [ ] Logging behavior
- [ ] Validation behavior
- [ ] Retry behavior
- [ ] Authorization behavior (optional)

### 🔌 Pre/Post Processors
- [ ] Request pre-processors
- [ ] Response post-processors

### 📦 Packaging & Ecosystem
- [ ] NuGet packaging
- [ ] Separate abstractions project
- [ ] Example console/web apps

### 🧪 Testing & Performance
- [ ] Unit test coverage
- [ ] Benchmarks

### 🔧 Advanced Features (Future)
- [ ] Request cancellation support
- [ ] Scoped & open generic resolution improvements
- [ ] Request decorators

### 🧱 Long-Term Vision
- [ ] HTTP/RPC transport layer
- [ ] Serialization support
- [ ] Hosting/runtime infrastructure
- [ ] Full-stack service framework (SierraStack.Web, SierraStack.Rpc, etc.)


## 📦 Installation

SierraStack.Mediator will soon be available on NuGet.

Once published, you’ll be able to install it via the .NET CLI:

```bash
dotnet add package SierraStack.Mediator
dotnet add package SierraStack.Mediator.Extensions.Microsoft
```

Or via the NuGet Package Manager:

```mathematica
Install-Package SierraStack.Mediator
Install-Package SierraStack.Mediator.Extensions.Microsoft
```
