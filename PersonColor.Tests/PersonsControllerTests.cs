using Microsoft.AspNetCore.Mvc;
using PersonColor.Api.Controllers;
using PersonColor.Api.Models;
using PersonColor.Api.Repositories;

namespace PersonColor.Tests
{
    public class PersonsControllerTests
    {
        private PersonsController GetControllerWithSampleData()
        {
            var csvPath = Path.Combine(AppContext.BaseDirectory, "sample-input.csv");
            var repo = new CsvPersonRepository(csvPath);
            return new PersonsController(repo);
        }

        [Fact]
        public void GetAll_ReturnsAllPersons()
        {
            var controller = GetControllerWithSampleData();
            var result = controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var persons = Assert.IsAssignableFrom<IEnumerable<Person>>(okResult.Value);
            Assert.True(persons.Any());
        }

        [Fact]
        public void GetById_ReturnsCorrectPerson()
        {
            var controller = GetControllerWithSampleData();
            var result = controller.GetById(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var person = Assert.IsType<Person>(okResult.Value);
            Assert.Equal(1, person.Id);
        }

        [Fact]
        public void GetByColor_ReturnsPersonsWithColor()
        {
            var controller = GetControllerWithSampleData();
            var result = controller.GetByColor("blau");
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var persons = Assert.IsAssignableFrom<IEnumerable<Person>>(okResult.Value);
            Assert.All(persons, p => Assert.Equal("blau", p.Color));
        }

        [Fact]
        public void Add_AddsNewPerson()
        {
            var controller = GetControllerWithSampleData();
            var newPerson = new Person { Name = "Test", LastName = "User", ZipCode = "12345", City = "Teststadt", Color = "rot" };
            var result = controller.Add(newPerson);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var person = Assert.IsType<Person>(createdResult.Value);
            Assert.Equal("Test", person.Name);
            Assert.Equal("rot", person.Color);
        }
    }
}
