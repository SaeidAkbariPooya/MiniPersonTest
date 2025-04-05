using MediatR;
using MiniPerson.Application.DTO;

namespace MiniPerson.Application.Features.LeaveTypes.Requests.Commands
{
    public class CreatePersonCommand : IRequest<long>
    {
        public AddPersonDto PersonDto { get; set; }
    }
}
