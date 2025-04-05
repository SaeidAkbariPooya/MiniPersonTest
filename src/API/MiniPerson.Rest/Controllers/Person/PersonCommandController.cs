using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniPerson.Application.Features.LeaveTypes.Requests.Commands;
using MiniPerson.Application.Features.Persons.Requests.Commands;

namespace MiniPerson.Rest.Controllers.Person
{
    public class PersonCommandController : BaseController
    {
        public PersonCommandController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdatePersonCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeletePersonCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
