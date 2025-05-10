using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Behaviors.Caching;
using SierraStack.Mediator.Behaviors.ExceptionHandling;
using SierraStack.Mediator.Behaviors.Logging;
using SierraStack.Mediator.Behaviors.Performance;
using SierraStack.Mediator.Behaviors.Processing;
using SierraStack.Mediator.Behaviors.Retry;
using SierraStack.Mediator.Behaviors.Timeout;
using SierraStack.Mediator.Behaviors.Validation;
using SierraStack.Mediator.Pipeline;

namespace SierraStack.Mediator.Behaviors.Extensions;

/// <summary>
/// Extension methods to register SierraStack behaviors.
/// </summary>
public static class BehaviorRegistrationExtensions
{
    /// <summary>
    /// Extension method to register the logging behavior in the SierraStack pipeline.
    /// </summary>
    public static IServiceCollection AddLoggingBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
    
    /// <summary>
    /// Extension method to register the validation behavior in the SierraStack pipeline.
    /// </summary>
    public static IServiceCollection AddValidationBehavior(this IServiceCollection services, Action<ValidationBehaviorOptions>? configure = null)
    {
        services.AddOptions<ValidationBehaviorOptions>();

        if (configure is not null)
            services.Configure(configure);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
    
    /// <summary>
    /// Extension method to register the retry behavior in the SierraStack pipeline.
    /// </summary>
    public static IServiceCollection AddRetryBehavior(this IServiceCollection services, Action<RetryBehaviorOptions>? configure = null)
    {
        services.AddOptions<RetryBehaviorOptions>();
        
        if (configure is not null)
            services.Configure(configure);
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RetryBehavior<,>));
        return services;
    }
    
    /// <summary>
    /// Extension method to register the performance behavior in the SierraStack pipeline.
    /// </summary>
    public static IServiceCollection AddPerformanceBehavior(this IServiceCollection services, Action<PerformanceBehaviorOptions>? configure = null)
    {
        services.AddOptions<PerformanceBehaviorOptions>();
        
        if (configure is not null)
            services.Configure(configure);
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        return services;
    }
    
    /// <summary>
    /// Extension method to register the exception handling behavior in the SierraStack pipeline.
    /// </summary>
    public static IServiceCollection AddExceptionHandlingBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
        return services;
    }
    
    /// <summary>
    /// Extension method to register the caching behavior in the SierraStack pipeline.
    /// </summary>
    public static IServiceCollection AddCachingBehavior(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        return services;
    }
    
    /// <summary>
    /// Extension method to register the request processing behavior in the SierraStack pipeline.
    /// </summary>
    public static IServiceCollection AddRequestProcessingBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestProcessingBehavior<,>));
        return services;
    }
    
    /// <summary>
    /// Extension method to register the timeout behavior in the SierraStack pipeline.
    /// </summary>
    public static IServiceCollection AddTimeoutBehavior(this IServiceCollection services, Action<TimeoutBehaviorOptions>? configure = null)
    {
        services.AddOptions<TimeoutBehaviorOptions>();
        
        if (configure is not null)
            services.Configure(configure);
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TimeoutBehavior<,>));
        return services;
    }
    
    /// <summary>
    /// Extension method to register all SierraStack behaviors in the pipeline.
    /// </summary>
    public static IServiceCollection AddSierraStackBehaviors(this IServiceCollection services)
    {
        return services
            .AddLoggingBehavior()
            .AddValidationBehavior()
            .AddRetryBehavior()
            .AddPerformanceBehavior()
            .AddExceptionHandlingBehavior()
            .AddCachingBehavior()
            .AddRequestProcessingBehavior()
            .AddTimeoutBehavior();
    }
}