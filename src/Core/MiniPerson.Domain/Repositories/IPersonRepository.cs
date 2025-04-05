using MiniPerson.Domain.Entities;

namespace MiniPerson.Contract.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(long Id);
        Task<List<Person>> GetAllAsync();
        Task<long> InsertAsync(Person person);
        Task UpdateAsync(Person person);
        Task<bool> DeleteAsync(long Id);
    }
}