using Microsoft.EntityFrameworkCore;
using PersonColor.Api.Models;

namespace PersonColor.Api.Repositories
{
    public class EfPersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _context;

        public EfPersonRepository(PersonDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetAll() => _context.Persons.AsNoTracking().ToList();

        public Person? GetById(int id) => _context.Persons.AsNoTracking().FirstOrDefault(p => p.Id == id);

        public IEnumerable<Person> GetByColor(string color)
            => _context.Persons.AsNoTracking().Where(p => p.Color.ToLower() == color.ToLower()).ToList();

        public Person Add(Person person)
        {
            var result = _context.Persons.Add(person);
            _context.SaveChanges();

            return result.Entity;
        }
    }
}
