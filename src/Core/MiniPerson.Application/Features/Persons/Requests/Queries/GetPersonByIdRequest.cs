using MediatR;
using MiniPerson.Application.DTO;

namespace MiniPerson.Application.Features.Person.Requests.Queries
{
    public class GetPersonByIdRequest : IRequest<PersonDto>
    {
        public long Id { get; set; }
    }
}
