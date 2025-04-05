using Microsoft.EntityFrameworkCore;
using MiniPerson.Contract.Repositories;
using MiniPerson.Grpc.Infrastructures;
using MiniPerson.Grpc.Interceptors;
using MiniPerson.Infrastructure.DataBase.Context;
using MiniPerson.Infrastructure.Patterns;
using MiniPerson.Infrastructure.Repositories.Person;
using MiniPerson.Application;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    WebRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    Args = args
});

builder.Services.ConfigureApplicationServices();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddDbContext<PersonDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PersonConnection"));
});

builder.Services.AddSingleton<ProtoFileProvider>();

builder.Services.AddGrpc(c =>
{
    c.EnableDetailedErrors = true;
    c.Interceptors.Add<ExceptionInterceptor>();
});

builder.Services.AddGrpcReflection();
builder.Services.AddGrpc();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcReflectionService();
app.MapGrpcService<MiniPerson.Grpc.Services.v1.PersonService>();


app.MapGet("/protos", (ProtoFileProvider protoFileProvider) =>
{
    return Results.Ok(protoFileProvider.GetAll());
});
app.MapGet("/protos/v{version:int}/{protoName}", (ProtoFileProvider protoFileProvider, int version, string protoName) =>
{
    string filePath = protoFileProvider.GetPath(version, protoName);
    if (string.IsNullOrEmpty(filePath))
        return Results.NotFound();
    return Results.File(filePath);
});

app.MapGet("/protos/v{version:int}/{protoName}/view", async (ProtoFileProvider protoFileProvider, int version, string protoName) =>
{
    string fileContent = await protoFileProvider.GetContent(version, protoName);
    if (string.IsNullOrEmpty(fileContent))
        return Results.NotFound();
    return Results.Text(fileContent);
});

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
