using Microsoft.Extensions.Logging;
using Moq;

namespace SierraStack.Mediator.Tests.Extensions;

public static class LoggerMockExtensions
{
    public static void VerifyLog<T>(
        this Mock<ILogger<T>> logger,
        LogLevel level,
        string message,
        Times times)
    {
        logger.Verify(l =>
                l.Log(
                    level,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains(message) == true),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }
}