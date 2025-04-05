
using MiniPerson.Infrastructure.DataBase.Context;

namespace MiniPerson.Infrastructure.Patterns
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonDbContext context;

        public UnitOfWork(PersonDbContext context)
        {
            this.context = context;
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
