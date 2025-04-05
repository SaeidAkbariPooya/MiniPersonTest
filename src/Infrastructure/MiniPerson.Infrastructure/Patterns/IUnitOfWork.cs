

namespace MiniPerson.Infrastructure.Patterns
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChanges();
    }
}
