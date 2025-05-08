# 🗺️ SierraStack Roadmap

This file tracks the current and planned development for SierraStack.Mediator and its supporting packages.

Have an idea? Open a [discussion](https://github.com/CornelBastiaanse/SierraStack/discussions) or [issue](https://github.com/CornelBastiaanse/SierraStack/issues).

---

## ✅ Core (v0.1.x)

- [x] Core `IMediator` implementation
- [x] `IRequest<TResponse>`, `INotification`, and DI integration
- [x] Pipeline behavior support
- [x] Built-in behaviors:
    - [x] Logging
    - [x] Validation (FluentValidation)
    - [x] Retry (Polly)
    - [x] Caching
    - [x] Performance tracking
    - [x] Exception handling
- [x] Full unit test coverage
- [x] Microsoft.Extensions.DependencyInjection extensions
- [x] Sample console project
- [x] Published to NuGet
- [x] CI via GitHub Actions
- [x] Release automation on tags

---

## 🔄 Upcoming (v0.2.x)

- [X] `IRequestPreProcessor<TRequest>` and `IRequestPostProcessor<TRequest, TResponse>` support
- [X] Timeout and CircuitBreaker behaviors
- [ ] Source generator for handler resolution
- [ ] Optional custom validation abstraction (to replace FluentValidation)
- [ ] Metrics integration (OpenTelemetry or App Insights)
- [ ] Add correlation ID behavior
- [ ] Improve performance of pipeline executor
- [ ] Benchmarks comparing to MediatR
- [ ] GitHub Release notes generator

---

## 🧪 Dev & Testing Tools

- [x] GitHub Actions CI for build/test
- [ ] Coverage reports with Coverlet + ReportGenerator
- [ ] Manual DI registration sample (no MS.Extensions)
- [ ] Optional minimal web API sample

---

## 📦 Ecosystem and Distribution

- [x] Publish stable NuGet packages
- [x] Sample app in `/samples`
- [ ] CLI tool to scaffold handlers, validators, etc
- [ ] Optional Blazor/Mobile/MAUI integration examples
- [ ] Add NuGet badges, license, and PR templates

---

## 🤝 Community and Collaboration

- [x] CONTRIBUTING.md
- [x] Issue and PR templates
- [x] GitHub Discussions enabled
- [ ] Label roadmap issues with `roadmap`
- [ ] Add GitHub sponsorship and pinned roadmap issue (future)
