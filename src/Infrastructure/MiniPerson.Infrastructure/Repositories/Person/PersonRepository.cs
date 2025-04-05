using Microsoft.EntityFrameworkCore;
using MiniPerson.Contract.Repositories;
using MiniPerson.Infrastructure.Base;
using MiniPerson.Infrastructure.DataBase.Context;

namespace MiniPerson.Infrastructure.Repositories.Person
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _context;

        public PersonRepository(PersonDbContext context)
        {
            _context = context;
        }
        public async Task<List<Domain.Entities.Person>> GetAllAsync()
        {
            var result =  _context.Persons.AsQueryable().AsNoTracking().ToList();
            return result;
        }

        public async Task<Domain.Entities.Person> GetByIdAsync(long id)
        {
            var result = await _context.Persons.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }

        public async Task<long> InsertAsync(Domain.Entities.Person person)
        {
            await _context.AddAsync(person);
            return person.Id;
        }
        public async Task<bool> DeleteAsync(long Id)
        {
            var personId = await _context.Persons.SingleOrDefaultAsync(s => s.Id == Id);
            if (personId == null)
                return false;

            _context.Remove(personId);
            return true;
        }

        public async Task UpdateAsync(Domain.Entities.Person person)
        {
            var current = await _context.Persons.FindAsync(person.Id);

            if (current == null) throw new DatabaseException("feature Id in not valid");
            current.Update(person);
        }
    }
}
