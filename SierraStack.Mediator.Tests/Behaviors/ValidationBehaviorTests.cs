using FluentValidation;
using Microsoft.Extensions.Options;
using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Behaviors.Validation;
using SierraStack.Mediator.Behaviors.Validation.Fluent;

namespace SierraStack.Mediator.Tests.Behaviors;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task Should_Allow_Request_When_No_Validators()
    {
        var options = Options.Create(new ValidationBehaviorOptions());
        var behavior = new ValidationBehavior<TestRequest, string>([], options);

        var response = await behavior.HandleAsync(new TestRequest(), CancellationToken.None, () => Task.FromResult("OK"));

        Assert.Equal("OK", response);
    }

    [Fact]
    public async Task Should_Throw_When_Validation_Fails()
    {
        var fluentValidator = new FailingValidator();
        var adapter = new FluentValidatorAdapter<TestRequest>(fluentValidator);
        var options = Options.Create(new ValidationBehaviorOptions());
        var behavior = new ValidationBehavior<TestRequest, string>([adapter], options);

        var ex = await Assert.ThrowsAsync<ValidationException>(() =>
            behavior.HandleAsync(new TestRequest(), CancellationToken.None, () => Task.FromResult("OK")));

        Assert.Single(ex.Errors);
        Assert.Equal("Name is required", ex.Errors.First().ErrorMessage);
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