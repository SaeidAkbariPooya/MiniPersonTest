using MediatR;
using MiniPerson.Application.DTO;

namespace MiniPerson.Application.Features.Person.Requests.Queries
{
    public class GetPersonRequest : IRequest<List<PersonDto>>
    {

    }
}
