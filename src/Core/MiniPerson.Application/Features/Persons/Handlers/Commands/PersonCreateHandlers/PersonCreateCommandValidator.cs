using FluentValidation;
using MiniPerson.Application.Common.Validation;
using MiniPerson.Application.Features.LeaveTypes.Requests.Commands;

namespace MiniPerson.Application.Features.Persons.Handlers.Commands.PersonCreateHandlers
{
    public class PersonCreateCommandValidator : CustomValidator<CreatePersonCommand>
    {
        public PersonCreateCommandValidator()
        {
            RuleFor(p => p.PersonDto.FirstName).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50");

            RuleFor(p => p.PersonDto.LastName).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50");

            RuleFor(p => p.PersonDto.NationCode).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10");
                
            RuleFor(p => p.PersonDto.BirthDate).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10");
        }
    }
}