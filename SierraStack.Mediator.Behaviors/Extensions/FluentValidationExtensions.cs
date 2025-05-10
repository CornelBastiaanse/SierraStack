using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Behaviors.Validation.Fluent;
using FluentValidation;

namespace SierraStack.Mediator.Behaviors.Extensions;

/// <summary>
/// Extension methods to register FluentValidation adapters for SierraStack validation.
/// </summary>
public static class FluentValidationExtensions
{
    /// <summary>
    /// Registers FluentValidation validators as adapters for SierraStack's <see cref="IValidator{T}"/>.
    /// </summary>
    public static IServiceCollection AddFluentValidationAdapters(this IServiceCollection services, Assembly assembly)
    {
        // Register all FluentValidation validators
        services.AddValidatorsFromAssembly(assembly);

        // Register adapters for each validator as SierraStack IValidator<T>
        foreach (var type in assembly.GetTypes())
        {
            var fluentInterface = type
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));

            if (fluentInterface == null)
                continue;

            var validatedType = fluentInterface.GenericTypeArguments[0];
            var adapterType = typeof(FluentValidatorAdapter<>).MakeGenericType(validatedType);

            services.AddTransient(typeof(SierraStack.Validation.IValidator<>).MakeGenericType(validatedType), adapterType);
        }

        return services;
    }
}