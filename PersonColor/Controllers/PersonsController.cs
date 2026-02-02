using Microsoft.AspNetCore.Mvc;
using PersonColor.Api.Models;
using PersonColor.Api.Repositories;

namespace PersonColor.Api.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _repository;

        public PersonsController(IPersonRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetById(int id)
        {
            var person = _repository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet("color/{color}")]
        public ActionResult<IEnumerable<Person>> GetByColor(string color)
        {
            var persons = _repository.GetByColor(color);
            return Ok(persons);
        }

        [HttpPost]
        public ActionResult<Person> Add([FromBody] Person person)
        {
            _repository.Add(person);
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }
    }
}
