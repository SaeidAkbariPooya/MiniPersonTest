using FluentValidation;
using MiniPerson.Application.Features.Persons.Requests.Commands;
using MiniPerson.Infrastructure.DataBase.Context;

namespace MiniPerson.Application.Features.Persons.Handlers.Commands.PersonUpdateHandlers
{
    public class GetPersonByIdRequestValidator : AbstractValidator<UpdatePersonCommand>
    {
        private readonly PersonDbContext _dbContext;
        public GetPersonByIdRequestValidator(PersonDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.PersonDto.Id)
                .Must(BeExist).WithMessage("The person dose not exist.");

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
        private bool BeExist(long id)
        {
            var person = _dbContext.Persons
                .FirstOrDefault(x => x.Id == id);

            return person != null;
        }
    }
}