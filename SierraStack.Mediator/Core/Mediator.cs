using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Core;

/// <summary>
/// Default implementation of <see cref="IMediator"/> that uses dependency injection
/// to resolve and invoke request and notification handlers.
/// </summary>
public class Mediator : IMediator
{
    /// <summary>
    /// The service provider used to resolve handlers.
    /// </summary>
    private readonly IServiceProvider _provider;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Mediator"/> class.
    /// </summary>
    /// <param name="provider">The service provider used to resolve handlers.</param>
    public Mediator(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = _provider.GetRequiredService(handlerType);
        return await handler.HandleAsync((dynamic)request, cancellationToken);
    }
    
    /// <inheritdoc/>
    public async Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) 
        where TNotification : INotification
    {
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
        var handlers = _provider.GetServices(handlerType);

        foreach (dynamic? handler in handlers)
        {
            if (handler is null)
                continue;
            
            await handler.HandleAsync(notification, cancellationToken);
        }
    }
}