using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;

namespace PawfectMatch.Tests
{
    public class PersonasServiceTests
    {
        private readonly PersonasService _service;
        private readonly ApplicationDbContext _context;

        public PersonasServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PersonasServiceTestDb")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new PersonasService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddPersona()
        {
            var persona = new PawfectMatch.Models.Personas
            {
                Nombre = "Test Persona",
                Telefono = "123456789",
                Sexo = 'm',
                Email = "test@correo.com"
            };
            var result = await _service.InsertAsync(persona);
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnPersonas()
        {
            var personas = await _service.ListAsync(p => !p.IsDeleted);
            Assert.NotNull(personas);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePersona()
        {
            var persona = new PawfectMatch.Models.Personas
            {
                Nombre = "Update Persona",
                Telefono = "123456789",
                Sexo = 'm',
                Email = "update@correo.com"
            };
            await _service.InsertAsync(persona);
            persona.Nombre = "Updated";
            var result = await _service.UpdateAsync(persona);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeletePersona()
        {
            var persona = new PawfectMatch.Models.Personas
            {
                Nombre = "Delete Persona",
                Telefono = "123456789",
                Sexo = 'm',
                Email = "delete@correo.com"
            };
            await _service.InsertAsync(persona);
            var result = await _service.DeleteAsync(persona.PersonasID);
            Assert.True(result);
        }
    }
}
