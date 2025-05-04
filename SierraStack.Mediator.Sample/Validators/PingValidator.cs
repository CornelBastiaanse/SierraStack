using FluentValidation;
using SierraStack.Mediator.Sample.Requests;

namespace SierraStack.Mediator.Sample.Validators;

public class PingValidator : AbstractValidator<Ping>
{
    public PingValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("Message cannot be empty.");
    }
}