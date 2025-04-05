using FluentValidation;
using MiniPerson.Application.Features.Persons.Requests.Commands;
using MiniPerson.Infrastructure.DataBase.Context;

namespace MiniPerson.Application.Features.Persons.Handlers.Commands.PersonDeleteHandlers
{
    public class PersonUpdateCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        private readonly PersonDbContext _dbContext;
        public PersonUpdateCommandValidator(PersonDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id)
                .Must(BeExist).WithMessage("The person dose not exist.");
        }
        private bool BeExist(long id)
        {
            var person = _dbContext.Persons
                .FirstOrDefault(x => x.Id == id);

            return person != null;
        }
    }
}