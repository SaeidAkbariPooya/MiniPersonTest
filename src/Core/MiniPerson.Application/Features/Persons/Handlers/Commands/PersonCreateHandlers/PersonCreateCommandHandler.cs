using FluentValidation;
using MediatR;
using MiniPerson.Application.Features.LeaveTypes.Requests.Commands;
using MiniPerson.Contract.Repositories;
using MiniPerson.Infrastructure.Patterns;

namespace MiniPerson.Application.Features.Persons.Handlers.Commands.PersonCreateHandlers
{
    public class PersonCreateCommandHandler
        : IRequestHandler<CreatePersonCommand, long>
    {
        private readonly IPersonRepository _personRepository;
        private IValidator<CreatePersonCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public PersonCreateCommandHandler(IUnitOfWork unitOfWork,
                                          IPersonRepository personRepository,
                                          IValidator<CreatePersonCommand> validator
                                          )
        {
            _personRepository = personRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new Exception(String.Join(",", validationResult.Errors.Select(q => q.ErrorMessage).ToArray()));
            }

            var person = Domain.Entities.Person.Create(
                           request.PersonDto.FirstName,
                           request.PersonDto.LastName,
                           request.PersonDto.NationCode,
                           request.PersonDto.BirthDate);

            await _personRepository.InsertAsync(person);
            await _unitOfWork.SaveChanges();

            return person.Id;
        }
    }
}
