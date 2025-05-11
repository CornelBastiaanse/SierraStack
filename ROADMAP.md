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

## ✅ Current (v0.2.x)

- [x] `IRequestPreProcessor<TRequest>` and `IRequestPostProcessor<TRequest, TResponse>` support
- [x] Timeout behavior with configurable options
- [x] Custom validation abstraction (decoupled from FluentValidation)
- [x] FluentValidation adapter and integration method
- [x] ValidationBehavior with options
- [x] Source generator foundation (compile-time handler discovery)
- [x] Initial benchmarks with BenchmarkDotNet

---

## 🧭 Next (v0.3.0)

- [ ] Source generator: AddHandlersFromSource()
- [ ] Metrics integration (OpenTelemetry or App Insights)
- [ ] Add correlation ID behavior
- [ ] Improve performance of pipeline executor
- [ ] Benchmarks comparing to MediatR
- [ ] GitHub Release notes generator
- [ ] Behavior lifetime customization
- [ ] Add IRequestPre/PostProcessor unit test templates

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
