using MediatR;
using MiniPerson.Application.DTO;

namespace MiniPerson.Application.Features.Persons.Requests.Commands
{
    public class UpdatePersonCommand : IRequest<long>
    {
        public PersonDto PersonDto { get; set; }
    }
}
