using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniPerson.Application.Features.Person.Requests.Queries;

namespace MiniPerson.Rest.Controllers.Person
{
    public class PersonQueriesController : BaseController
    {
        public PersonQueriesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetPersonRequest { };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] GetPersonByIdRequest query)
        {
            return Ok(await Mediator.Send(query));
        }

    }
}
