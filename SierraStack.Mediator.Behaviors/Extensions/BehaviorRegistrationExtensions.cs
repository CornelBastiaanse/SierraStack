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

public static class BehaviorRegistrationExtensions
{
    public static IServiceCollection AddLoggingBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }

    public static IServiceCollection AddValidationBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }

    public static IServiceCollection AddRetryBehavior(this IServiceCollection services, Action<RetryBehaviorOptions>? configure = null)
    {
        services.AddOptions<RetryBehaviorOptions>();
        
        if (configure is not null)
            services.Configure(configure);
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RetryBehavior<,>));
        return services;
    }

    public static IServiceCollection AddPerformanceBehavior(this IServiceCollection services, Action<PerformanceBehaviorOptions>? configure = null)
    {
        services.AddOptions<PerformanceBehaviorOptions>();
        
        if (configure is not null)
            services.Configure(configure);
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        return services;
    }

    public static IServiceCollection AddExceptionHandlingBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
        return services;
    }
    
    public static IServiceCollection AddCachingBehavior(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        return services;
    }

    public static IServiceCollection AddRequestProcessingBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestProcessingBehavior<,>));
        return services;
    }

    public static IServiceCollection AddTimeoutBehavior(this IServiceCollection services, Action<TimeoutBehaviorOptions>? configure = null)
    {
        services.AddOptions<TimeoutBehaviorOptions>();
        
        if (configure is not null)
            services.Configure(configure);
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TimeoutBehavior<,>));
        return services;
    }

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