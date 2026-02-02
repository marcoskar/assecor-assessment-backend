using PersonColor.Api.Models;

namespace PersonColor.Api.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
        Person? GetById(int id);
        IEnumerable<Person> GetByColor(string color);
        Person Add(Person person);
    }
}
