using AutoMapper;
using FluentValidation;
using MediatR;
using MiniPerson.Application.Features.Persons.Requests.Commands;
using MiniPerson.Contract.Repositories;
using MiniPerson.Infrastructure.Patterns;

namespace MiniPerson.Application.Features.Persons.Handlers.Commands.PersonUpdateHandlers
{
    public class PersonUpdateCommandHandler : IRequestHandler<UpdatePersonCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IValidator<UpdatePersonCommand> _validator;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonUpdateCommandHandler(IUnitOfWork unitOfWork, 
                                          IPersonRepository personRepository,
                                          IValidator<UpdatePersonCommand> validator,
                                          IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _personRepository = personRepository;
            _mapper = mapper;
        }
        public async Task<long> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new Exception(String.Join(",", validationResult.Errors.Select(q => q.ErrorMessage).ToArray()));
            }
            var person = Domain.Entities.Person.UpdateNew(request.PersonDto.Id, request.PersonDto.FirstName, request.PersonDto.LastName, request.PersonDto.NationCode,request.PersonDto.BirthDate);

            await _personRepository.UpdateAsync(person);
            await _unitOfWork.SaveChanges();

            return person.Id;
        }
    }
}
