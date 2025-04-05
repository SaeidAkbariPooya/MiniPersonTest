using Microsoft.Extensions.DependencyInjection;
using MiniPerson.Contract.Repositories;
using MiniPerson.Infrastructure.Patterns;
using MiniPerson.Infrastructure.Repositories.Person;

namespace MiniPerson.Infrastructure
{
    public static class InfrastructureSetup
    {
        public static void AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
