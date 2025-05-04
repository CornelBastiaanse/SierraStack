using Microsoft.Extensions.Logging;
using Moq;
using SierraStack.Mediator.Behaviors.Logging;
using SierraStack.Mediator.Tests.Extensions;
using SierraStack.Mediator.Tests.Requests;

namespace SierraStack.Mediator.Tests.Behaviors;

public class LoggingBehaviorTests
{
    [Fact]
    public async Task Should_Log_Before_And_After_Request()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<LoggingBehavior<TestRequest, string>>>();

        var behavior = new LoggingBehavior<TestRequest, string>(loggerMock.Object);

        var request = new TestRequest();

        // Act
        var response = await behavior.HandleAsync(request, CancellationToken.None, () => Task.FromResult("success"));

        // Assert
        Assert.Equal("success", response);

        loggerMock.VerifyLog(LogLevel.Information, "Handling request: TestRequest", Times.Once());
        loggerMock.VerifyLog(LogLevel.Information, "Handled request: TestRequest", Times.Once());
    }
}