using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Behaviors.Logging;
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

    public static IServiceCollection AddSierraStackBehaviors(this IServiceCollection services)
    {
        return services
            .AddLoggingBehavior()
            .AddValidationBehavior();
    }
}