using FluentValidation;
using MediatR;
using MiniPerson.Application.Features.Persons.Requests.Commands;
using MiniPerson.Contract.Repositories;
using MiniPerson.Infrastructure.Patterns;

namespace MiniPerson.Application.Features.Persons.Handlers.Commands.PersonDeleteHandlers
{
    public class PersonDeleteCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private IValidator<DeletePersonCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public PersonDeleteCommandHandler(IUnitOfWork unitOfWork,
                                          IPersonRepository leaveTypeRepository,
                                          IValidator<DeletePersonCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _personRepository = leaveTypeRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new Exception(String.Join(",", validationResult.Errors.Select(q => q.ErrorMessage).ToArray()));
            }

            var result = await _personRepository.DeleteAsync(request.Id);
            if (result != true)
                return false;
            await _unitOfWork.SaveChanges();
            return true;
        }
    }
}
