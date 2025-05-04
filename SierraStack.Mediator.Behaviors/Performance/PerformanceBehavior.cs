using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SierraStack.Mediator.Pipeline;

namespace SierraStack.Mediator.Behaviors.Performance;

/// <summary>
/// Measures and logs the time it takes to handle a request.
/// </summary>
/// <typeparam name="TRequest">The type of the request that is being handled.</typeparam>
/// <typeparam name="TResponse">The type of the response that is expected.</typeparam>
public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// Logger for logging performance metrics.
    /// </summary>
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
    
    /// <summary>
    /// Options for configuring the behavior.
    /// </summary>
    private readonly PerformanceBehaviorOptions _options;
    
    /// <summary>
    /// Parameterized constructor for PerformanceBehavior.
    /// </summary>
    /// <param name="logger">Logger for logging performance metrics.</param>
    /// <param name="options">Options for configuring the behavior.</param>
    public PerformanceBehavior(
        ILogger<PerformanceBehavior<TRequest, TResponse>> logger,
        IOptions<PerformanceBehaviorOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }
    
    /// <inheritdoc/>
    public  async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var sw = Stopwatch.StartNew();
        
        var response = await next();
        
        sw.Stop();
        
        if(_options.LogEveryRequest)
            _logger.LogInformation(
                "Handled {RequestName} in {ElapsedMilliseconds}ms",
                typeof(TRequest).Name,
                sw.ElapsedMilliseconds);
        
        if (sw.ElapsedMilliseconds > _options.WarningThresholdMilliseconds)
            _logger.LogWarning(
                "⚠️ {RequestName} took too long: {ElapsedMilliseconds}ms (threshold: {Threshold}ms)",
                typeof(TRequest).Name,
                sw.ElapsedMilliseconds,
                _options.WarningThresholdMilliseconds);
        
        return response;
    }
}