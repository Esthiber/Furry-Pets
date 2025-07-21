using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;

namespace PawfectMatch.Tests
{
    public class EstadosServiceTests
    {
        private readonly EstadosService _service;
        private readonly ApplicationDbContext _context;

        public EstadosServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"EstadosServiceTestDb_{Guid.NewGuid()}")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new EstadosService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddEstado()
        {
            // Arrange
            var estado = new Estados
            {
                Nombre = "Test Estado",
                IsDeleted = false
            };

            // Act
            var result = await _service.InsertAsync(estado);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnEstados()
        {
            // Arrange
            var estado = new Estados
            {
                Nombre = "Test Estado List",
                IsDeleted = false
            };
            await _service.InsertAsync(estado);

            // Act
            var estados = await _service.ListAsync(e => !e.IsDeleted);

            // Assert
            Assert.NotNull(estados);
            Assert.Contains(estados, e => e.Nombre == "Test Estado List");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEstado()
        {
            // Arrange
            var estado = new Estados
            {
                Nombre = "Update Estado",
                IsDeleted = false
            };
            await _service.InsertAsync(estado);
            estado.Nombre = "Updated Estado";

            // Act
            var result = await _service.UpdateAsync(estado);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteEstado()
        {
            // Arrange
            var estado = new Estados
            {
                Nombre = "Delete Estado",
                IsDeleted = false
            };
            await _service.InsertAsync(estado);

            // Act
            var result = await _service.DeleteAsync(estado.EstadoID);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnEstado()
        {
            // Arrange
            var estado = new Estados
            {
                Nombre = "Search Estado",
                IsDeleted = false
            };
            await _service.InsertAsync(estado);

            // Act
            var result = await _service.SearchByIdAsync(estado.EstadoID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Search Estado", result.Nombre);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrue_WhenEstadoExists()
        {
            // Arrange
            var estado = new Estados
            {
                Nombre = "Exist Estado",
                IsDeleted = false
            };
            await _service.InsertAsync(estado);

            // Act
            var exists = await _service.ExistAsync(estado.EstadoID);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnFalse_WhenEstadoDoesNotExist()
        {
            // Act
            var exists = await _service.ExistAsync(999);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsert_WhenEstadoIsNew()
        {
            // Arrange
            var estado = new Estados
            {
                EstadoID = 0,
                Nombre = "Save New Estado",
                IsDeleted = false
            };

            // Act
            var result = await _service.SaveAsync(estado);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SaveAsync_ShouldUpdate_WhenEstadoExists()
        {
            // Arrange
            var estado = new Estados
            {
                Nombre = "Save Update Estado",
                IsDeleted = false
            };
            await _service.InsertAsync(estado);
            estado.Nombre = "Updated via Save";

            // Act
            var result = await _service.SaveAsync(estado);

            // Assert
            Assert.True(result);
        }
    }
}