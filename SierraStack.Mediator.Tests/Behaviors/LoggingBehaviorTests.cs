using FluentValidation;
using SierraStack.Mediator.Behaviors.Validation;
using SierraStack.Mediator.Tests.Requests;

namespace SierraStack.Mediator.Tests.Behaviors;

public class LoggingBehaviorTests
{
    [Fact]
    public async Task Should_Allow_Request_When_No_Validators()
    {
        var behavior = new ValidationBehavior<TestRequest, string>([]);
        
        var response = await behavior.HandleAsync(
            new TestRequest(), 
            CancellationToken.None, 
            () => Task.FromResult("OK"));
        
        Assert.Equal("OK", response);
    }
    
    [Fact]
    public async Task Should_Throw_When_Validation_Fails()
    {
        var validator = new FailingValidator();
        var behavior = new ValidationBehavior<TestRequest, string>([validator]);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => behavior.HandleAsync(new TestRequest(), CancellationToken.None, () => Task.FromResult("OK")));

        Assert.Contains("Name is required", exception.Message);
    }

    private class FailingValidator : AbstractValidator<TestRequest>
    {
        public FailingValidator()
        {
            RuleFor(_ => "")
                .Must(_ => false)
                .WithMessage("Name is required");
        }
    }
}