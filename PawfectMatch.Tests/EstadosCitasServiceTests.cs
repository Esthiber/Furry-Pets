using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;

namespace PawfectMatch.Tests
{
    public class EstadosCitasServiceTests
    {
        private readonly EstadosCitasService _service;
        private readonly ApplicationDbContext _context;

        public EstadosCitasServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"EstadosCitasServiceTestDb_{Guid.NewGuid()}")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new EstadosCitasService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddEstadoCita()
        {
            // Arrange
            var estadoCita = new EstadosCitas
            {
                Nombre = "Test Estado Cita",
                IsDeleted = false
            };

            // Act
            var result = await _service.InsertAsync(estadoCita);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnEstadosCitas()
        {
            // Arrange
            var estadoCita = new EstadosCitas
            {
                Nombre = "Test Estado Cita List",
                IsDeleted = false
            };
            await _service.InsertAsync(estadoCita);

            // Act
            var estadosCitas = await _service.ListAsync(e => !e.IsDeleted);

            // Assert
            Assert.NotNull(estadosCitas);
            Assert.Contains(estadosCitas, e => e.Nombre == "Test Estado Cita List");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEstadoCita()
        {
            // Arrange
            var estadoCita = new EstadosCitas
            {
                Nombre = "Update Estado Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(estadoCita);
            estadoCita.Nombre = "Updated Estado Cita";

            // Act
            var result = await _service.UpdateAsync(estadoCita);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteEstadoCita()
        {
            // Arrange
            var estadoCita = new EstadosCitas
            {
                Nombre = "Delete Estado Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(estadoCita);

            // Act
            var result = await _service.DeleteAsync(estadoCita.EstadosCitasID);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnEstadoCita()
        {
            // Arrange
            var estadoCita = new EstadosCitas
            {
                Nombre = "Search Estado Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(estadoCita);

            // Act
            var result = await _service.SearchByIdAsync(estadoCita.EstadosCitasID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Search Estado Cita", result.Nombre);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrue_WhenEstadoCitaExists()
        {
            // Arrange
            var estadoCita = new EstadosCitas
            {
                Nombre = "Exist Estado Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(estadoCita);

            // Act
            var exists = await _service.ExistAsync(estadoCita.EstadosCitasID);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnFalse_WhenEstadoCitaDoesNotExist()
        {
            // Act
            var exists = await _service.ExistAsync(999);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsert_WhenEstadoCitaIsNew()
        {
            // Arrange
            var estadoCita = new EstadosCitas
            {
                EstadosCitasID = 0,
                Nombre = "Save New Estado Cita",
                IsDeleted = false
            };

            // Act
            var result = await _service.SaveAsync(estadoCita);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SaveAsync_ShouldUpdate_WhenEstadoCitaExists()
        {
            // Arrange
            var estadoCita = new EstadosCitas
            {
                Nombre = "Save Update Estado Cita",
                IsDeleted = false
            };
            await _service.InsertAsync(estadoCita);
            estadoCita.Nombre = "Updated via Save";

            // Act
            var result = await _service.SaveAsync(estadoCita);

            // Assert
            Assert.True(result);
        }
    }
}