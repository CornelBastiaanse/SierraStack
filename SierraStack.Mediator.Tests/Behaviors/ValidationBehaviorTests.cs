using FluentValidation;
using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Behaviors.Validation;

namespace SierraStack.Mediator.Tests.Behaviors;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task Should_Allow_Request_When_No_Validators()
    {
        var behavior = new ValidationBehavior<TestRequest, string>([]);

        var response = await behavior.HandleAsync(new TestRequest(), CancellationToken.None, () => Task.FromResult("OK"));

        Assert.Equal("OK", response);
    }

    [Fact]
    public async Task Should_Throw_When_Validation_Fails()
    {
        var validator = new FailingValidator();
        var behavior = new ValidationBehavior<TestRequest, string>([validator]);

        var ex = await Assert.ThrowsAsync<ValidationException>(() =>
            behavior.HandleAsync(new TestRequest(), CancellationToken.None, () => Task.FromResult("OK")));

        Assert.Contains("Name is required", ex.Message);
    }

    private class TestRequest : IRequest<string> { }

    private class FailingValidator : AbstractValidator<TestRequest>
    {
        public FailingValidator()
        {
            RuleFor(_ => "").Must(_ => false).WithMessage("Name is required");
        }
    }
}