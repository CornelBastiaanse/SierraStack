using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Core;

namespace SierraStack.Mediator.Extensions.Microsoft.DependencyInjection;

/// <summary>
/// Provides extension methods for integrating SierraStack.Mediator with Microsoft.Extensions.DependencyInjection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the mediator, request handlers, and notification handlers in the specified assemblies.
    /// </summary>
    /// <param name="services">The service collection to add to.</param>
    /// <param name="assemblies">Assemblies to scan for handlers.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddSierraStackMediator(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddSingleton<IMediator, Core.Mediator>();
        
        foreach (var assembly in assemblies)
            RegisterHandlers(services, assembly);
        
        return services;
    }
    
    /// <summary>
    /// Registers the request and notification handlers in the specified assembly.
    /// </summary>
    /// <param name="services">The service collection to add to.</param>
    /// <param name="assembly">The assembly to scan for handlers.</param>
    private static void RegisterHandlers(IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            var interfaces = type.GetInterfaces();
            
            if (type.IsAbstract || type.IsInterface) 
                continue;

            foreach (var @interface in interfaces)
            {
                if(IsRequestHandler(@interface))
                    services.AddTransient(@interface, type);
                    
                if (IsNotificationHandler(@interface))
                    services.AddTransient(@interface, type);
            }
        }
    }
    
    /// <summary>
    /// Checks if the given type is a request handler.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>Whether the type is a request handler.</returns>
    private static bool IsRequestHandler(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IRequestHandler<,>);
    }
    
    /// <summary>
    /// Checks if the given type is a notification handler.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>Whether the type is a notification handler</returns>
    private static bool IsNotificationHandler(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(INotificationHandler<>);
    }
}