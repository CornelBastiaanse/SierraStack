using SierraStack.Mediator.Pipeline;
using SierraStack.Mediator.Processing;

namespace SierraStack.Mediator.Behaviors.Processing;

/// <summary>
/// Executes registered pre-processors before the handler and post-processors after.
/// </summary>
public class RequestProcessingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// All registered pre-processors.
    /// </summary>
    private readonly IEnumerable<IRequestPreProcessor<TRequest>> _preProcessors;
    
    /// <summary>
    /// All registered post-processors.
    /// </summary>
    private readonly IEnumerable<IRequestPostProcessor<TRequest, TResponse>> _postProcessors;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RequestProcessingBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="preProcessors">All registered pre-processors.</param>
    /// <param name="postProcessors">All registered post-processors.</param>
    public RequestProcessingBehavior(
        IEnumerable<IRequestPreProcessor<TRequest>> preProcessors,
        IEnumerable<IRequestPostProcessor<TRequest, TResponse>> postProcessors)
    {
        _preProcessors = preProcessors;
        _postProcessors = postProcessors;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        foreach (var processor in _preProcessors)
            await processor.ProcessAsync(request, cancellationToken);
        
        var response = await next();
        
        foreach (var processor in _postProcessors)
            await processor.ProcessAsync(request, response, cancellationToken);
        
        return response;
    }
}