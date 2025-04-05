using MediatR;

namespace MiniPerson.Application.Features.Persons.Requests.Commands
{
    public class DeletePersonCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}
