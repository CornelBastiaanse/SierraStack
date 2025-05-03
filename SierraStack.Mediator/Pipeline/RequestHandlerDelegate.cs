namespace SierraStack.Mediator.Pipeline;

/// <summary>
/// Delegate representing the final request handler in the pipeline.
/// </summary>
/// <typeparam name="TResponse">The response type.</typeparam>
public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();