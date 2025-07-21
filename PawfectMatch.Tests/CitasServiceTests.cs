using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using PawfectMatch.Models.Servicios;

namespace PawfectMatch.Tests
{
    public class CitasServiceTests
    {
        private readonly CitasService _service;
        private readonly ApplicationDbContext _context;

        public CitasServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"CitasServiceTestDb_{Guid.NewGuid()}")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new CitasService(factory);

            // Get context from factory
            _context = factory.CreateDbContext();
        }

        [Fact]
        public async Task InsertAsync_ShouldAddCita()
        {
            // Arrange
            var cita = new Citas
            {
                PersonasID = 1,
                MascotasPersonasID = 1,
                EstadosCitasID = 1,
                Fecha = DateTime.Now,
                Hora = TimeSpan.FromHours(10),
                Motivo = "Test Cita",
                IsDeleted = false
            };

            // Act
            var result = await _service.InsertAsync(cita);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnCitas()
        {
            // Arrange
            // Create persona
            var persona = new Personas
            {
                Nombre = "Test Persona",
                Telefono = "123456789",
                Sexo = 'M',
                Email = "test@example.com",
                IsDeleted = false
            };
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            // Create mascota
            var mascota = new MascotasPersonas
            {
                Nombre = "Test Mascota",
                PersonasID = persona.PersonasID,
                RazasID = 1,
                Sexo = 'M',
                IsDeleted = false
            };
            _context.MascotasPersonas.Add(mascota);
            await _context.SaveChangesAsync();

            // Create estado cita
            var estadoCita = new EstadosCitas
            {
                Nombre = "Pendiente",
                IsDeleted = false
            };
            _context.EstadosCitas.Add(estadoCita);
            await _context.SaveChangesAsync();

            var cita = new Citas
            {
                PersonasID = persona.PersonasID,
                MascotasPersonasID = mascota.MascotasPersonasID,
                EstadosCitasID = estadoCita.EstadosCitasID,
                Fecha = DateTime.Now,
                Hora = TimeSpan.FromHours(10),
                Motivo = "Test Cita List",
                IsDeleted = false
            };

            await _service.InsertAsync(cita);

            // Act
            var citas = await _service.ListAsync(c => !c.IsDeleted);

            // Assert
            Assert.NotNull(citas);
            Assert.True(citas.Count > 0);
            Assert.Contains(citas, c => c.Motivo == "Test Cita List");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCita()
        {
            // Arrange
            var cita = new Citas
            {
                PersonasID = 1,
                MascotasPersonasID = 1,
                EstadosCitasID = 1,
                Fecha = DateTime.Now,
                Hora = TimeSpan.FromHours(10),
                Motivo = "Update Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(cita);
            cita.Motivo = "Updated Cita";

            // Act
            var result = await _service.UpdateAsync(cita);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteCita()
        {
            // Arrange
            var cita = new Citas
            {
                PersonasID = 1,
                MascotasPersonasID = 1,
                EstadosCitasID = 1,
                Fecha = DateTime.Now,
                Hora = TimeSpan.FromHours(10),
                Motivo = "Delete Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(cita);

            // Act
            var result = await _service.DeleteAsync(cita.CitasID);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnCita()
        {
            // Arrange
            // Create persona
            var persona = new Personas
            {
                Nombre = "Test Persona",
                Telefono = "123456789",
                Sexo = 'M',
                Email = "test@example.com",
                IsDeleted = false
            };
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            // Create mascota
            var mascota = new MascotasPersonas
            {
                Nombre = "Test Mascota",
                PersonasID = persona.PersonasID,
                RazasID = 1,
                Sexo = 'M',
                IsDeleted = false
            };
            _context.MascotasPersonas.Add(mascota);
            await _context.SaveChangesAsync();

            // Create estado cita
            var estadoCita = new EstadosCitas
            {
                Nombre = "Pendiente",
                IsDeleted = false
            };
            _context.EstadosCitas.Add(estadoCita);
            await _context.SaveChangesAsync();

            // Arrange
            var cita = new Citas
            {
                CitasID = 1,
                PersonasID = 1,
                MascotasPersonasID = 1,
                EstadosCitasID = 1,
                Fecha = DateTime.Now,
                Hora = TimeSpan.FromHours(10),
                Motivo = "Search Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(cita);

            // Act
            var result = await _service.SearchByIdAsync(cita.CitasID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Search Cita", result.Motivo);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrue_WhenCitaExists()
        {
            // Arrange
            var cita = new Citas
            {
                PersonasID = 1,
                MascotasPersonasID = 1,
                EstadosCitasID = 1,
                Fecha = DateTime.Now,
                Hora = TimeSpan.FromHours(10),
                Motivo = "Exist Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(cita);

            // Act
            var exists = await _service.ExistAsync(cita.CitasID);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnFalse_WhenCitaDoesNotExist()
        {
            // Act
            var exists = await _service.ExistAsync(999);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsert_WhenCitaIsNew()
        {
            // Arrange
            var cita = new Citas
            {
                CitasID = 0,
                PersonasID = 1,
                MascotasPersonasID = 1,
                EstadosCitasID = 1,
                Fecha = DateTime.Now,
                Hora = TimeSpan.FromHours(10),
                Motivo = "Save New Cita",
                IsDeleted = false
            };

            // Act
            var result = await _service.SaveAsync(cita);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SaveAsync_ShouldUpdate_WhenCitaExists()
        {
            // Arrange
            var cita = new Citas
            {
                PersonasID = 1,
                MascotasPersonasID = 1,
                EstadosCitasID = 1,
                Fecha = DateTime.Now,
                Hora = TimeSpan.FromHours(10),
                Motivo = "Save Update Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(cita);
            cita.Motivo = "Updated via Save";

            // Act
            var result = await _service.SaveAsync(cita);

            // Assert
            Assert.True(result);
        }
    }
}