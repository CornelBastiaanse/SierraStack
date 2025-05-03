using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Core;
using SierraStack.Mediator.Extensions.Microsoft.DependencyInjection;

namespace SierraStack.Mediator.Tests;

public class MediatorTests
{
    private readonly IMediator _mediator;

    public MediatorTests()
    {
        var services = new ServiceCollection();
        
        // Register mediator and handlers from this test assembly
        services.AddSierraStackMediator(typeof(PingHandler).Assembly);
        
        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        
        // Get the mediator instance
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }
    
    [Fact]
    public async Task SendAsync_ShouldReturnExpectedResponse()
    {
        // Arrange
        var request = new Ping { Message = "Test" };
        
        // Act
        var response = await _mediator.SendAsync(request);
        
        // Assert
        Assert.Equal("Pong: Test", response);
    }
    
    [Fact]
    public async Task PublishAsync_ShouldCallAllNotificationHandlers()
    {
        // Arrange
        HelloNotificationHandler.Reset();
        var notification = new HelloNotification { Name = "XUnit" };
        
        // Act
        await _mediator.PublishAsync(notification);
        
        // Assert
        Assert.True(HelloNotificationHandler.WasCalled);
    }
}