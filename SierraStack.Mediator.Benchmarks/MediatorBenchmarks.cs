using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Core;
using SierraStack.Mediator.Extensions.Microsoft.DependencyInjection;

namespace SierraStack.Mediator.Benchmarks;

[MemoryDiagnoser]
public class MediatorBenchmarks
{
    private IMediator _mediator;

    [GlobalSetup]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddSierraStackMediator(typeof(PingHandler).Assembly);
        _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
    }
    
    [Benchmark(Baseline = true)]
    public async Task<string> Send_SimpleRequest()
    {
        return await _mediator.SendAsync(new Ping());
    }
}