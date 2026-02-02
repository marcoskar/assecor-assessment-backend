using PersonColor.Api.Mappings;
using PersonColor.Api.Models;

namespace PersonColor.Api.Repositories
{
    public class CsvPersonRepository : IPersonRepository
    {
        private readonly List<Person> _persons = new();
        private readonly string _csvPath;
        private int _nextId = 1;

        public CsvPersonRepository(string csvPath)
        {
            _csvPath = csvPath;
            LoadFromCsv();
        }

        private void LoadFromCsv()
        {
            if (!File.Exists(_csvPath))
            {
                return;
            }

            var lines = File.ReadAllLines(_csvPath);
            int id = 1;

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length < 4)
                {
                    continue;
                }

                var lastname = parts[0].Trim();
                var name = parts[1].Trim();
                var zipCity = parts[2].Trim();
                var colorIdStr = parts[3].Trim();
                if (!int.TryParse(colorIdStr, out int colorId))
                {
                    continue;
                }

                var color = ColorMapping.GetColor(colorId) ?? "unbekannt";
                var zipCityParts = zipCity.Split(' ', 2);
                var zipcode = zipCityParts.Length > 0 ? zipCityParts[0] : "";
                var city = zipCityParts.Length > 1 ? zipCityParts[1] : "";
                _persons.Add(new Person
                {
                    Id = id++,
                    Name = name,
                    LastName = lastname,
                    ZipCode = zipcode,
                    City = city,
                    Color = color
                });
            }
            _nextId = id;
        }

        public IEnumerable<Person> GetAll() => _persons;

        public Person? GetById(int id) => _persons.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Person> GetByColor(string color) => _persons.Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase));

        public Person Add(Person person)
        {
            person.Id = _nextId++;
            _persons.Add(person);

            return person;
        }
    }
}
