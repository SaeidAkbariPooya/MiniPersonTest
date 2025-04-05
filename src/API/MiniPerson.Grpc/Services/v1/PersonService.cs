using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using MiniPerson.Application.DTO;
using MiniPerson.Application.Features.LeaveTypes.Requests.Commands;
using MiniPerson.Application.Features.Person.Requests.Queries;
using MiniPerson.Application.Features.Persons.Requests.Commands;
using MiniPerson.Grpc.Protos.v1;

namespace MiniPerson.Grpc.Services.v1;

public class PersonService : Protos.v1.PersonService.PersonServiceBase
{
    private readonly IMediator _mediator;

    public PersonService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task CreatePerson(IAsyncStreamReader<PersonCreateRequest> requestStream, IServerStreamWriter<PersonCreateReply> responseStream, ServerCallContext context)
    {
        await foreach (var item in requestStream.ReadAllAsync())
        {
            var command = new CreatePersonCommand
            {
                PersonDto = new AddPersonDto
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    NationCode = item.NationCode,
                    BirthDate = item.BirthDate,
                }
            };
            var personId = await _mediator.Send(command);
            await responseStream.WriteAsync(new PersonCreateReply { Id = Convert.ToInt32(personId) });
        }
        await Task.CompletedTask;

    }
    public override async Task<PersonDeleteReply> DeletePerson(PersonByIdRequest request, ServerCallContext context)
    {
        var command = new DeletePersonCommand
        {
            Id = request.ID
        };

        var serviceResult = await _mediator.Send(command);

        return new PersonDeleteReply
        {
            Success = serviceResult
        };
    }

    public override async Task<PersonUpdateReply> UpdatePerson(PersonUpdateRequest request, ServerCallContext context)
    {
        var command = new UpdatePersonCommand
        {
            PersonDto = new PersonDto
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationCode = request.NationCode,
                BirthDate = request.BirthDate,
            }
        };

        var resultId = await _mediator.Send(command);

        return new PersonUpdateReply
        {
            Id = resultId
        };
    }

    public override async Task<PersonReply> GetById(PersonByIdRequest request, ServerCallContext context)
    {
        var query = new GetPersonByIdRequest {  Id = request.ID };
        var person = await _mediator.Send(query);

        if (person is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Person with id {request.ID} not found"));
        }

        var reply = new PersonReply
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            NationCode = person.NationCode,
            BirthDate = person.BirthDate,
        };
        return reply;
    }

    public override async Task GetAll(Empty request, IServerStreamWriter<PersonReply> responseStream, ServerCallContext context)
    {
        var query = new GetPersonRequest();
        var persons = await _mediator.Send(query);

        foreach (var item in persons)
        {
            var reply = new PersonReply
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                NationCode = item.NationCode,
                BirthDate = item.BirthDate,
            };

            await responseStream.WriteAsync(reply);
        }
        await Task.CompletedTask;
    }
}
